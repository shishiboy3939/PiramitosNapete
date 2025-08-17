using StarterAssets;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

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
            LaunchSpeedController();
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

    void LaunchSpeedController()
    {
        //SpeedControllerクラスのSpeedTimer()を動かす
        controller.isRunning = true;
        controller.timer = waitSeconds;
    }
}
