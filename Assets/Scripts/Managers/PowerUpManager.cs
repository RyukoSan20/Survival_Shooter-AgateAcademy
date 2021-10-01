using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float spawnTime = 7f;
    public Transform spawnPointContainer;
    public List<Transform> spawnPoints = new List<Transform>();

    [SerializeField] private MonoBehaviour factory;
    IFactory Factory => factory as IFactory;

    private void Start()
    {
        spawnPoints.AddRange(spawnPointContainer.GetComponentsInChildren<Transform>());
        InvokeRepeating(nameof(Spawn), spawnTime, spawnTime);
    }


    private void Spawn()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        var spawnPowerUp = Random.Range(0, 3);
        var spawnPointIndex = Random.Range(0, spawnPoints.Count);
        
        Factory.FactoryMethod(spawnPowerUp, spawnPoints[spawnPointIndex]);
    }

    public void SpawnOnDeath(Transform enemyPosition)
    {
        var spawnPowerUp = Random.Range(0, 3);
        
        Factory.FactoryMethod(spawnPowerUp, enemyPosition);
    }
}
