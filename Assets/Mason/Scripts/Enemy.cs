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
    HighScoreTest hs;

    // Resources To Drop
    GameObject goldPrefab;
    GameObject clothPrefab;
    GameObject woodPrefab;
    GameObject ironPrefab;
    GameObject skull;
    


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
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();

        if (gameScript.GetComponent<HighScoreTest>())
            hs = gameScript.GetComponent<HighScoreTest>();

        if (GameObject.FindGameObjectWithTag("heatingUp"))
            HU = GameObject.FindGameObjectWithTag("heatingUp").GetComponent<heatingUp>();

        InputSystem.onDeviceChange += OnDeviceChange;
        foreach (var device in InputSystem.devices)
        {
            if (device is Gamepad)
            {
                gamepad = (Gamepad)device;
                //Debug.Log("Xbox controller connected!");
                break;
            }
        }

        if (EnemyType.Weak == Etype)
        {
            EnemyHealth = 10;
            EnemyDefence = 0;
        }
        if (EnemyType.Medium == Etype)
        {
            EnemyHealth = 15;
            EnemyDefence = 0;
        }
        if (EnemyType.Heavy == Etype)
        {
            EnemyHealth = 30;
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
    }

    public void Rumble(float intensity, float duration)
    {
        if (gamepad != null)
        {
            // Make the controller rumble
            gamepad.SetMotorSpeeds(intensity, intensity);
            StartCoroutine(StopRumble(duration));
        }
        else
        {
            Debug.LogWarning("No Xbox controller connected!");
        }
    }
    
    IEnumerator StopRumble(float duration)
    {
        yield return new WaitForSeconds(duration);
        // Stop the rumble after the specified duration
        gamepad.SetMotorSpeeds(0, 0);
    }
    void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (device is Gamepad && change == InputDeviceChange.Added && gamepad == null)
        {
            gamepad = (Gamepad)device;
            Debug.Log("Xbox controller connected!");
        }
        else if (device is Gamepad && change == InputDeviceChange.Removed && gamepad == device)
        {
            gamepad = null;
            Debug.Log("Xbox controller disconnected!");
        }
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
                    gameScript.keyboardPlayerHealth -= EnemyDamage;
                }

                if (EnemyType.Medium == Etype)
                {
                    EnemyDamage = 5;
                    gameScript.keyboardPlayerHealth -= EnemyDamage;
                }

                if (EnemyType.Heavy == Etype)
                {
                    EnemyDamage = 7;
                    gameScript.keyboardPlayerHealth -= EnemyDamage;
                }

                if (EnemyType.Boss == Etype)
                {
                    EnemyDamage = 10;
                    gameScript.keyboardPlayerHealth -= EnemyDamage;
                }

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
                    Rumble(0.3f, 0.3f);
                }
                
                if (EnemyType.Medium == Etype)
                {
                    EnemyDamage = 5;
                    gameScript.controllerPlayerHealth -= EnemyDamage;
                    Rumble(0.3f, 0.3f);
                }
                
                if (EnemyType.Heavy == Etype)
                {
                    EnemyDamage = 7;
                    gameScript.controllerPlayerHealth -= EnemyDamage;
                    Rumble(0.3f, 0.3f);
                }

                if (EnemyType.Boss == Etype)
                {
                    EnemyDamage = 10;
                    gameScript.controllerPlayerHealth -= EnemyDamage;
                    Rumble(0.3f, 0.3f);
                }
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
    }

    public void ResetAttack()
    {
        alreadyAttacked = false;
        StopRumble(0.3f);
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
            StopRumble(0.3f);
        }

        if (gameScript.controllerPlayerHealth <= 0f){
            StopRumble(0.3f);
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
        StopRumble(0.3f);
    }


    void dropItems()
    {
        int numOfGold = Random.Range(3, 10);
        if (EnemyType.Boss == Etype)
            numOfGold *= 20;
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

        // Drop SKULL

        GameObject currentMetalScrap = Instantiate(skull, transform.position + Vector3.up + Random.insideUnitSphere, Random.rotation, gameScript.ResourceTransform);
        float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
        currentMetalScrap.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);

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

    
}