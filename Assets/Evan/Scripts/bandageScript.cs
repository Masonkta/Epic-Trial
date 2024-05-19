using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bandageScript : MonoBehaviour
{
    public float healthRestored;
    public gameHandler gameScript;

    float timeOfSpawn;
    float pickupDist = 4.5f;
    float pickupTimer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();

        timeOfSpawn = Time.time;

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
        if (Vector3.Distance(transform.position + Vector3.up, gameScript.keyboardPlayer.transform.position) < pickupDist)
        {
            gameScript.collectKeyboardBandages();
            Destroy(gameObject);
        }

        // Controller
        if (Vector3.Distance(transform.position + Vector3.up, gameScript.controllerPlayer.transform.position) < pickupDist)
        {
            gameScript.collectControllerBandages();
            Destroy(gameObject);
        }
    }

}