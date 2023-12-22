using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Temp", menuName = "Game/Temp", order = int.MinValue), Serializable]
public class TempData : ScriptableObject
{
    private static TempData _instance = null;
    public static TempData Instance => _instance;

    public static float[] stageScore = new float[3];
    public static int WeaponLevel = 0; // 0 ~ 3

    public static int stageIndex;

    public static int curFlightIndex;

    public void InitTempData()
    {
        _instance = this;
    }
}
