using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;

    void LateUpdate()
    {
        if (targetCamera == null)
        {
            // カメラが指定されていなければメインカメラを使う
            targetCamera = Camera.main;
            if (targetCamera == null) return;
        }


         transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.forward,targetCamera.transform.rotation * Vector3.up);
    }
}
