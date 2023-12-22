using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StageBase : MonoBehaviour
{
    public EnemyBase[] enemies;
    public itemBase[] items;

    protected List<Func<IEnumerator>> waveList = new List<Func<IEnumerator>>();
    public IEnumerator StageRoutine()
    {
        foreach (var item in waveList)
        {
            yield return StartCoroutine(item?.Invoke());
        }
        yield break;
    }
}
