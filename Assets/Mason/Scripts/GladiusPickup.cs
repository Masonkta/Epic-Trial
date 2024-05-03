using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GladiusPickup : MonoBehaviour
{
    public gameHandler gameScript;

    public GameObject keyboardPlayer;
    public playerAccessWeapons keyboardWeapons;

    public GameObject controllerPlayer;
    public playerAccessWeapons controllerWeapons;

    // Start is called before the first frame update
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();


        keyboardPlayer = gameScript.keyboardPlayer;
        keyboardWeapons = keyboardPlayer.GetComponent<playerAccessWeapons>();

        controllerPlayer = gameScript.controllerPlayer;
        controllerWeapons = controllerPlayer.GetComponent<playerAccessWeapons>();
    }


    // Update is called once per frame
    void Update()
    {
        checkKeyboard();
        checkController();
    }

    void checkKeyboard()
    {
        if (Vector3.Distance(transform.position, keyboardPlayer.transform.position) < 4f)
        {
            if (keyboardWeapons.handOpen()) // Keyboard Dude Not Holding Sword Yet
            {
                print("Player 1 picked up Gladius.");
                keyboardWeapons.getGladius().SetActive(true);
                Destroy(gameObject); // Destroy Gladius object on the ground
            }
        }
    }

    void checkController()
    {
        if (Vector3.Distance(transform.position, controllerPlayer.transform.position) < 4f)
        {
            if (controllerWeapons.handOpen()) // Controller Dude Not Holding Weapons Yet
            {
                print("Player 2 picked up Gladius.");
                controllerWeapons.getGladius().SetActive(true);
                Destroy(gameObject); // Destroy Gladius object on the ground
            }
        }
    }
}
