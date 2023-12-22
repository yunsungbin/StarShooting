using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelHeal : itemBase
{
    protected override void TriggerEvent(Collider2D collision)
    {
        collision.GetComponent<PlayerControl>().FuelRecover();
    }
}
