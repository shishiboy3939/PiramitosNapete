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
            SpeedUp();
            gameObject.SetActive(false);
        }
    }
    public void SpeedUp()
    {
        SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.SE_SpeedUp);
        controller.isRunning = true;
        controller.timer = waitSeconds;
        // UIフェード
        var img = ViewManager.Instance.SpeedUp;
        DG.Tweening.Sequence seq = DOTween.Sequence();

        seq.Append(img.DOFade(0.3f, 1f))  // 1秒で α=0.5
           .AppendInterval(2f)                   // 2秒待機
           .Append(img.DOFade(0f, 1f));  // 1秒で α=0
    }

}
