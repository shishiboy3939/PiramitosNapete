// ToggleAutoForwardOnJump.cs （新規, Assembly-CSharp）
using UnityEngine;
using StarterAssets;

public class ToggleAutoForwardOnJump : MonoBehaviour
{
    [SerializeField] FirstPersonController fpc;
    bool wasGrounded;

    void Awake()
    {
        if (!fpc) fpc = GetComponent<FirstPersonController>();
    }

    void Update()
    {

    }
}
