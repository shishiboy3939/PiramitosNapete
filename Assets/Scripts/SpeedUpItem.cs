using StarterAssets;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class SpeedUpItem : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float sprintSpeed = 8.0f;
    [SerializeField] float waitSeconds = 10.0f;
    private bool Triggered = false;
    private float nowSpeed = 0;
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
        if (col.gameObject.name == player.name && !Triggered)
        {
            StartCoroutine(DelayCoroutine(col));
        }
    }

    private IEnumerator DelayCoroutine(Collider col)
    {
        Triggered = true;
        FirstPersonController colObj = col.gameObject.GetComponent<FirstPersonController>();
        nowSpeed = colObj.MoveSpeed;
        colObj.MoveSpeed = sprintSpeed;
        // 10•bŠÔ‘Ò‚Â
        yield return new WaitForSeconds(waitSeconds);
        // 10•bŒã‚É‘¬“x‚ð–ß‚·
        colObj.MoveSpeed = nowSpeed;
        Destroy(gameObject);
    }
}
