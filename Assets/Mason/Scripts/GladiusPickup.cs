using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GladiusPickup : MonoBehaviour
{
    public GameObject p1;
    public GameObject p1Sword;
    public GameObject p2;
    public GameObject p2Sword;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkP1();
        checkP2();
    }

    void checkP1()
    {
        if (Vector3.Distance(transform.position, p1.transform.position) < 4f)
        {
            print("Player 1 picked up Gladius.");
            p1Sword.SetActive(true);
            Destroy(gameObject); // Destroy Gladius object on the ground
        }
    }

    void checkP2()
    {
        if (Vector3.Distance(transform.position, p2.transform.position) < 4f)
        {
            print("Player 2 picked up Gladius.");
            p2Sword.SetActive(true);
            Destroy(gameObject); // Destroy Gladius object on the ground
        }
    }
}
