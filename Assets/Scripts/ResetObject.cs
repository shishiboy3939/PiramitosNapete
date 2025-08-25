using UnityEngine;

public class ResetObject : MonoBehaviour
{
    [Header("リセット座標（ワールド座標）")]
    [SerializeField] private Vector3 resetPosition = new Vector3(0, 0, 0);
    [Header("リセット回転（オイラー角）")]
    [SerializeField] private Vector3 resetRotation = new Vector3(0, 0, 0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        
    }

    //初期位置にリセット
    public void ResetPosition()
    {
        gameObject.transform.position = resetPosition;
        gameObject.transform.localEulerAngles =resetRotation;
    }
}
