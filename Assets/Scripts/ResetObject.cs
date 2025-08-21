using UnityEngine;

public class ResetObject : MonoBehaviour
{
    private Vector3 _initialPosition; // 初期位置
    private Vector3 _initialRotation; // 初期回転
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //初期位置にリセット
    public void ResetPosition()
    {
        gameObject.transform.position = _initialPosition;
        gameObject.transform.localEulerAngles = _initialRotation;
    }
}
