using StarterAssets;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SpeedUpItem : MonoBehaviour
{
    [Tooltip("ヒエラルキー上のPlayerCapsuleオブジェクト"), SerializeField] GameObject player;
    [Tooltip("PlayerCapsuleの中のSpeedControllerオブジェクト"), SerializeField] SpeedController controller;
    [Tooltip("速度が速くなる時間（秒）"), SerializeField] float waitSeconds = 10.0f;
    [SerializeField, Range(0f,1f)] float halfAlpha = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == player.name)
        {
            StartCoroutine(SpeedUp());
            gameObject.SetActive(false);
        }
    }
    IEnumerator SpeedUp()
    {
        SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.SE_SpeedUp);
        controller.isRunning = true;
        controller.timer = waitSeconds;
        // UIフェード（Image のアルファを 2秒で halfAlpha に → 1秒で 0 に）
        var imageGO = ViewManager.Instance.SpeedUp;
        if (imageGO != null && imageGO.TryGetComponent<Image>(out var image))
        {
            yield return image.DOFade(halfAlpha, 1f).WaitForCompletion();
            yield return image.DOFade(0f, 1f).WaitForCompletion();
        }
        yield break;
    }

}
