using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float spawnTime = 15f;

    private float _existTime;

    private void Start()
    {
        _existTime = 0f;
    }

    private void FixedUpdate()
    {
        _existTime += Time.deltaTime;

        if (_existTime >= spawnTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            ApplyPowerUp(other.gameObject);
            Destroy(gameObject);
        }
    }
    
    protected virtual void ApplyPowerUp(GameObject target) {}
}
