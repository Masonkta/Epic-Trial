using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GladiusPickup : MonoBehaviour
{
    public gameHandler gameScript;

    public GameObject keyboardPlayer;
    public GameObject keyboardGladius;
    public GameObject controllerPlayer;
    public GameObject controllerGladius;

    // Start is called before the first frame update
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();

        keyboardPlayer = gameScript.keyboardPlayer;
        keyboardGladius = keyboardPlayer.GetComponent<playerAccessWeapons>().getGladius();

        controllerPlayer = gameScript.controllerPlayer;
        controllerGladius = controllerPlayer.GetComponent<playerAccessWeapons>().getGladius();


    }


    // Update is called once per frame
    void Update()
    {
        checkPK();
        checkPC();
    }

    void checkPK()
    {
        if (Vector3.Distance(transform.position, keyboardPlayer.transform.position) < 4f)
        {
            if (!keyboardGladius.activeInHierarchy) // Keyboard Dude Not Holding Sword Yet
            {
                print("Player 1 picked up Gladius.");
                keyboardGladius.SetActive(true);
                Destroy(gameObject); // Destroy Gladius object on the ground
            }
        }
    }

    void checkPC()
    {
        if (Vector3.Distance(transform.position, controllerPlayer.transform.position) < 4f)
        {
            if (!controllerGladius.activeInHierarchy) // Controller Dude Not Holding Sword Yet
            {
                print("Player 2 picked up Gladius.");
                controllerGladius.SetActive(true);
                Destroy(gameObject); // Destroy Gladius object on the ground
            }
        }
    }
}
