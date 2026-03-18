using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClearOrOverManager : MonoBehaviour
{
    [SerializeField] private StageChanger stageChanger;
    [SerializeField] private GameObject clearImage;
    [SerializeField] private TMP_Text firstStageTitle;
    [SerializeField] private List<TMP_Text> nextStages;

    [Tooltip("最後のステージ番号")]
    [SerializeField] private int lastStageIndex = 2;

    [Tooltip("シーン遷移のトランジション時間")]
    [SerializeField] private float fadeTime = 2f;

    [SerializeField] private float clearTextExpandDuration = 0.5f;
    [SerializeField] private float clearTextShrinkDuration = 2f;

    [Header("次ステージタイトル表示")]
    [SerializeField] private float nextStageFadeInDuration = 1f;
    [SerializeField] private float nextStageStayDuration = 1f;
    [SerializeField] private float nextStageFadeOutDuration = 2f;

    public static ClearOrOverManager Instance;

    private TMP_Text[] clearTexts;
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
            return;
        }

        clearTexts = clearImage.GetComponentsInChildren<TMP_Text>(true);

        SetClearImageAlpha(0f);
        SetClearTextAlpha(0f);
        SetFirstStageTitleAlpha(0f);
        SetNextStageTextAlpha(0f);
    }

    private void Update()
    {
        CheckElapsedTime();
    }

    private void CheckElapsedTime()
    {
        if (!GameManager.isWaiting)
        {
            if (GameManager.now2Dor3D == 0)
            {
                if (GameManager.elapsedTime <= 0)
                {
                    StartCoroutine(ChangeStageTransition(GameManager.nowStage, 1));
                }
            }
            else
            {
                if (GameManager.elapsedTime <= 0)
                {
                    GameOver();
                }
            }

            if (!GameManager.isPausing)
            {
                GameManager.elapsedTime -= Time.deltaTime;
            }
        }
    }

    public void StageClear()
    {
        StartCoroutine(ClearEffect());
    }

    public void GameOver()
    {
        int stageIndex = GameManager.nowStage;
        stageChanger.ChangeStages(stageIndex, 0);
        StartCoroutine(PlayCurrentStageTitle(stageIndex));
    }

    public IEnumerator ClearEffect()
    {
        if (clearStarted)
        {
            yield break;
        }

        clearStarted = true;
        GameManager.isWaiting = true;

        SetNextStageTextAlpha(0f);

        // Success表示
        FadeClearText(1f, 0f);
        yield return new WaitForSeconds(clearTextExpandDuration + clearTextShrinkDuration);

        // 背景を黒フェード
        FadeClearImage(1f, fadeTime);
        yield return new WaitForSeconds(fadeTime);

        // 最後のステージでなければ次のStageTitleを表示
        if (!IsLastStage())
        {
            yield return StartCoroutine(PlayNextStageTitle());
        }

        if (IsLastStage())
        {
            SetClearTextAlpha(0f);
            SetNextStageTextAlpha(0f);

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
            _stage = GameManager.nowStage + 1;
            stageChanger.ChangeStages(_stage, 0);

            SetClearTextAlpha(0f);
            SetNextStageTextAlpha(0f);

            FadeClearImage(0f, fadeTime);
        }

        clearStarted = false;
    }

    public IEnumerator BlackOut()
    {
        FadeClearImage(1f, 0f);
        StageChanger.Instance.GotoTitle();
        yield return new WaitForSeconds(1f);
        FadeClearImage(0f, 2f);
    }

    public IEnumerator ChangeStageTransition(int stage, int dim)
    {
        if (fading)
        {
            yield break;
        }

        bool shouldPlayFirstStageTitle = stage == 0 && dim == 0 && ViewManager.Instance.titleScreen.activeSelf;

        GameManager.isWaiting = true;
        fading = true;

        FadeClearImage(1f, fadeTime);
        yield return new WaitForSeconds(fadeTime);

        stageChanger.ChangeStages(stage, dim);

        if (shouldPlayFirstStageTitle)
        {
            StartCoroutine(PlayFirstStageTitle());
        }

        FadeClearImage(0f, fadeTime);

        fading = false;
    }

    public void FadeClearImage(float alpha, float duration)
    {
        Image image = clearImage.GetComponent<Image>();
        image.DOFade(alpha, duration);
    }

    public void FadeClearText(float alpha, float duration)
    {
        for (int i = 0; i < clearTexts.Length; i++)
        {
            TMP_Text text = clearTexts[i];

            if (text == null)
            {
                continue;
            }

            text.DOKill();
            text.rectTransform.DOKill();

            text.rectTransform.localScale = Vector3.one;

            Color textColor = text.color;
            textColor.a = alpha;
            text.color = textColor;

            Sequence seq = DOTween.Sequence();
            seq.Append(text.rectTransform.DOScale(Vector3.one * 1.3f, clearTextExpandDuration).SetEase(Ease.OutQuad));
            seq.Append(text.rectTransform.DOScale(Vector3.one, clearTextShrinkDuration).SetEase(Ease.InOutQuad));
            seq.Join(text.DOFade(0f, clearTextShrinkDuration).SetEase(Ease.InOutQuad));
        }
    }

    public void SetClearImageAlpha(float alpha)
    {
        Image image = clearImage.GetComponent<Image>();
        Color imageColor = image.color;
        imageColor.a = alpha;
        image.color = imageColor;
    }

    public void SetClearTextAlpha(float alpha)
    {
        for (int i = 0; i < clearTexts.Length; i++)
        {
            TMP_Text text = clearTexts[i];

            if (text == null)
            {
                continue;
            }

            Color textColor = text.color;
            textColor.a = alpha;
            text.color = textColor;
        }
    }

    public void SetFirstStageTitleAlpha(float alpha)
    {
        if (firstStageTitle == null)
        {
            return;
        }

        firstStageTitle.DOKill();
        firstStageTitle.rectTransform.DOKill();

        Color textColor = firstStageTitle.color;
        textColor.a = alpha;
        firstStageTitle.color = textColor;
    }

    public void SetNextStageTextAlpha(float alpha)
    {
        if (nextStages == null)
        {
            return;
        }

        for (int i = 0; i < nextStages.Count; i++)
        {
            TMP_Text text = nextStages[i];

            if (text == null)
            {
                continue;
            }

            text.DOKill();
            text.rectTransform.DOKill();

            Color textColor = text.color;
            textColor.a = alpha;
            text.color = textColor;
        }
    }

    public IEnumerator PlayNextStageTitle()
    {
        if (nextStages == null)
        {
            yield break;
        }

        if (GameManager.nowStage < 0)
        {
            yield break;
        }

        if (GameManager.nowStage >= nextStages.Count)
        {
            yield break;
        }

        SetNextStageTextAlpha(0f);

        TMP_Text text = nextStages[GameManager.nowStage];

        if (text == null)
        {
            yield break;
        }

        text.DOKill();
        text.rectTransform.DOKill();
        text.rectTransform.localScale = Vector3.one;

        Color textColor = text.color;
        textColor.a = 0f;
        text.color = textColor;

        Sequence seq = DOTween.Sequence();
        seq.Append(text.DOFade(1f, nextStageFadeInDuration).SetEase(Ease.InOutQuad));
        seq.AppendInterval(nextStageStayDuration);
        seq.Append(text.DOFade(0f, nextStageFadeOutDuration).SetEase(Ease.InOutQuad));

        yield return seq.WaitForCompletion();

        textColor = text.color;
        textColor.a = 0f;
        text.color = textColor;
    }

    public IEnumerator PlayFirstStageTitle()
    {
        if (firstStageTitle == null)
        {
            yield break;
        }

        yield return StartCoroutine(PlayStageTitleText(firstStageTitle));
    }

    public IEnumerator PlayCurrentStageTitle(int stageIndex)
    {
        TMP_Text stageTitle = GetCurrentStageTitle(stageIndex);

        if (stageTitle == null)
        {
            yield break;
        }

        yield return StartCoroutine(PlayStageTitleText(stageTitle));
    }

    private TMP_Text GetCurrentStageTitle(int stageIndex)
    {
        if (stageIndex == 0)
        {
            return firstStageTitle;
        }

        int titleIndex = stageIndex - 1;
        if (nextStages == null || titleIndex < 0 || titleIndex >= nextStages.Count)
        {
            return null;
        }

        return nextStages[titleIndex];
    }

    private IEnumerator PlayStageTitleText(TMP_Text text)
    {
        if (text == null)
        {
            yield break;
        }

        SetFirstStageTitleAlpha(0f);
        SetNextStageTextAlpha(0f);

        text.DOKill();
        text.rectTransform.DOKill();

        Sequence seq = DOTween.Sequence();
        seq.Append(text.DOFade(1f, nextStageFadeInDuration).SetEase(Ease.InOutQuad));
        seq.AppendInterval(nextStageStayDuration);
        seq.Append(text.DOFade(0f, nextStageFadeOutDuration).SetEase(Ease.InOutQuad));

        yield return seq.WaitForCompletion();

        Color textColor = text.color;
        textColor.a = 0f;
        text.color = textColor;
    }

    private bool IsLastStage()
    {
        if (GameManager.nowStage >= lastStageIndex)
        {
            return true;
        }

        return false;
    }
}
