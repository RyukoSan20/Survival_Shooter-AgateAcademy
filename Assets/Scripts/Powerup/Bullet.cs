using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PowerUp
{
    public int refillAmount = 30;
    protected override void ApplyPowerUp(GameObject target)
    {
        target.GetComponentInChildren<PlayerShooting>().UpdateBullet(refillAmount);
    }
}
