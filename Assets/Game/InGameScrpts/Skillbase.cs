using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skillbase : MonoBehaviour
{
    public Image skillGauge;

    public void Setskill(float fillAmount)
    {
        skillGauge.fillAmount = fillAmount;

        skillGauge.gameObject.SetActive(fillAmount > 0f);
    }
}
