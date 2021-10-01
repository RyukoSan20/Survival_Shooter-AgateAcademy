using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerUp
{
    public int healAmount = 20;

    protected override void ApplyPowerUp(GameObject target)
    {
        target.GetComponent<PlayerHealth>().Heal(healAmount);
    }
}
