using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text time;

    void Update()
    {
        SetTime();
    }

    public void SetTime()
    {
        time.text = string.Format("{0:#,0}", InGameManager.times);
    }
}
