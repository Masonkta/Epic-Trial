using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomizedSpawner : MonoBehaviour
{

    Enemy enemy;
    public GameObject enemyWeak;
    public GameObject enemyMid;
    public GameObject enemyStr;
    public GameObject Spawner;
    private int SpawnWave = 0;
    private bool allowedToSpawn;
    private int enemyCount;
    private float xPos;
    private float zPos;
    private int sphereRadius = 5;

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
                randomizeSpawn();
                Instantiate(enemyWeak, new Vector3(xPos * 230, 2.0f, zPos * 230), Quaternion.identity);
            }
        }
        if (SpawnWave == 2)
        {
            for (int i = 0; i < 3; i++)
            {
                randomizeSpawn();
                Instantiate(enemyWeak, new Vector3(xPos * 230, 2.0f, zPos * 230), Quaternion.identity);
            }
            randomizeSpawn();
            Instantiate(enemyMid, new Vector3(xPos * 230, 2.0f, zPos * 230), Quaternion.identity);
        }
        if (SpawnWave == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                randomizeSpawn();
                Instantiate(enemyWeak, new Vector3(xPos * 230, 2.0f, zPos * 230), Quaternion.identity);
            }
            for (int i = 0; i < 3; i++)
            {
                randomizeSpawn();
                Instantiate(enemyMid, new Vector3(xPos * 230, 2.0f, zPos * 230), Quaternion.identity);
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

    void randomizeSpawn()
    {
        xPos = Random.Range(-1.0f, 1.0f);
        zPos = Random.Range(-1.0f, 1.0f);
        if (Physics.CheckSphere(new Vector3(xPos * 230, 2.0f, zPos * 230), sphereRadius))
        {
            return;
        }
        else
        {
            randomizeSpawn();
        }
    }

}

