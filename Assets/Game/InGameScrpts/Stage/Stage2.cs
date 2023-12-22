using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2 : StageBase
{
    private void Start()
    {
        waveList.Add(Wave1);
        waveList.Add(Wave2);
        waveList.Add(Wave3);
        waveList.Add(Wave4);
        waveList.Add(BossWave);
    }

    IEnumerator Wave1()
    {
        for (int i = 0; i < 10; i++)
        {
            var temp = Instantiate(enemies[0], new Vector3(Random.Range(-3, 3), 6), Quaternion.identity);
            if (i == 4) temp.GetComponent<EnemyBase>().dieAction += () => SpawnItem(1, temp.transform);

            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(2f);
    }

    IEnumerator Wave2()
    {
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                var temp = Instantiate(enemies[1], new Vector3(Random.Range(-4, 4), 6), Quaternion.identity);
                if (i == Random.Range(2, 6)) temp.GetComponent<EnemyBase>().dieAction += () => SpawnItem(1, temp.transform);

                yield return new WaitForSeconds(0.2f);
            }
        }

        yield return new WaitForSeconds(2f);
    }

    IEnumerator Wave3()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(enemies[1], new Vector3(-5, Random.Range(5f, -3f)), Quaternion.Euler(0, 0, Random.Range(80f, 100f)));
            Instantiate(enemies[1], new Vector3(5, Random.Range(5f, -3f)), Quaternion.Euler(0, 0, Random.Range(240f, 280f)));

            yield return new WaitForSeconds(0.8f);
        }
        yield return new WaitForSeconds(2f);
    }

    IEnumerator Wave4()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(enemies[2], new Vector3(-2.25f, 6f), Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(1.35f);

            var temp = Instantiate(enemies[2], new Vector3(2.54f, 6f), Quaternion.Euler(0, 0, 0));
            temp.GetComponent<EnemyBase>().dieAction += () => SpawnItem(1, temp.transform);
            for (int j = 0; j < 5; j++)
            {
                Instantiate(enemies[2], new Vector3(-5, Random.Range(5f, -2f)), Quaternion.Euler(0, 0, Random.Range(80f, 100f)));
                Instantiate(enemies[2], new Vector3(5, Random.Range(5f, -2f)), Quaternion.Euler(0, 0, Random.Range(240f, 280f)));

                yield return new WaitForSeconds(0.8f);
            }
        }

        yield return new WaitForSeconds(5f);
    }

    IEnumerator BossWave()
    {
        var boss = Instantiate(enemies[2]);
        yield return new WaitUntil(() => (boss as BossTram).isEnd);
        yield return new WaitForSeconds(5f);
    }

    void SpawnItem(int index, Transform transform)
    {
        Instantiate(items[index], transform.position, Quaternion.identity);
    }
}
