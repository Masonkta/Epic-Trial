using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum EnemyType
{
    Weak,
    Medium,
    Heavy,
    Boss,
}

public class Enemy : MonoBehaviour
{
    public int EnemyHealth;
    public int EnemyDefence;
    public int EnemyDamage;
    HighScoreTest hs;
    public EnemyType Etype;

    public static int enemiesKilled = 0;
    public static int scoreMultiplier = 1;
    public int Double = 2; 
    public int Triple = 5;
    float startTime;
    bool timerStarted = false;
    float timeFrame = 10f;
    float timeElapsed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        hs = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<HighScoreTest>();

        if (EnemyType.Weak == Etype)
        {
            EnemyHealth = 10;
            EnemyDefence = 0;
        }
        if (EnemyType.Medium == Etype)
        {
            EnemyHealth = 20;
            EnemyDefence = 5;
        }
        if (EnemyType.Heavy == Etype)
        {
            EnemyHealth = 30;
            EnemyDefence = 10;
        }
        if (EnemyType.Boss == Etype)
        {
            EnemyHealth = 100;
            EnemyDefence = 20;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           if (EnemyType.Weak == Etype)
            {
                EnemyDamage = 2;
                PlayerB player = collision.gameObject.GetComponent<PlayerB>();
                player.PlayerHealth -= EnemyDamage;
                Debug.Log(Etype.ToString() + " Dealt " + EnemyDamage + " damage to the Player");
            }

            if (EnemyType.Medium == Etype)
            {
                EnemyDamage = 10;
                PlayerB player = collision.gameObject.GetComponent<PlayerB>();
                player.PlayerHealth -= EnemyDamage;
                Debug.Log(Etype.ToString() + " Dealt " + EnemyDamage + " damage to the Player");
            }

            if (EnemyType.Heavy == Etype)
            {
                EnemyDamage = 20;
                PlayerB player = collision.gameObject.GetComponent<PlayerB>();
                player.PlayerHealth -= EnemyDamage;
                Debug.Log(Etype.ToString() + " Dealt " + EnemyDamage + " damage to the Player");
            }

            if (EnemyType.Boss == Etype)
            {
                EnemyDamage = 30;
                PlayerB player = collision.gameObject.GetComponent<PlayerB>();
                player.PlayerHealth -= EnemyDamage;
                Debug.Log(Etype.ToString() + " Dealt " + EnemyDamage + " damage to the Player");
            }
        }
    }

    public void timer()
    {
        startTime = Time.time;
        timerStarted = true;

    }
    // Update is called once per frame
    void Update()
    {
        if (EnemyHealth <= 0)
        {
            if (!timerStarted)
            {
                timer();
            }
            timeElapsed = Time.time - startTime;
            Debug.Log(timeElapsed);

            if (timeElapsed <= timeFrame)
            {
                enemiesKilled++;
                Debug.Log(enemiesKilled);


                if (enemiesKilled >= Triple)
                {
                    scoreMultiplier = 3;
                    Debug.Log("Mulitplier 3x");
                }
                else if (enemiesKilled >= Double)
                {
                    scoreMultiplier = 2;
                    Debug.Log("Mulitplier 2x");
                }
                else
                {
                    scoreMultiplier = 1;
                    Debug.Log("Mulitplier 1x");

                }

                int scoreToAdd = GetScore(Etype) * scoreMultiplier;
                Debug.Log("Gained " + scoreToAdd + " points");
                hs.score += scoreToAdd;

                Debug.Log("Current Score is " + hs.score);
                Destroy(gameObject);
            }
        }
        // Check if time frame has elapsed and reset counters
        if (timerStarted)
        {
            Debug.Log("started");
            Debug.Log(timeElapsed);
            if (timeElapsed > timeFrame)
                {
                    // Reset counters
                    enemiesKilled = 0;
                    timerStarted = false;
                    scoreMultiplier = 1;
                    Debug.Log("Mulitplier 1x");
                }
            }
    }

    // Method to get score based on enemy type
    int GetScore(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Weak:
                return 10;
            case EnemyType.Medium:
                return 100;
            case EnemyType.Heavy:
                return 1000;
            case EnemyType.Boss:
                return 10000;
            default:
                return 10;
        }
    }
}