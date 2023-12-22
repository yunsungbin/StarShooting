using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossTram : EnemyBase
{
    public GameObject bullet;
    public GameObject meteor;
    public GameObject explosion;

    hpGauge hpgauge;

    int phaseLevel;

    public bool isEnd = false;
    public static bool next = false;
    //Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        hpgauge = InGameManager.Instance.cavas.bossHp;
        StartCoroutine(StartIntro());
    }

    protected override void DieDestroy()
    {
        phaseLevel = 0;
        StartCoroutine(Outro());
    }

    IEnumerator Outro()
    {
        float delay = 0.25f;
        float cur = 0f;
        while (transform.position.y < 6.5f)
        {
            if (cur >= delay)
            {
                Instantiate(explosion, transform.position + (Vector3)Random.insideUnitCircle, Quaternion.identity, transform);
                cur = 0f;
            }

            cur += Time.deltaTime;
            transform.Translate(Vector3.up * Time.deltaTime);
            yield return null;
        }
        next = true;
        isEnd = true;
        
        yield break;
    }

    float delay = 3f;
    float cur = 0f;

    //Update is called once per frame
    void Update()
    {
        if (phaseLevel == 1) Phase1();
        if (phaseLevel == 2) Phase2();
        if (delay <= cur)
        {
            Instantiate(meteor, new Vector3(Random.Range(-4, 5), 6), Quaternion.identity);

            cur = 0;
        }
        cur += Time.deltaTime;
    }

    float delay1 = 1f;
    float cur1 = 0f;

    float delay2 = 2.4f;
    float cur2 = 0f;

    float delay3 = 0.3f;
    float cur3 = 0f;
    IEnumerator StartIntro()
    {
        transform.position = new Vector3(-0.19f, 3.31f);

        hpgauge.gameObject.SetActive(true);
        StartCoroutine(gaugeFill(1f));
        phaseLevel = 1;
        yield return new WaitUntil(() => hp / maxHp <= 0.5f);

        cur1 = 0f;
        cur2 = 0f;

        delay1 = 0.2f;
        delay2 = 2f;

        if(hp / maxHp <= 0.5f)
        {
            phaseLevel = 2;
        }
        

        yield break;

        IEnumerator gaugeFill(float duration)
        {
            float timer = 0f;

            while (timer < duration)
            {
                hpgauge.SetGauge(timer / duration);

                timer += Time.deltaTime;
                yield return null;
            }
        }
    }

    void Phase1()
    {
        if(delay1 <= cur1)
        {
            for(int i = 0; i < 360; i += Random.Range(35, 35))
            {
                Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i));
            }

            cur1 = 0;
        }

        if (delay2 <= cur2)
        {
            for (int i = -45; i < 45; i += 6)
            {
                var temp = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i - 90f));
                temp.GetComponent<EnemyBullet>().speed = Random.Range(1f, 5f);
            }

            cur2 = 0;
        }

        cur1 += Time.deltaTime;
        cur2 += Time.deltaTime;

        hpgauge.SetGauge(hp / maxHp);
    }

    int dir = 1;
    void Phase2()
    {
        if(delay1 <= cur1)
        {
            for(int i = 0; i < 8; i++)
            {
                Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i * -30));
            }

            cur1 = 0;
        }
        
        if(delay2 <= cur2)
        {
            for(int i = -45; i < 45; i += 6)
            {
                var temp = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i - 90f));
                temp.GetComponent<EnemyBullet>().speed = Random.Range(1f, 5f);
            }

            cur2 = 0;
        }

        float c = transform.position.x + dir * Time.deltaTime * 3f;
        if(delay3 <= cur3 || c < -3f || c > 3f)
        {
            dir *= -1;
            cur3 = 0f;
            delay3 = Random.Range(0.3f, 1f);
        }

        transform.Translate(Vector3.right * dir * Time.deltaTime * 3f);

        cur1 += Time.deltaTime;
        cur2 += Time.deltaTime;
        cur3 += Time.deltaTime;

        if (hp <= 0)
        {
            next = true;
        }
        hpgauge.SetGauge(hp / maxHp);
    }

    public override void OnDamage(float dmg)
    {
        if (phaseLevel != 0)
            base.OnDamage(dmg);
    }
}
