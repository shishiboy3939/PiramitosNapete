using UnityEngine;
using UnityEngine.AI;

public class GoalItem : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] ClearOrOverManager clearOrOverManager;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] NavMeshAgentController navMeshAgentController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        //プレイヤーと接触した場合
        if (col.gameObject.name == player.name)
        {
            //敵のsetActiveをfalseに
            navMeshAgent.gameObject.SetActive(false);
            navMeshAgentController.gameObject.SetActive(false);
            //クリア画面を呼び出し
            clearOrOverManager.StageClear();
        }
    }

}
