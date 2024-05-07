using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JustSpawnLol : MonoBehaviour
{

    Enemy enemy;
    public GameObject enemyWeak;
    public GameObject enemyMid;
    public GameObject enemyStr;
    public GameObject Spawner;
    private int SpawnWave = 0;
    private bool allowedToSpawn;
    private int enemyCount;

    /*
    IEnumerator SpawnIt()
    {
        yield return new WaitForSeconds(5f);
        if (allowedToSpawn == true)
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(enemyBoi, Spawner.transform);
                yield return new WaitForSeconds(5f);
            }
            allowedToSpawn = false;
        }
    }
    */
    public void SpawnWaves()
    {
        if (SpawnWave == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(enemyWeak, Spawner.transform);
            }
        }
        if (SpawnWave == 2)
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(enemyWeak, Spawner.transform);
            }
            Instantiate(enemyMid, Spawner.transform);
        }
        if (SpawnWave == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(enemyWeak, Spawner.transform);
            }
            for (int i = 0; i < 3; i++)
            {
                Instantiate(enemyMid, Spawner.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        Debug.Log(enemyCount);
        if (enemyCount == 0)
        {
            SpawnWave++;
            SpawnWaves();
        }
    }

}

