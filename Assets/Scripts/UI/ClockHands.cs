using UnityEngine;

public class ClockHands : MonoBehaviour
{
    float angle;
    float limitTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //時計の針の角度を、経過時間/制限時間*360にする
        if(GameManager.now2Dor3D == 0)
        {
            limitTime = ViewManager.Instance.Stages[GameManager.nowStage].limitTime2D;

        }
        else
        {
            limitTime = ViewManager.Instance.Stages[GameManager.nowStage].limitTime3D;
        }
        angle = -(1 - GameManager.elapsedTime / limitTime) * 360;
        gameObject.transform.localEulerAngles = new Vector3 (0f, 0f, angle);
    }
}
