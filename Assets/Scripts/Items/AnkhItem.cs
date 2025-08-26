using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class AnkhItem : MonoBehaviour
{
    [Tooltip("ヒエラルキー上のPlayerCapsuleオブジェクト"), SerializeField] GameObject player;
    [Tooltip("ヒエラルキー上のStageChanger"), SerializeField] private StageChanger stageChanger;
    [Tooltip("ヒエラルキー上のStageChanger"), SerializeField] private ViewManager viewManager;
    [SerializeField] GameObject spotLight;
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
            StartCoroutine(AnkhEmote());
        }
    }
    IEnumerator AnkhEmote()
    {
        spotLight.SetActive(true);
        SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.SE_AppearKey);
        yield return new WaitForSeconds(1);
        AnkhRestart();
        spotLight.SetActive(false);
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
