using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

public enum EnemyType
{
    Weak,
    Medium,
    Fast,
    Heavy,
    Boss,
}

public class Enemy : MonoBehaviour
{
    gameHandler gameScript;
    GameObject hs_trigger;
    HighScoreTest hs;

    // Resources To Drop
    GameObject goldPrefab;
    GameObject clothPrefab;
    GameObject woodPrefab;
    GameObject ironPrefab;
    GameObject skull;
    GameObject featherPrefab;
    


    public int EnemyHealth;
    public int EnemyDefence;
    public int EnemyDamage;
    public EnemyType Etype;
    public ParticleSystem deathEffect;
    public heatingUp HU;


    float timeBetweenAttacks = 1f;
    bool alreadyAttacked = false;

    private const string playerKScoreTextObjectName = "ScoreK";
    private const string playerCScoreTextObjectName = "ScoreC";
    private Gamepad gamepad;



    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();

            if (gameScript.GetComponent<HighScoreTest>())
                hs = gameScript.GetComponent<HighScoreTest>();

            if (GameObject.FindGameObjectWithTag("heatingUp"))
                HU = GameObject.FindGameObjectWithTag("heatingUp").GetComponent<heatingUp>();
        }
        if (SceneManager.GetActiveScene().name != "Tutorial" && SceneManager.GetActiveScene().name != "startScreen")
        {
            gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();

            hs_trigger = GameObject.FindGameObjectWithTag("HighScore");
            if (hs_trigger.GetComponent<HighScoreTest>())
                hs = hs_trigger.GetComponent<HighScoreTest>();

            if (GameObject.FindGameObjectWithTag("heatingUp"))
                HU = GameObject.FindGameObjectWithTag("heatingUp").GetComponent<heatingUp>();
        }
        if (EnemyType.Weak == Etype)
        {
            EnemyHealth = 12;
            EnemyDefence = 0;
        }
        if (EnemyType.Medium == Etype)
        {
            EnemyHealth = 18;
            EnemyDefence = 0;
        }
        if (EnemyType.Heavy == Etype)
        {
            EnemyHealth = 32;
            EnemyDefence = 0;
        }
        if (EnemyType.Boss == Etype)
        {
            EnemyHealth = 250;
            EnemyDefence = 2;
        }

        goldPrefab = gameScript.goldPrefab;
        clothPrefab = gameScript.clothPrefab;
        woodPrefab = gameScript.woodPrefab;
        ironPrefab = gameScript.ironPrefab;
        skull = gameScript.skullPrefab;
        featherPrefab = gameScript.FeatherPrefab;
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
                    gameScript.keyboardPlayerHealth -= 2 * gameScript.playerKDmgMult;

                if (EnemyType.Medium == Etype)
                    gameScript.keyboardPlayerHealth -= 5 * gameScript.playerKDmgMult;

                if (EnemyType.Heavy == Etype)
                    gameScript.keyboardPlayerHealth -= 7 * gameScript.playerKDmgMult;

                if (EnemyType.Boss == Etype)
                    gameScript.keyboardPlayerHealth -= 10 * gameScript.playerKDmgMult;

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }

        if (collision.gameObject.CompareTag("PlayerController"))
        {
            if (!alreadyAttacked)
            {
                if (EnemyType.Weak == Etype)
                    gameScript.controllerPlayerHealth -= 2 * gameScript.playerCDmgMult;
                
                if (EnemyType.Medium == Etype)
                    gameScript.controllerPlayerHealth -= 5 * gameScript.playerCDmgMult;
                
                if (EnemyType.Heavy == Etype)
                    gameScript.controllerPlayerHealth -= 7 * gameScript.playerCDmgMult;

                if (EnemyType.Boss == Etype)
                    gameScript.controllerPlayerHealth -= 10 * gameScript.playerCDmgMult;

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
    }

    public void ResetAttack()
    {
        alreadyAttacked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyHealth <= 0)
        {
            if (HU != null)
                HU.AddKill();

            TMPro.TextMeshProUGUI playerKScoreText = GetPlayerScoreText();
            TMPro.TextMeshProUGUI playerCScoreText = GetPlayerScoreText2();
            int scoreToAdd = GetScore(Etype);
            
            if (HU != null)
            {
                hs.score += scoreToAdd * HU.scoreMultiplier;
                playerKScoreText.text = "Score: " + hs.score.ToString();
                playerCScoreText.text = "Score: " + hs.score.ToString();
            }
            


            Instantiate(deathEffect, transform.position + Vector3.up + Random.insideUnitSphere, Quaternion.identity);
            die();
        }

        if (gameScript.controllerPlayerHealth <= 0f){
        }

    }

    public TMPro.TextMeshProUGUI GetPlayerScoreText()
    {
        GameObject textObject = GameObject.Find(playerKScoreTextObjectName); // Find the GameObject by name
        if (textObject != null)
        {
            return textObject.GetComponentInChildren<TMPro.TextMeshProUGUI>(); // Get TextMeshProUGUI component
        }
        else
        {
            Debug.LogWarning("Player score text object not found with name: " + playerKScoreTextObjectName);
            return null;
        }
        
    }

    public TMPro.TextMeshProUGUI GetPlayerScoreText2()
    {
        GameObject textObject2 = GameObject.Find(playerCScoreTextObjectName); // Find the GameObject by name
        if (textObject2 != null)
        {
            return textObject2.GetComponentInChildren<TMPro.TextMeshProUGUI>(); // Get TextMeshProUGUI component
        }
        else
        {
            Debug.LogWarning("Player score text object not found with name: " + playerCScoreTextObjectName);
            return null;
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
        if (EnemyType.Boss == Etype)
        {
            gameScript.WaitAndChangeScene();
        }
        Destroy(gameObject);
    }


    void dropItems()
    {
        int numOfGold = Random.Range(3, 10);
        if (EnemyType.Boss == Etype)
            numOfGold *= 50;
        dropGold(numOfGold * gameScript.ResourceDropRate);

        // Decide how many and what items to drop
        float threshToDetermine = Random.value;
        float clothCountThresh = Random.value;
        int numberOfCloth = clothCountThresh <= 0.333f ? 6 : 3; // 40% for 3, 60% for 2
        dropCloth(numberOfCloth * gameScript.ResourceDropRate);
        float featherCountThresh = Random.value;
        int numberOfFeathers = 0;
        if (EnemyType.Heavy == Etype)
        {
            numberOfFeathers = 3;
            dropFeathers(numberOfFeathers * gameScript.ResourceDropRate);
        }



            // Drop SKULL

            GameObject enemySkull = Instantiate(skull, transform.position + Vector3.up + Random.insideUnitSphere, Random.rotation, gameScript.ResourceTransform);
        if (EnemyType.Boss == Etype) enemySkull.transform.localScale *= 2f;
        float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
        enemySkull.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);

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
    void dropFeathers(int num)
    {
        for (int i = 0; i < num; i++)
        {                                  //       VVV   Change this
            GameObject currentFeather = Instantiate(featherPrefab, transform.position + Vector3.up + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
            currentFeather.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);
            currentFeather.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 22f;
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
}