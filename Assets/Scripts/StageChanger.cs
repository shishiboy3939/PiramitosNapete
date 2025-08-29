using MK.Toon;
using StarterAssets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class StageChanger : MonoBehaviour
{
    public static StageChanger Instance;
    [SerializeField] ViewManager viewManager;
    [SerializeField] FirstPersonController fpc;
    public bool tutorialOn2D;
    public bool tutorialOn3D;
    [SerializeField] Tutorialmanager tutorialmanager;
    [SerializeField] private List<NavMeshAgent> agents;
    public List<GameObject> goalItem;
    [SerializeField] private List<GameObject> allItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        GotoTitle();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// ステージを変える
    /// </summary>
    /// <param name="stage">変える先のステージの番号</param>
    /// <param name="dim">2Dか3Dか（0だと2D、1だと3D）</param>
    public void ChangeStages(int stage, int dim)
    {
        viewManager.InitializeStages();
        if (dim == 0)
        {
                foreach (var g in goalItem)
            {
                g.SetActive(false);
            }
            //2Dのとき
            viewManager.Stages[stage].mazeCanvas.SetActive(true);
            //線を全部消す
            DestroyLines();
            viewManager.StrokeManager2D.gameObject.SetActive(true);
            viewManager.camera2D.SetActive(true);
            GameManager.elapsedTime = viewManager.Stages[stage].limitTime2D;
            SoundManager.Instance.PlayBgm(SoundManager.Instance.MapBGM);
            if (tutorialOn2D)
            {
                tutorialmanager.CallTutorial();
            }
            SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.SE_Appear2DMap);
        }
        else if (dim == 1)
        {
            //3Dのとき
            viewManager.Stages[stage].mazeCubes.SetActive(true);
            //3Dステージの線をステージ標高に沿った高さに
            //標高を測りたいオブジェクトにはGroundタグを付けて！！！
            viewManager.StrokeManager3D.SetupStrokeHeight(viewManager, stage);
            //プレイヤーを指定座標に配置
            viewManager.playerCapsule.transform.position = viewManager.Stages[stage].playerPosition;
            viewManager.playerCapsule.transform.localEulerAngles = viewManager.Stages[stage].playerRotation;
            viewManager.playerCapsule.SetActive(true);
            //最初はプレイヤーがダッシュしないように
            fpc.autoForward = false;
            //位置のリセットが必要なGameObjectの位置をリセット
            foreach (ResetObject r in viewManager.Stages[stage].resetObjects)
            {
                r.ResetPosition();
                r.gameObject.SetActive(true);
            }
            viewManager.StrokeManager3D.gameObject.SetActive(true);
            viewManager.camera3D.SetActive(true);
            GameManager.elapsedTime = viewManager.Stages[stage].limitTime3D;
            fpc.Grounded = true;
            if (GameManager.nowStage == 0)
            {
                SoundManager.Instance.PlayBgm(SoundManager.Instance.Stage01BGM);
                if (tutorialOn3D)
                {
                    tutorialmanager.CallTutorial();
                    StopAllAgents();
                }
                else
                {
                    SoundManager.Instance.FootStepPlay(SoundManager.Instance.SE_FootStep);
                }
            }
            else if (GameManager.nowStage == 1)
            {
                SoundManager.Instance.PlayBgm(SoundManager.Instance.Stage02BGM);
                SoundManager.Instance.FootStepPlay(SoundManager.Instance.SE_FootStep);
            }
            else if (GameManager.nowStage == 2)
            {
                SoundManager.Instance.PlayBgm(SoundManager.Instance.Stage03BGM);
                SoundManager.Instance.FootStepPlay(SoundManager.Instance.SE_FootStep);
            }
            foreach (var i in allItem)
            {
                i.SetActive(true);
            }

        }
        GameManager.nowStage = stage;
        GameManager.now2Dor3D = dim;
        GameManager.isWaiting = false;
    }

    /// <summary>
    /// 初期化してタイトル画面に戻る
    /// </summary>
    public void GotoTitle()
    {
        viewManager.InitializeStages();
        //線を全部消す
        DestroyLines();
        viewManager.titleScreen.SetActive(true);
        viewManager.camera2D.SetActive(true);
        GameManager.elapsedTime = 0;
        GameManager.nowStage = 0;
        GameManager.now2Dor3D = 0;
        GameManager.isWaiting = true;
        SoundManager.Instance.PlayBgm(SoundManager.Instance.TitleBGM);
        tutorialmanager.Reset();
        //途中離席を鑑みて念のためゴールとアイテムを初期状態に
        foreach (var g in goalItem)
        {
            g.SetActive(false);
        }
        foreach (var a in allItem)
        {
            a.SetActive(true);
        }
    }

    //線を全部消す
    void DestroyLines()
    {
        foreach (Transform n in viewManager.StrokeManager2D.transform)
        {
            Destroy(n.gameObject);
        }
        viewManager.StrokeManager2D.lineRenderers2D = new List<LineRenderer>();
        foreach (Transform n in viewManager.StrokeManager3D.transform)
        {
            Destroy(n.gameObject);
        }
        viewManager.StrokeManager3D.lineRenderers3D = new List<LineRenderer>();
    }
        public void StopAllAgents()
    {
        foreach (var a in agents)
        {
            if (a != null) a.isStopped = true;
        }
    }

    public void ResumeAllAgents()
    {
        foreach (var a in agents)
        {
            if (a != null) a.isStopped = false;
        }
    }
}
