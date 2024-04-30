using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

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
    HighScoreTest hs;
    public EnemyType Etype;

    public static int enemiesKilled = 0;
    public static int scoreMultiplier = 1;
    public int DoubleScore = 2; 
    public int TripleScore = 5;
    public float timeFrame = 30f;

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



    // Update is called once per frame
    void Update()
    {
        if (EnemyHealth <= 0)
        {

            enemiesKilled++;


            if (enemiesKilled >= TripleScore)
            {
                scoreMultiplier = 3;
                Debug.Log("Mulitplier 3x");
            }
            else if (enemiesKilled >= DoubleScore)
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