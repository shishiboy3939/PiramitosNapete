using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterControle : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] ClearOrOverManager clearOrOverManager;
    [SerializeField] NavMeshAgent agent; 
    void OnTriggerEnter(Collider col)
    {
        //プレイヤーと接触した場合
        if (col.gameObject == player)
        {
            Debug.Log("衝突は検出できてる");
            StartCoroutine(ChangeKillCam());
        }
    }
    IEnumerator ChangeKillCam()
    {
        Debug.Log("ChangeKillCamは呼べている");
        if (agent != null) agent.isStopped = true;
        ViewManager.Instance.camera3D.SetActive(false);
        ViewManager.Instance.killCamera[GameManager.nowStage].SetActive(true);
        GameManager.isWaiting = true;
        SoundManager.Instance.FootStepStop();
        SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.SE_EatSoul);
        yield return new WaitForSeconds(2);
        ViewManager.Instance.killCamera[GameManager.nowStage].SetActive(false);
        if (agent != null) agent.isStopped = false;
        clearOrOverManager.GameOver();
    }
}
