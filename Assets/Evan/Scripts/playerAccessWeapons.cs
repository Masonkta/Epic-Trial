using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAccessWeapons : MonoBehaviour
{
    public bool handIsOpen;

    public GameObject gladius;
    public GameObject club;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handIsOpen = handOpen();
    }

    public GameObject getGladius()
    {
        return gladius;
    }

    public GameObject getClub()
    {
        return club;
    }

    void OnDropItem()
    {
        if (gladius.activeInHierarchy){
            print("DROP Gladius");
            gladius.SetActive(false);
        }

        if (club.activeInHierarchy)
        {
            print("DROP Club");
            club.SetActive(false);
        }
        
        else
            print("Holding Nothing");
    }

    public bool handOpen()
    {
        return !(gladius.activeInHierarchy || club.activeInHierarchy);
    }
}
