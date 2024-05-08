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
    private int SpawnWave = 0;
    private int enemyCount;
    private float xPos;
    private float zPos;
    public float sphereRadius = 20f;

    public Transform p2;
    public Transform p3;
    public Transform spawnIn;
    public GameObject boss;


    public void SpawnWaves()
    {
        if (SpawnWave == 1)
        {
            for (int i = 0; i < 3; i++)
                Spawn(enemyWeak);
        }


        if (SpawnWave == 2)
        {
            for (int i = 0; i < 3; i++)
                Spawn(enemyWeak);

            Spawn(enemyMid);
            Spawn(enemyMid);
            
            Spawn(enemyStr);
        }

        if (SpawnWave == 3)
        {
            for (int i = 0; i < 3; i++)
                Spawn(enemyWeak);

            for (int i = 0; i < 3; i++)
            {
                Spawn(enemyWeak);
            }
        }

        if (SpawnWave == 4)
        {
            boss.SetActive(true);
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
        xPos = Random.Range(-sphereRadius, sphereRadius);
        zPos = Random.Range(-sphereRadius, sphereRadius);
        if (Physics.CheckSphere(new Vector3(xPos, 0f, zPos), sphereRadius))
        {
            return;
        }
        else
        {
            randomizeSpawn();
        }
    }

    void Spawn(GameObject enemyType)
    {
        randomizeSpawn();
        Instantiate(enemyType, transform.position + new Vector3(xPos, 0f, zPos), Quaternion.identity);
    }

}

