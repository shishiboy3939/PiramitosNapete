using StarterAssets;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    [SerializeField] FirstPersonController player;
    public bool isRunning = false;
    public float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpeedTimer();
    }

    void SpeedTimer()
    {
        if(!isRunning)
        {
            player.isSprinting = false;
            timer = 0;
        }
        else
        {
            player.isSprinting = true;
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                isRunning = false;
                timer = 0;
            }
        }
    }
}
