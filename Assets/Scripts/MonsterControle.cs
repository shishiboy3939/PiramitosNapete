using UnityEngine;

public class MonsterControle : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] ClearOrOverManager clearOrOverManager;

    void OnTriggerEnter(Collider col)
    {
        //プレイヤーと接触した場合
        if (col.gameObject.name == player.name)
        {
            clearOrOverManager.GameOver();
        }
    }
}
