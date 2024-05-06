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
    gameHandler gameScript;
    HighScoreTest hs;

    // Resources To Drop
    GameObject goldPrefab;
    GameObject clothPrefab;
    GameObject woodPrefab;
    GameObject ironPrefab;
    


    public int EnemyHealth;
    public int EnemyDefence;
    public int EnemyDamage;
    public EnemyType Etype;



    public static int enemiesKilled = 0;
    public static int scoreMultiplier = 1;
    public int Double = 2; 
    public int Triple = 5;
    float startTime;
    bool timerStarted = false;
    float timeFrame = 10f;
    float timeElapsed = 0f;
    float timeBetweenAttacks = 1f;
    bool alreadyAttacked = false;

    public float dashRange = 5f; // The maximum distance at which the enemy will dash towards the player
    public float dashSpeed = 5f; // The speed at which the enemy dashes towards the player



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
            EnemyDefence = 0;
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

        goldPrefab = gameScript.goldPrefab;
        clothPrefab = gameScript.clothPrefab;
        woodPrefab = gameScript.woodPrefab;
        ironPrefab = gameScript.ironPrefab;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerKeyboard"))
        {
            //if (collision.gameObject == gameScript.keyboardPlayer)
            //{
            if (!alreadyAttacked)
            {
                if (EnemyType.Weak == Etype)
                {
                    EnemyDamage = 2;
                    PlayerB player = collision.gameObject.GetComponent<PlayerB>();
                    gameScript.keyboardPlayerHealth -= EnemyDamage;
                }

                if (EnemyType.Medium == Etype)
                {
                    EnemyDamage = 5;
                    PlayerB player = collision.gameObject.GetComponent<PlayerB>();
                    gameScript.keyboardPlayerHealth -= EnemyDamage;
                }

                /*if (EnemyType.Heavy == Etype)
                {
                    EnemyDamage = 20;
                    PlayerB player = collision.gameObject.GetComponent<PlayerB>();
                    player.Player1Health -= Time.deltaTime * 20f;
                    Debug.Log("Player1 took " + EnemyDamage + " damage and only have " + player.Player1Health + " left.");
                }

                if (EnemyType.Boss == Etype)
                {
                    EnemyDamage = 30;
                    PlayerB player = collision.gameObject.GetComponent<PlayerB>();
                    player.Player1Health -= Time.deltaTime * 40f;
                    Debug.Log("Player1 took " + EnemyDamage + " damage and only have " + player.Player1Health + " left.");
                }*/

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
            //}
        if (collision.gameObject.CompareTag("PlayerController"))
        {
            if (!alreadyAttacked)
            {
                if (EnemyType.Weak == Etype)
                {
                    EnemyDamage = 2;
                    gameScript.controllerPlayerHealth -= EnemyDamage;
                }

                
                if (EnemyType.Medium == Etype)
                {
                    EnemyDamage = 5;
                    gameScript.controllerPlayerHealth -= EnemyDamage;
                }
                /*
                if (EnemyType.Heavy == Etype)
                {
                    EnemyDamage = 20;
                    PlayerB player = collision.gameObject.GetComponent<PlayerB>();
                    player.Player1Health -= Time.deltaTime * 10f;
                    Debug.Log("Player2 took " + EnemyDamage + " damage and only have " + player.Player2Health + " left.");
                }

                if (EnemyType.Boss == Etype)
                {
                    EnemyDamage = 30;
                    PlayerB player = collision.gameObject.GetComponent<PlayerB>();
                    player.Player2Health -= Time.deltaTime * 20f;
                    Debug.Log("Player2 took " + EnemyDamage + " damage and only have " + player.Player2Health + " left.");
                }*/
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

            if (timeElapsed <= timeFrame)
            {
                enemiesKilled++;


                if (enemiesKilled >= Triple)
                {
                    scoreMultiplier = 3;
                }
                else if (enemiesKilled >= Double)
                {
                    scoreMultiplier = 2;
                }
                else
                {
                    scoreMultiplier = 1;

                }

                int scoreToAdd = GetScore(Etype) * scoreMultiplier;
                hs.score += scoreToAdd;


                die();
            }
        }
        // Calculate the distance between the enemy and the player
        float distanceToKeyPlayer = Vector3.Distance(transform.position, gameScript.keyboardPlayer.transform.position);
        float distanceToControllerPlayer = Vector3.Distance(transform.position, gameScript.controllerPlayer.transform.position);
  
        if (Etype == EnemyType.Medium && distanceToKeyPlayer <= dashRange)
        {
            
            DashTowardsKeyPlayer();
        }
        if (Etype == EnemyType.Medium && distanceToControllerPlayer <= dashRange)
        {
            DashTowardsControllerPlayer();
        }

        // Check if time frame has elapsed and reset counters
        if (timerStarted)
        {
            if (timeElapsed > timeFrame)
                {
                    // Reset counters
                    enemiesKilled = 0;
                    timerStarted = false;
                    scoreMultiplier = 1;
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
        int numOfGold = Random.Range(3, 10);
        dropGold(numOfGold * gameScript.ResourceDropRate);

        // Decide how many and what items to drop
        float threshToDetermine = Random.value;
        
        if (threshToDetermine <= 0.45f) // Start with cloth ( 45% )
        {
            float clothCountThresh = Random.value;
            int numberOfCloth = clothCountThresh <= 0.1f ? 3 : (clothCountThresh <= 0.4f ? 2 : 1); // 10% for 3, 30% for 2, 60% for 1
            dropCloth(numberOfCloth * gameScript.ResourceDropRate);
        }

        else if (threshToDetermine <= 0.8f) // Start with wood ( 35% )
        {
            int numberOfWood = Random.value < 0.3f ? 2 : 1; // 40% for duplicate
            dropWood(numberOfWood * gameScript.ResourceDropRate);
        }
        
        else // Start with metal ( 20% )
        {
            int numberOfIron = Random.value < 0.1f ? 2 : 1; // 20% for duplicate
            dropIron(numberOfIron * gameScript.ResourceDropRate);
        }


        
    }



    void dropGold(int num)
    {
        for (int i = 0; i < num; i++)
        {                                  //       VVV   Change this
            GameObject currentCloth = Instantiate(goldPrefab, transform.position + Vector3.up + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
            currentCloth.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);
            currentCloth.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 22f;
        }
    }

    void dropCloth(int num)
    {
        for (int i = 0; i < num; i++)
        {                                  //       VVV   Change this
            GameObject currentCloth = Instantiate(clothPrefab, transform.position + Vector3.up + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
            currentCloth.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);
            currentCloth.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 22f;
        }
    }

    void dropWood(int num)
    {
        for (int i = 0; i < num; i++)
        {                                 //       VVV   Change this
            GameObject currentWood = Instantiate(woodPrefab, transform.position + Vector3.up + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
            currentWood.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);
            currentWood.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 22f;
        }
    }
    void dropIron(int num)
    {
        for (int i = 0; i < num; i++)
        {                                       //       VVV   Change this
            GameObject currentMetalScrap = Instantiate(ironPrefab, transform.position + Vector3.up + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
            currentMetalScrap.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);
            currentMetalScrap.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 22f;
        }
    }
    void DashTowardsKeyPlayer()
    {
        // Calculate the direction from the enemy to the keyboard player
        Vector3 direction = (gameScript.keyboardPlayer.transform.position - transform.position).normalized;


        transform.position += direction * dashSpeed;
    }

    void DashTowardsControllerPlayer()
    {
        // Calculate the direction from the enemy to the controller player
        Vector3 direction = (gameScript.controllerPlayer.transform.position - transform.position).normalized;

        // Move the enemy towards the controller player with increased speed for a short duration
        transform.position += direction * dashSpeed;
    }

}