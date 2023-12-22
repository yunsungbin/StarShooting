using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.IO;

[CreateAssetMenu(fileName = "User", menuName = "Game/User", order = int.MinValue), Serializable]
public class User : ScriptableObject
{
    private static User _instance = null;
    public static User Instance => _instance;

    RankerCon ranker = new RankerCon();
    public List<RankingUser> rankingDatas = new List<RankingUser>();

    string dataSavePath => Application.dataPath + "/savedata.json";
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        _instance = this;
        JsonLoad();
    }

    public List<RankingUser> GetFiveTopRankers()
    {
        List<RankingUser> result = new List<RankingUser>();

        var ordered = rankingDatas.OrderByDescending((item) => item.score).ToList();

        // 최대 5개 정보, 5개 미만일 경우 반복문을 그만큼만
        int count = 0;
        for (int i = 0; i < ordered.Count(); i++)
        {
            if (count < 5)
                result.Add(ordered[i]);
            else
                break;

            count++;
        }

        return result;
    }
    public void AddRanking(string name, float score, DateTime saveDate)
    {
        RankingUser user = new RankingUser(name, score, saveDate);
        rankingDatas.Add(user);
        JsonSave();
    }

    string jsonKey => "rankingSave";

    void JsonSave()
    {
        ranker.users = rankingDatas;
        string result = JsonUtility.ToJson(ranker, true);

        File.WriteAllText(jsonKey, result);
    }

    void JsonLoad()
    {
        string result = "";

        if (File.Exists(jsonKey))
        {
            result = File.ReadAllText(jsonKey);
        }

        if (!string.IsNullOrEmpty(result))
        {
            ranker = JsonUtility.FromJson<RankerCon>(result);
            rankingDatas = ranker.users;
        }
        else
        {
            ranker = new RankerCon();
            rankingDatas = new List<RankingUser>();
        }

    }
}
[Serializable]
public class RankerCon
{
    public List<RankingUser> users = new List<RankingUser>();
}

[Serializable]
public class RankingUser
{
    public string name;
    public float score;
    public string saveDate;

    public RankingUser(string _name, float _score, DateTime _saveDate)
    {
        name = _name;
        score = _score;
        saveDate = _saveDate.ToString();
    }
}
