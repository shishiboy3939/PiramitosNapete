using UnityEngine;

public class Start3DButton : MonoBehaviour
{
    [SerializeField] StageChanger stageChanger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ボタンが押されたらすぐ3D画面へ
    public void Start3DGame()
    {
        stageChanger.ChangeStages(GameManager.nowStage, 1);
    }
}
