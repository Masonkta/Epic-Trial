using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GladiusPickup : MonoBehaviour
{
    public GameObject keyboardPlayer;
    public GameObject keyboardGladius;
    public GameObject controllerPlayer;
    public GameObject controllerGladius;

    // Start is called before the first frame update
    void Start()
    {
        
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
            print("Player 1 picked up Gladius.");
            keyboardGladius.SetActive(true);
            Destroy(gameObject); // Destroy Gladius object on the ground
        }
    }

    void checkPC()
    {
        if (Vector3.Distance(transform.position, controllerPlayer.transform.position) < 4f)
        {
            print("Player 2 picked up Gladius.");
            controllerGladius.SetActive(true);
            Destroy(gameObject); // Destroy Gladius object on the ground
        }
    }
}
