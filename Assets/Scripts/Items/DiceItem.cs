using System.Collections;
using UnityEngine;

public class DiceItem : MonoBehaviour
{
    [Tooltip("ヒエラルキー上のPlayerCapsuleオブジェクト"), SerializeField] GameObject player;
    [Tooltip("追加する制限時間（秒）"), SerializeField] private float addTime = 7f;
    Animator m_player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.m_player = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //プレイヤーと衝突したら、制限時間を追加
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == player.name)
        {
            StartCoroutine(BreakDice());
        }
    }
    IEnumerator BreakDice()
    {
        m_player.SetTrigger("DiceOpen");
        SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.SE_breakDice);
        yield return new WaitForSeconds(1);
        AddElapsedTime(addTime);
        gameObject.SetActive(false);
        StageChanger.Instance.goalItem[GameManager.nowStage].SetActive(true); //ゴールを有効に
    }

    void AddElapsedTime(float t)
    {
        GameManager.elapsedTime += t;
    }
}
