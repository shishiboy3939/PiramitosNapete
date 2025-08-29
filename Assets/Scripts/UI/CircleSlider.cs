using UnityEngine;
using UnityEngine.UI;

public class CircleSlider : MonoBehaviour
{
    [Tooltip("スライダー画像"), SerializeField] Image countDownImage;
    [Tooltip("ヒエラルキー上のStageChanger"), SerializeField] private ViewManager viewManager;
    float angle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //円型スライダーの角度を、経過時間/制限時間にする
        angle = 1 - GameManager.elapsedTime / viewManager.Stages[GameManager.nowStage].limitTime2D;
        countDownImage.fillAmount = angle;
    }
}
