using DG.Tweening;
using MK.Toon;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ClearOrOverManager : MonoBehaviour
{
    [SerializeField] StageChanger stageChanger;
    [SerializeField] GameObject clearImage;
    [SerializeField] List<TMP_Text> nextStages;
    [Tooltip("シーン遷移のトランジションの時間"), SerializeField] float fadeTime = 2f;
    [SerializeField] float clearTextExpandDuration = 0.5f;
    [SerializeField] float clearTextShrinkDuration = 2f;
    public static ClearOrOverManager Instance;
    TMP_Text[] clearTexts;
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

        clearTexts = clearImage.GetComponentsInChildren<TMP_Text>(true);
        SetClearImageAlpha(0f);
        SetClearTextAlpha(0f);
        SetNextStageTextAlpha(0f);
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
            FadeClearText(1f, 0f);
            yield return new WaitForSeconds(clearTextExpandDuration + clearTextShrinkDuration);
            //フェードアウト
            FadeClearImage(1f, fadeTime);
            yield return new WaitForSeconds(fadeTime);
            //次のStageTitleを表示する
            PlayNextStageTitle();
            yield return new WaitForSeconds(1.5f);

            if (GameManager.nowStage == 2)
            {
                //Successの文字を消す
                SetClearTextAlpha(0f);
                //エンディンング動画再生
                //ここまで初期化する必要は無いと思うけど一応ね
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
                FadeClearImage(0f, fadeTime);
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
        FadeClearImage(1f, 0f);
        StageChanger.Instance.GotoTitle();
        yield return new WaitForSeconds(1);
        FadeClearImage(0f, 2f);
    }

    //フェードアウト、フェードインを加えたシーン遷移処理
    public IEnumerator ChangeStageTransition(int stage, int dim)
    {
        if(!fading)
        {
            GameManager.isWaiting = true;
            fading = true;
            //フェードアウト
            FadeClearImage(1f, fadeTime);
            yield return new WaitForSeconds(fadeTime);
            //ステージ移動
            stageChanger.ChangeStages(stage, dim);
            //フェードイン
            FadeClearImage(0f, fadeTime);
            //↓これ書くと何故かコルーチンが動かなくなる
            //yield return new WaitForSeconds(fadeTime);
            fading = false;
        }
        else
        {
            yield break;
        }
    }

    public void FadeClearImage(float alpha, float duration)
    {
        clearImage.GetComponent<Image>().DOFade(alpha, duration);
    }

    public void FadeClearText(float alpha, float duration)
    {
        //Successを表示する
        foreach (var text in clearTexts)
        {
            if (text != null)
            {
                text.DOKill();
                text.rectTransform.DOKill();

                text.rectTransform.localScale = Vector3.one;
                var textColor = text.color;
                textColor.a = alpha;
                text.color = textColor;

                Sequence seq = DOTween.Sequence();
                seq.Append(text.rectTransform.DOScale(Vector3.one * 1.3f, clearTextExpandDuration).SetEase(Ease.OutQuad));
                seq.Append(text.rectTransform.DOScale(Vector3.one, clearTextShrinkDuration).SetEase(Ease.InOutQuad));
                seq.Join(text.DOFade(0f, clearTextShrinkDuration).SetEase(Ease.InOutQuad));
            }
        }
    }

    public void SetClearImageAlpha(float alpha)
    {
        var image = clearImage.GetComponent<Image>();
        var imageColor = image.color;
        imageColor.a = alpha;
        image.color = imageColor;
    }

    public void SetClearTextAlpha(float alpha)
    {
        foreach (var text in clearTexts)
        {
            if (text == null) continue;
            var textColor = text.color;
            textColor.a = alpha;
            text.color = textColor;
        }
    }

    public void SetNextStageTextAlpha(float alpha)
    {
        if (nextStages == null) return;

        foreach (var text in nextStages)
        {
            if (text == null) continue;
            var textColor = text.color;
            textColor.a = alpha;
            text.color = textColor;
        }
    }

    public void PlayNextStageTitle()
    {
        if (nextStages == null) return;
        if (GameManager.nowStage < 0 || GameManager.nowStage >= nextStages.Count) return;

        var text = nextStages[GameManager.nowStage];
        if (text == null) return;

        text.DOKill();
        text.rectTransform.DOKill();
        text.rectTransform.localScale = Vector3.one;

        var textColor = text.color;
        textColor.a = 1f;
        text.color = textColor;

        Sequence seq = DOTween.Sequence();
        seq.Join(text.DOFade(1f, 1.5f).SetEase(Ease.InOutQuad));
        seq.Join(text.DOFade(0f, 2f).SetEase(Ease.InOutQuad));

    }
}
