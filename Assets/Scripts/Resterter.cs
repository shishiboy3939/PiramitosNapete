using UnityEngine;

public class Resterter : MonoBehaviour
{
    [SerializeField] private float inactivityTime;
    [SerializeField] private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown || Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.mouseScrollDelta.y != 0) {
                timer = 0f;
            } else {
                timer += Time.deltaTime;
                if (timer >= inactivityTime) {
                    Debug.Log(inactivityTime.ToString() + "秒間の無操作を検知しました。");
                StageChanger.Instance.GotoTitle();
                    timer = 0f;
                }
            }
    }
}
