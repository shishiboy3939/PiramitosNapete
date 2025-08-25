using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class AnkhItem : MonoBehaviour
{
    [Tooltip("ヒエラルキー上のPlayerCapsuleオブジェクト"), SerializeField] GameObject player;
    [Tooltip("ヒエラルキー上のStageChanger"), SerializeField] private StageChanger stageChanger;
    [Tooltip("ヒエラルキー上のStageChanger"), SerializeField] private ViewManager viewManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //プレイヤーと衝突したら、ステージをリスタート（制限時間は満タンで）
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == player.name)
        {
            AnkhRestart();
        }
    }

    //プレイヤーの位置を初期値にして、制限時間は満タンにする
    void AnkhRestart()
    {
        viewManager.playerCapsule.SetActive(false);
        viewManager.playerCapsule.transform.position = viewManager.Stages[GameManager.nowStage].playerPosition;
        viewManager.playerCapsule.transform.localEulerAngles = viewManager.Stages[GameManager.nowStage].playerRotation;
        viewManager.playerCapsule.SetActive(true);
        GameManager.elapsedTime = viewManager.Stages[GameManager.nowStage].limitTime3D;
        //リスタートしたら、自身のsetActiveをfalseにして、リスタート後に再度現れないようにする
        gameObject.SetActive(false);
    }
}
