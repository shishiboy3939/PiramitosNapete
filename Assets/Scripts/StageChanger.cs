using MK.Toon;
using StarterAssets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using DG.Tweening;
public class StageChanger : MonoBehaviour
{
    public static StageChanger Instance;
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
        ViewManager.Instance.InitializeStages();
        if (dim == 0)
        {
            foreach (var g in goalItem)
            {
                g.SetActive(false);
            }
            //2Dのとき
            ViewManager.Instance.Stages[stage].mazeCanvas.SetActive(true);
            //線を全部消す
            DestroyLines();
            ViewManager.Instance.StrokeManager2D.gameObject.SetActive(true);
            ViewManager.Instance.camera2D.SetActive(true);
            GameManager.elapsedTime = ViewManager.Instance.Stages[stage].limitTime2D;
            SoundManager.Instance.PlayBgm(SoundManager.Instance.MapBGM);
            if (tutorialOn2D)
            {
                tutorialmanager.CallTutorial();
            }

            if (!tutorialOn2D && GameManager.nowStage <= 1)
            {
                SoundManager.Instance.PlayLongSE(SoundManager.Instance.LongSE_Clock);
                Debug.Log("タイマー音呼べてる");
            }
            SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.SE_Appear2DMap);
            Cursor.visible = true;
        }
        else if (dim == 1)
        {
            //3Dのとき
            ViewManager.Instance.Stages[stage].mazeCubes.SetActive(true);
            //3Dステージの線をステージ標高に沿った高さに
            //標高を測りたいオブジェクトにはGroundタグを付けて！！！
            ViewManager.Instance.StrokeManager3D.SetupStrokeHeight(ViewManager.Instance, stage);
            //プレイヤーを指定座標に配置
            ViewManager.Instance.playerCapsule.transform.position = ViewManager.Instance.Stages[stage].playerPosition;
            ViewManager.Instance.playerCapsule.transform.localEulerAngles = ViewManager.Instance.Stages[stage].playerRotation;
            ViewManager.Instance.playerCapsule.SetActive(true);
            //最初はプレイヤーがダッシュしないように
            fpc.autoForward = false;
            //位置のリセットが必要なGameObjectの位置をリセット
            foreach (ResetObject r in ViewManager.Instance.Stages[stage].resetObjects)
            {
                r.ResetPosition();
                r.gameObject.SetActive(true);
            }
            ViewManager.Instance.StrokeManager3D.gameObject.SetActive(true);
            ViewManager.Instance.camera3D.SetActive(true);
            GameManager.elapsedTime = ViewManager.Instance.Stages[stage].limitTime3D;
            fpc.Grounded = true;
            SoundManager.Instance.StopLongSE();
            SoundManager.Instance.StopPencilSound();
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
            Cursor.visible = false;

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
        ViewManager.Instance.InitializeStages();
        //線を全部消す
        DestroyLines();
        ViewManager.Instance.titleScreen.SetActive(true);
        ViewManager.Instance.camera2D.SetActive(true);
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
        //SpeedUp透明化
        var image = ViewManager.Instance.SpeedUp.GetComponent<Image>();
        image.DOFade(0, 0);
        SoundManager.Instance.StopLongSE();
        Cursor.visible = false;
    }

    //線を全部消す
    void DestroyLines()
    {
        foreach (Transform n in ViewManager.Instance.StrokeManager2D.transform)
        {
            Destroy(n.gameObject);
        }
       ViewManager.Instance.StrokeManager2D.lineRenderers2D = new List<LineRenderer>();
        foreach (Transform n in ViewManager.Instance.StrokeManager3D.transform)
        {
            Destroy(n.gameObject);
        }
        ViewManager.Instance.StrokeManager3D.lineRenderers3D = new List<LineRenderer>();
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
