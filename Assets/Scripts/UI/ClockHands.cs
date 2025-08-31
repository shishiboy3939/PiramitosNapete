using UnityEngine;

public class ClockHands : MonoBehaviour
{
    float angle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //時計の針の角度を、経過時間/制限時間*360にする
        angle = -(1 - GameManager.elapsedTime / ViewManager.Instance.Stages[GameManager.nowStage].limitTime2D) * 360;
        gameObject.transform.localEulerAngles = new Vector3 (0f, 0f, angle);
    }
}
