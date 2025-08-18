using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] StageChanger stageChanger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkTitle();
    }

    //マウスを押下したら最初のステージに進む
    void checkTitle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //最初のステージへ
            stageChanger.ChangeStages(0, 0);
        }
    }

}
