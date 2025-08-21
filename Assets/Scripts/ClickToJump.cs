// ClickToJump.cs  （Assets/ 内に作成、asmdef 無し = Assembly-CSharp）
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
using StarterAssets; // StarterAssetsInputs を使う

public class ClickToJump : MonoBehaviour
{
    private StarterAssetsInputs starterAssetInputs;
    //private FirstPersonController firstPersonController;

    void Awake()
    {
        starterAssetInputs = GetComponent<StarterAssetsInputs>();
        if (starterAssetInputs == null)
        {
            Debug.LogError("StarterAssetsInputs が見つかりません。プレイヤーにアタッチしてください。");
        }
    }

    void Update()
    {
#if ENABLE_INPUT_SYSTEM
        // 3Dビュー中のみ・待機中でない時のみ
        if (GameManager.now2Dor3D == 1 && !GameManager.isWaiting)
        {
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                starterAssetInputs.JumpInput(true);
                //firstPersonController.isJumping = true;
            }
        }
#endif
    }
}
