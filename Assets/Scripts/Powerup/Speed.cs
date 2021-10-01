using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : PowerUp
{
    public float speedUpAmount = 1.2f;
    public float speedUpTime = 10f;

    protected override void ApplyPowerUp(GameObject target)
    {
        target.GetComponent<PlayerMovement>().SpeedUp(speedUpAmount, speedUpTime);
    }
}
