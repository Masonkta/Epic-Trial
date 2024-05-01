using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

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
    public EnemyType Etype;
    GameObject clothPiece;
    GameObject woodPiece;
    GameObject metalScrap;

    gameHandler gameScript;
    HighScoreTest hs;

    public static int enemiesKilled = 0;
    public static int scoreMultiplier = 1;
    public int Double = 2; 
    public int Triple = 5;
    float startTime;
    bool timerStarted = false;
    float timeFrame = 10f;
    float timeElapsed = 0f;
    float timeBetweenAttacks;
    bool alreadyAttacked = false;


    // Start is called before the first frame update
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
        hs = gameScript.GetComponent<HighScoreTest>();

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

        clothPiece = gameScript.clothPiece;
        woodPiece = gameScript.woodPiece;
        metalScrap = gameScript.metalScrap;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!alreadyAttacked)
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
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
    }

    public void ResetAttack()
    {
        alreadyAttacked = false;
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

                die();
            }
        }
        // Check if time frame has elapsed and reset counters
        if (timerStarted)
        {
            Debug.Log("started");
            //Debug.Log(timeElapsed);
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

    void die()
    {
        dropItems();

        Destroy(gameObject);
    }


    void dropItems()
    {
        bool dropItem = Random.value < 0.75f;

        // Decide how many and what items to drop
        float threshToDetermine = Random.value;
        
        if (threshToDetermine <= 0.45f) // Start with cloth ( 45% )
        {
            float clothCountThresh = Random.value;
            int numberOfCloth = clothCountThresh <= 0.1f ? 3 : (clothCountThresh <= 0.4f ? 2 : 1); // 10% for 3, 30% for 2, 60% for 1
            dropCloth(numberOfCloth * gameScript.resourceDropRate);
        }

        else if (threshToDetermine <= 0.8f) // Start with wood ( 35% )
        {
            int numberOfWood = Random.value < 0.3f ? 2 : 1; // 40% for duplicate
            dropWood(numberOfWood * gameScript.resourceDropRate);
        }
        
        else // Start with wood ( 20% )
        {
            int numberOfMetalScraps = Random.value < 0.1f ? 2 : 1; // 20% for duplicate
            dropMetalScrap(numberOfMetalScraps * gameScript.resourceDropRate);
        }
        
    }



    void dropCloth(int num)
    {
        // Spawn in Cloth Pieces
        for (int i = 0; i < num; i++)
        {
            GameObject currentCloth = Instantiate(clothPiece, transform.position + Vector3.up, Quaternion.identity);
            float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
            currentCloth.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);
            currentCloth.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 22f;
        }
    }

    void dropWood(int num)
    {
        // Spawn in Wood Pieces
        for (int i = 0; i < num; i++)
        {
            GameObject currentWood = Instantiate(woodPiece, transform.position + Vector3.up, Quaternion.identity);
            float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
            currentWood.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);
            currentWood.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 22f;
        }
    }
    void dropMetalScrap(int num)
    {
        // Spawn in Wood Pieces
        for (int i = 0; i < num; i++)
        {
            GameObject currentMetalScrap = Instantiate(metalScrap, transform.position + Vector3.up, Quaternion.identity);
            float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
            currentMetalScrap.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);
            currentMetalScrap.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 22f;
        }
    }

}