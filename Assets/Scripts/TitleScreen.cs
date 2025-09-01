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
            if(!ClearOrOverManager.Instance.fading)
            {
                //タイトル画面をクリックしたらダイスの効果音を流す（一旦）
                SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.SE_GameStart);
            }
            //最初の画面に移動
            StartCoroutine(ClearOrOverManager.Instance.ChangeStageTransition(0, 0));
        }
    }

}
