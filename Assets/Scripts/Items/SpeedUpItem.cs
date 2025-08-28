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
        var image =  ViewManager.Instance.SpeedUp.GetComponent<Image>();
        image.DOFade(1, 2);
        yield return new WaitForSeconds(2);
        ViewManager.Instance.SpeedUp.gameObject.SetActive(false);
    }

}
