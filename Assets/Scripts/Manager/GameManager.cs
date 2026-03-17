using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    /// 今がどのステージか 0から2まで
    public static int nowStage = 0;

    /// 今が2D画面か3D画面か 0だと2D、1だと3D
    public static int now2Dor3D = 0;

    /// 経過時間 ステージ移動したら経過時間は満タン、どんどん減算していく
    public static float elapsedTime = 0;

    /// プレイヤーと敵の動き、タイマーを止める
    public static bool isWaiting = false;
    
    /// タイマーだけを止める
    public static bool isPausing = false;
}


