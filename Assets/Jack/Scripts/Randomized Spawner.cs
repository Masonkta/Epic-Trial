using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI enemyCounterK;
    public TextMeshProUGUI enemyCounterC;
    private float xPos;
    private float zPos;
    public float sphereRadius = 20f;

    public Transform p2;
    public Transform p3;
    public Transform spawnIn;
    public GameObject bossObj;
    public GameObject SBOSS;

    public bool instaKill = true;


    public bool bossGravityEnabled = false;
    public float bossSpawnedAtTime;
    public float timerForBoss;

    public void SpawnWaves()
    {
        if (SpawnWave == 1)
        {
            for (int i = 0; i < 6; i++)
                Spawn(enemyWeak);
        }


        if (SpawnWave == 2)
        {
            for (int i = 0; i < 2; i++)
                Spawn(enemyWeak);

            for (int i = 0; i < 3; i++)
                Spawn(enemyMid);
        }

        if (SpawnWave == 3)
        {
            for (int i = 0; i < 3; i++)
                Spawn(enemyMid);

            for (int i = 0; i < 4; i++)
                Spawn(enemyStr);
        }


        if (SpawnWave == 4)
        {
            bossObj.SetActive(true);
            bossSpawnedAtTime = Time.time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        // Update Enemy Counter UI's
        if (SpawnWave < 4)
        {
            enemyCounterK.text = $"Wave {SpawnWave}/3\n\nEnemies\nAlive: {enemyCount}";
            enemyCounterC.text = $"Wave {SpawnWave}/3\n\nEnemies\nAlive: {enemyCount}";
        }
        if (SpawnWave == 4)
        {
            enemyCounterK.text = "Wave *";
            enemyCounterC.text = "Wave *";
            // Madi enable image of skull here
        }

        if (enemyCount == 0)
        {
            SpawnWave++;
            SpawnWaves();
        }

        // K kills all enemies
        if (Input.GetKeyDown(KeyCode.K) && Input.GetKey(KeyCode.O) && instaKill)
            foreach(Enemy t in FindObjectsOfType<Enemy>())
                t.GetComponent<Enemy>().EnemyHealth = 0;

        if (bossObj.activeInHierarchy)
        {
            if (!bossGravityEnabled)
            {
                if (Time.time > bossSpawnedAtTime + timerForBoss)
                {
                    SBOSS.GetComponent<Rigidbody>().useGravity = true;
                    bossGravityEnabled = true;
                }
            }
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
        Instantiate(enemyType, transform.position + new Vector3(xPos, 0f, zPos), Quaternion.identity, spawnIn);
        Instantiate(enemyType, p2.position + new Vector3(xPos, 0f, zPos), Quaternion.identity, spawnIn);
        Instantiate(enemyType, p3.position + new Vector3(xPos, 0f, zPos), Quaternion.identity, spawnIn);
    }

}

