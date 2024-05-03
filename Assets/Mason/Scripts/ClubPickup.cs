using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubPickup : MonoBehaviour
{
    public gameHandler gameScript;

    public GameObject keyboardPlayer;
    public GameObject keyboardClub;
    public GameObject controllerPlayer;
    public GameObject controllerClub;

    // Start is called before the first frame update
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();

        keyboardPlayer = gameScript.keyboardPlayer;
        keyboardClub = keyboardPlayer.GetComponent<playerAccessWeapons>().getClub();

        controllerPlayer = gameScript.controllerPlayer;
        controllerClub = controllerPlayer.GetComponent<playerAccessWeapons>().getClub();


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
            if (!keyboardClub.activeInHierarchy) // Keyboard Dude Not Holding Sword Yet
            {
                print("Player 1 picked up Gladius.");
                keyboardClub.SetActive(true);
                Destroy(gameObject); // Destroy Gladius object on the ground
            }
        }
    }

    void checkPC()
    {
        if (Vector3.Distance(transform.position, controllerPlayer.transform.position) < 4f)
        {
            if (!controllerClub.activeInHierarchy) // Controller Dude Not Holding Sword Yet
            {
                print("Player 2 picked up Gladius.");
                controllerClub.SetActive(true);
                Destroy(gameObject); // Destroy Gladius object on the ground
            }
        }
    }
}
