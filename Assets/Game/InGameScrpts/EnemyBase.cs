using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class EnemyBase : MonoBehaviour
{
    public float maxHp;
    public float hp = 1;

    public Action dieAction;

    InGameManager inGameManager;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        hp = maxHp;
        dieAction += DieDestroy;
    }

    public virtual void OnDamage(float dmg)
    {
        hp -= dmg;

        if (hp <= 0)
        {
            InGameManager.scoreGet = true;
            dieAction?.Invoke();
            Destroy(this.gameObject);
        }
    }

    protected abstract void DieDestroy();

}
