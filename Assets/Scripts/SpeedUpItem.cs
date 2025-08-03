using StarterAssets;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class SpeedUpItem : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] SpeedController controller;
    [SerializeField] float waitSeconds = 10.0f;
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
            Destroy(gameObject);
        }
    }

    void LaunchSpeedController()
    {
        controller.isRunning = true;
        controller.timer = waitSeconds;
    }
}
