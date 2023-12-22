using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class InGameManager : MonoBehaviour
{
    private static InGameManager _instance = null;
    public static InGameManager Instance => _instance;
    ScoreBox scoreBox;

    private void Awake()
    {
        _instance = this;
    }
    [Header("UI")]
    public Cavas cavas;

    public int stageIndex;
    public List<StageBase> stages = new List<StageBase>();
    StageBase curStage;

    [Header("player")]
    public Transform playerSpawnPos;

    [Header("Score")]
    public static float score;
    public static bool scoreGet = false;
    public float scoreIncreasing = 10f;

    [Header("Skill")]
    public float healMaxDelay = 15f;
    public float bombMaxDelay = 30f;
    float healCurDelay;
    float bombCurDelay;

    public PlayerControl ConPlayer;
    public GameObject skillBomb;

    [Header("Timer")]
    public float time = 0;
    public static float times = 0;

    private float TimeScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        stageIndex = Instance.stageIndex;  

        stages = GetComponents<StageBase>().ToList();
        curStage = stages[stageIndex];

        StartCoroutine(IntroLogic());
    }

    // Update is called once per frame
    void Update()
    {
        ScoreUp();
        SkillSeting();
        timer();
    }

    IEnumerator IntroLogic()
    {
        int charIdx = TempData.curFlightIndex;

        cavas.weapon.InitWeapon(charIdx);

        yield return StartCoroutine(moveTo(ConPlayer.transform, new Vector3(0, -4), 1f));

        yield return StartCoroutine(IngameLogic());

        yield break;
    }

    IEnumerator moveTo(Transform subject, Vector3 targetPos, float duration)
    {
        float timer = 0f;
        Vector3 startPos = subject.position;

        while (timer <= duration)
        {
            subject.position = Vector3.Lerp(startPos, targetPos, easeOutCubic(timer / duration));
            timer += Time.deltaTime;
            yield return null;
        }


        yield return null;

        float easeOutCubic(float x)
        {
            return 1f - Mathf.Pow(1f - x, 3f);
        }
    }

    void Score()
    {
        score += 10;

        if (ConPlayer == null) return;
    }

    void ScoreUp()
    {
        if (TimeScore > 0.5f)
        {
            Score();
            TimeScore = 0;
        }
        if (scoreGet == true)
        {
            score += 1000;
            scoreGet = false;
        }
        TimeScore += Time.deltaTime;
    }

    void timer()
    {
        if(time > 1)
        {
            times += 1;
            time = 0;
        }
        time += Time.deltaTime;
    }

    void SkillSeting()
    {
        if (ConPlayer == null) return;

        healCurDelay += Time.deltaTime;
        bombCurDelay += Time.deltaTime;

        if(Input.GetKey(KeyCode.X) && healCurDelay >= healMaxDelay)
        {
            ConPlayer.HpRecover(ConPlayer.MaxHp / 2f);
            healCurDelay = 0f;
        }

        if (Input.GetKey(KeyCode.C) && bombCurDelay >= bombMaxDelay)
        {
            Instantiate(skillBomb, ConPlayer.transform.position, Quaternion.identity);
            bombCurDelay = 0f;
        }

        cavas.healSkillGauge.Setskill(1 - healCurDelay / healMaxDelay);
        cavas.BombSkillGauge.Setskill(1 - bombCurDelay / bombMaxDelay);
    }

    IEnumerator IngameLogic()
    {
        yield return StartCoroutine(curStage.StageRoutine());

        TempData.stageScore[stageIndex] = score;

        SceneManager.LoadScene("Ranking");

        if (stageIndex < 2)
        {
            TempData.stageIndex++;
            SceneManager.LoadScene("InGame");

        }
        else
        {
            SceneManager.LoadScene("Ranking");
        }
        yield break;
    }
}
