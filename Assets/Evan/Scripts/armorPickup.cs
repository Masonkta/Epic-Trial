using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armorPickup : MonoBehaviour
{
    public gameHandler gameScript;

    float timeOfSpawn;
    float pickupDist = 6.5f;
    float pickupTimer = 2f;

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
        if (!gameScript.keyboardPlayerPoisonPotion)
        {
            if (Vector3.Distance(transform.position + Vector3.up, gameScript.keyboardPlayer.transform.position) < pickupDist)
            {
                if (!gameScript.playerKHasArmor)
                {
                    gameScript.turnOnArmorK();
                    Destroy(gameObject);
                }
            }
        }
        // Controller
        if (!gameScript.keyboardPlayerPoisonPotion)
        {
            if (Vector3.Distance(transform.position + Vector3.up, gameScript.controllerPlayer.transform.position) < pickupDist)
            {
                if (!gameScript.playerCHasArmor)
                {
                    gameScript.turnOnArmorC();
                    Destroy(gameObject);
                }
            }
        }
    }

}