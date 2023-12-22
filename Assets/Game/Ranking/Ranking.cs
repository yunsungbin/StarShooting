using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    public Text NameTxt;
    public Text ScoreTxt;
    public Text dateText;
    public void InitUnit(RankingUser user)
    {
        NameTxt.text = user.name;
        ScoreTxt.text = string.Format("{0:#,0}", user.score);
        dateText.text = user.saveDate.ToString();
    }
}
