using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bandageScript : MonoBehaviour
{
    public float healthRestored;
    public gameHandler gameScript;
    public List<GameObject> players = new List<GameObject>();

    float timeOfSpawn;
    float pickupDist = 3.5f;
    float pickupTimer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();

        players.Add(gameScript.keyboardPlayer);
        players.Add(gameScript.controllerPlayer);

    }

    // Update is called once per frame
    void Update()
    {
        float timeOnMap = Time.time - timeOfSpawn;

        if (timeOnMap > pickupTimer)
            checkWithinRangeOfBothPlayers();


    }

    void checkWithinRangeOfBothPlayers()
    {
        // Keyboard
        if (Vector3.Distance(transform.position + Vector3.up, gameScript.keyboardPlayer.transform.position) < pickupDist && gameScript.keyboardPlayerHealth < 100f)
        {
            gameScript.keyboardPlayerHealth += healthRestored;
            if (gameScript.keyboardPlayerHealth > 100f)
                gameScript.keyboardPlayerHealth = 100f;
            Destroy(gameObject);
        }

        // Controller
        if (Vector3.Distance(transform.position + Vector3.up, gameScript.controllerPlayer.transform.position) < pickupDist && gameScript.controllerPlayerHealth < 100f)
        {
            gameScript.controllerPlayerHealth += healthRestored;
            if (gameScript.controllerPlayerHealth > 100f)
                gameScript.controllerPlayerHealth = 100f;
            Destroy(gameObject);
        }
    }

}