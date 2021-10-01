using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFactory : MonoBehaviour, IFactory
{
    public List<GameObject> powerUps;

    public GameObject FactoryMethod(int index, Transform spawnPoint)
    {
        GameObject powerUp = Instantiate(powerUps[index], spawnPoint.position, powerUps[index].transform.rotation);
        return powerUp;
    }
}
