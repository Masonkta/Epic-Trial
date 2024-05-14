using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubPickup : MonoBehaviour
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
        if (keyboardWeapons.readyToGrab)
        {
            if (Vector3.Distance(transform.position, keyboardPlayer.transform.position) < 4f)
            {
                if (keyboardWeapons.handOpen()) // Keyboard Dude Not Holding Sword Yet
                {
                    keyboardPlayer.GetComponent<SphereCollider>().radius *= 1.4f;
                    keyboardWeapons.getClub().SetActive(true);
                    Destroy(gameObject); // Destroy Club object on the ground
                }
            }
        }
        
    }

    void checkController()
    {
        if (controllerWeapons.readyToGrab)
        {
            if (Vector3.Distance(transform.position, controllerPlayer.transform.position) < 4f)
            {
                if (controllerWeapons.handOpen()) // Controller Dude Not Holding Weapons Yet
                {
                    controllerPlayer.GetComponent<SphereCollider>().radius *= 1.4f;
                    controllerWeapons.getClub().SetActive(true);
                    Destroy(gameObject); // Destroy Club object on the ground
                }
            }
        }

    }
}
