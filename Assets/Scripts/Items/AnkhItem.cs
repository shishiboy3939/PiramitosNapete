using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkhItem : MonoBehaviour
{
    [SerializeField] GameObject player;

    [Header("減少量（例: 30）")]
    [SerializeField] float reduceAmount = 30f;

    [Header("減少/復帰にかける時間")]
    [SerializeField] float fadeDuration = 0.5f;

    [Header("子の SpriteRenderer / MeshRenderer を割り当て")]
    [SerializeField] Renderer targetRenderer;

    static readonly int FadeID = Shader.PropertyToID("_DirectionalGlowFadeFade"); // ←実名を確認
    Material _mat;
    bool _triggered;

    // 追加: “元のフェード”を保持 & 競合防止用
    float _baseFade;            // Awake時点の元の値
    Coroutine _fadeRoutine;     // 進行中のtween

    void Awake()
    {
        if (!targetRenderer)
        {
            Debug.LogError($"{name}: targetRenderer が未割り当てです。子の Renderer を設定してください。");
            return;
        }
        _mat = targetRenderer.material;
        if (_mat && !_mat.HasProperty(FadeID))
            Debug.LogWarning($"{name}: マテリアルに {FadeID} がありません。内部名を確認してください。");

        // 追加: 元の値を保存
        if (_mat && _mat.HasProperty(FadeID))
            _baseFade = _mat.GetFloat(FadeID);
    }

    void OnTriggerEnter(Collider col)
    {
        if (_triggered || col.gameObject != player) return;
        _triggered = true;

        // 0.5秒かけて reduceAmount だけ下げる
        FadeBy(-reduceAmount, fadeDuration);
        GameManager.isWaiting = true;
        StartCoroutine(AnkhEmote());

        SoundManager.Instance?.PlaySoundEffect(SoundManager.Instance.SE_GetAnk);
    }

    IEnumerator AnkhEmote()
    {
        yield return new WaitForSeconds(1f);
        SoundManager.Instance?.PlaySoundEffect(SoundManager.Instance.SE_Respawn);
        // …あなたのリスタート処理
        ViewManager.Instance.playerCapsule.SetActive(false);
        ViewManager.Instance.playerCapsule.transform.position =
            ViewManager.Instance.Stages[GameManager.nowStage].playerPosition;
        ViewManager.Instance.playerCapsule.transform.localEulerAngles =
            ViewManager.Instance.Stages[GameManager.nowStage].playerRotation;
        ViewManager.Instance.playerCapsule.SetActive(true);
        GameManager.elapsedTime = ViewManager.Instance.Stages[GameManager.nowStage].limitTime3D;
        GameManager.isWaiting = false;
        RestoreToBaseFade(0);
        gameObject.SetActive(false);   
        yield return new WaitForSeconds(1f);        
    }

    // 任意の差分だけ増減（delta）を duration 秒で
    void FadeBy(float delta, float duration)
    {
        if (_mat == null || !_mat.HasProperty(FadeID)) return;
        float start = _mat.GetFloat(FadeID);
        float goal  = start + delta;
        StartFadeTo(goal, duration);
    }

    // 公開：元のフェード（Awake時点）へ戻す
    public void RestoreToBaseFade(float duration = 0f)
    {
        _triggered = false;
        if (_mat == null || !_mat.HasProperty(FadeID)) return;
        StartFadeTo(_baseFade, duration);
    }

    // 共通：goal へ duration 秒で補間
    void StartFadeTo(float goal, float duration)
    {
        if (_fadeRoutine != null) StopCoroutine(_fadeRoutine);
        _fadeRoutine = StartCoroutine(TweenFadeTo(goal, duration));
    }

    IEnumerator TweenFadeTo(float goal, float duration)
    {
        float start = _mat.GetFloat(FadeID);
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float v = Mathf.Lerp(start, goal, Mathf.Clamp01(t / duration));
            _mat.SetFloat(FadeID, v);
            yield return null;
        }
        _mat.SetFloat(FadeID, goal);
        _fadeRoutine = null;
    }
}
