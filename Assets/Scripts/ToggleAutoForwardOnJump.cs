﻿// ToggleAutoForwardOnJump.cs （新規, Assembly-CSharp）
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
        bool g = fpc.Grounded;                 // FPCの接地フラグを利用
        if (wasGrounded && !g)                 // 接地→非接地 = ジャンプ成立
        {
            if(ViewManager.Instance.Stages[GameManager.nowStage].limitTime2D - GameManager.elapsedTime > 0.1f)
            {
                //ステージが切り替わってすぐの場合はトグルしないように
                fpc.ToggleAutoForward();           // 前進オン/オフ切替
                Debug.Log("Toggled!");
            }
        }
            

        wasGrounded = g;
    }
}
