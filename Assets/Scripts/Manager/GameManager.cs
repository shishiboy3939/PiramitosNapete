using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    /// <summary>
    /// 今がどのステージか 0から2まで
    /// </summary>
    public static int nowStage = 0;
    /// <summary>
    /// 今が2D画面か3D画面か 0だと2D、1だと3D
    /// </summary>
    public static int now2Dor3D = 0;
    /// <summary>
    /// 経過時間 ステージ移動したら経過時間は満タン、どんどん減算していく
    /// </summary>
    public static float elapsedTime = 0;
    /// <summary>
    /// プレイヤーと敵の動き、タイマーを止める
    /// </summary>
    public static bool isWaiting = false;
    /// <summary>
    /// タイマーだけを止める
    /// </summary>
    public static bool isPausing = false;
}


