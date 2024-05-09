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
    public int SpawnWave = 0;
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
            for (int i = 0; i < 25; i++)
                Spawn(enemyWeak);
        }


        if (SpawnWave == 2)
        {
            for (int i = 0; i < 5; i++)
                Spawn(enemyWeak);

            Spawn(enemyMid);
            Spawn(enemyMid);
            
            Spawn(enemyStr);
        }

        if (SpawnWave == 3)
        {
            for (int i = 0; i < 7; i++)
                Spawn(enemyMid);

            for (int i = 0; i < 4; i++)
            {
                Spawn(enemyStr);
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

        if (enemyCount == 0)
        {
            SpawnWave++;
            SpawnWaves();
        }

        // K kills all enemies
        if (Input.GetKeyDown(KeyCode.K))
            foreach(Enemy t in FindObjectsOfType<Enemy>())
                t.GetComponent<Enemy>().EnemyHealth = 0;
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
        Instantiate(enemyType, transform.position + new Vector3(xPos, 0f, zPos), Quaternion.identity, spawnIn);
        Instantiate(enemyType, p2.position + new Vector3(xPos, 0f, zPos), Quaternion.identity, spawnIn);
        Instantiate(enemyType, p3.position + new Vector3(xPos, 0f, zPos), Quaternion.identity, spawnIn);
    }

}

