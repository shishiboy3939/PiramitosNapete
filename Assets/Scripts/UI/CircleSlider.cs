using UnityEngine;
using UnityEngine.UI;

public class CircleSlider : MonoBehaviour
{
    [Tooltip("スライダー画像"), SerializeField] Image countDownImage;
    float angle;
    float limitTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //円型スライダーの角度を、経過時間/制限時間にする
        if (GameManager.now2Dor3D == 0)
        {
            limitTime = ViewManager.Instance.Stages[GameManager.nowStage].limitTime2D;

        }
        else
        {
            limitTime = ViewManager.Instance.Stages[GameManager.nowStage].limitTime3D;
        }
        angle = 1 - GameManager.elapsedTime / limitTime;
        countDownImage.fillAmount = angle;
    }
}
