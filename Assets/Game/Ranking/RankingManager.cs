using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RankingManager : MonoBehaviour
{
    public Text scoreTxt;
    public InputField nameInput;

    float totalScore;
    DateTime nowTime;

    private void Start()
    {
        totalScore = InGameManager.score;
        nowTime = DateTime.Now;
        scoreTxt.text = string.Format("{0:#,0}", totalScore);
    }

    public void Ranking()
    {
        if (nameInput.text.Length <= 0) return;

        nameInput.text.Replace(" ", "_");
        User.Instance.AddRanking(nameInput.text, totalScore, nowTime);

        SceneManager.LoadScene("Title");
    }


}
