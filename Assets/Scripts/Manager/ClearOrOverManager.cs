using DG.Tweening;
using MK.Toon;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ClearOrOverManager : MonoBehaviour
{
    [SerializeField] StageChanger stageChanger;
    [SerializeField] GameObject clearImage;
    [Tooltip("シーン遷移のトランジションの時間"), SerializeField] float fadeTime = 2f;
    public static ClearOrOverManager Instance;
    private int _stage = 0;
    private bool clearStarted = false;
    public bool fading = false;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkElapsedTime();
    }

    void checkElapsedTime()
    {
        //GameManager.isWaitingがfalseのとき、経過時間を計測して時間切れか判定する
        if (!GameManager.isWaiting)
        {
            if (GameManager.now2Dor3D == 0)
            {
                //2Dのとき
                if (GameManager.elapsedTime <= 0)
                {
                    //制限時間を超えたら3Dステージへ移動
                    StartCoroutine(ChangeStageTransition(GameManager.nowStage, 1));
                }
            }
            else
            {
                //3Dのとき
                if (GameManager.elapsedTime <= 0)
                {
                    //制限時間を超えたらゲームオーバー
                    GameOver();
                }
            }
            //ポーズ状態じゃなければタイマーを動かす
            if (!GameManager.isPausing)
            {
                GameManager.elapsedTime -= Time.deltaTime;
            }
        }
    }

    public void StageClear()
    {
        //クリア演出のコルーチンを実行
        StartCoroutine(ClearEffect());
    }

    public void GameOver()
    {
        //同じステージの2D画面に戻る
        stageChanger.ChangeStages(GameManager.nowStage, 0);
    }

    //クリア時の演出
    //ChangeStageTransitionとそんなに変わらない
    //もし今後クリア時の演出を変えたければここを変更
    public IEnumerator ClearEffect()
    {
        if (!clearStarted)
        {
            clearStarted = true;
            //クリア画像表示
            GameManager.isWaiting = true;
            yield return new WaitForSeconds(1);
            var image = clearImage.GetComponent<Image>();
            //フェードアウト
            image.DOFade(1, fadeTime);
            yield return new WaitForSeconds(fadeTime);

            if (GameManager.nowStage == 2)
            {
                //エンディンング動画再生
                ViewManager.Instance.InitializeStages();
                ViewManager.Instance.camera2D.SetActive(true);
                GameManager.elapsedTime = 0;
                GameManager.nowStage = 0;
                GameManager.now2Dor3D = 0;
                GameManager.isWaiting = true;
                SoundManager.Instance.StopBgm();
                SoundManager.Instance.StopLongSE();
                VideoManager.Instance.EndingPlay();
            }
            else
            {
                //次のステージの2D画面に進む
                _stage = GameManager.nowStage + 1;
                stageChanger.ChangeStages(_stage, 0);
                //フェードイン
                image.DOFade(0, fadeTime);
                //↓これも死ねや案件
                //yield return new WaitForSeconds(fadeTime);
            }

            //初期化
            clearStarted = false;
        }
        else
        {
            yield break;
        }
    }

    public IEnumerator BlackOut()
    {
        var image = clearImage.GetComponent<Image>();
        image.DOFade(1, 0);
        StageChanger.Instance.GotoTitle();
        yield return new WaitForSeconds(1);
        image.DOFade(0, 2);
    }

    //フェードアウト、フェードインを加えたシーン遷移処理
    public IEnumerator ChangeStageTransition(int stage, int dim)
    {
        if(!fading)
        {
            GameManager.isWaiting = true;
            fading = true;
            //フェードアウト
            var image = clearImage.GetComponent<Image>();
            image.DOFade(0f, 0f);
            image.DOFade(1f, fadeTime);
            yield return new WaitForSeconds(fadeTime);
            //ステージ移動
            stageChanger.ChangeStages(stage, dim);
            //フェードイン
            image.DOFade(0f, fadeTime);
            //↓これ書くと何故かコルーチンが動かなくなる
            //死ねや
            //yield return new WaitForSeconds(fadeTime);
            fading = false;
        }
        else
        {
            yield break;
        }
    }
}
