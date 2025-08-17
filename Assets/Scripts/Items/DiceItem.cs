using UnityEngine;

public class DiceItem : MonoBehaviour
{
    [Tooltip("ヒエラルキー上のPlayerCapsuleオブジェクト"), SerializeField] GameObject player;
    [Tooltip("追加する制限時間（秒）"), SerializeField] private float addTime = 7f;
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
            AddElapsedTime(addTime);
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

    void AddElapsedTime(float t)
    {
        GameManager.elapsedTime += t;
    }
}
