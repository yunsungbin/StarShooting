using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemHeal : itemBase
{
    protected override void TriggerEvent(Collider2D collision)
    {
        collision.GetComponent<PlayerControl>().HpRecover();
    }
}
