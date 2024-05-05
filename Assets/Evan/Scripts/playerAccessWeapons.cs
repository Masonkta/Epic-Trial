using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAccessWeapons : MonoBehaviour
{
    public bool handIsOpen;
    public bool readyToGrab;
    
    float timeOfLastDrop;

    public GameObject gladius;
    public GameObject club;

    [Header("Pickups")]
    public GameObject gladiusPickup;
    public GameObject clubPickup;


    // Start is called before the first frame update
    void Start()
    {
        timeOfLastDrop = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        handIsOpen = handOpen();

        readyToGrab = Time.time - timeOfLastDrop > 1f;
    }

    public GameObject getGladius()
    {
        return gladius;
    }

    public GameObject getClub()
    {
        return club;
    }

    public void OnDropItem()
    {
        if (gladius.activeInHierarchy)
        {
            print("DROP Gladius");

            GameObject gladiusPickupComingFromDrop = Instantiate(gladiusPickup, gladius.transform.position, GetComponent<playerMovement>().cameraTransform.rotation);
            gladiusPickupComingFromDrop.GetComponent<Rigidbody>().velocity = GetComponent<playerMovement>().getLookDirection() * 8f;
            gladiusPickupComingFromDrop.GetComponent<Rigidbody>().angularVelocity = transform.right * 100f;


            gladius.SetActive(false);
        }


        if (club.activeInHierarchy)
        {
            print("DROP Club");

            GameObject clubPickupComingFromDrop = Instantiate(clubPickup, gladius.transform.position, GetComponent<playerMovement>().cameraTransform.rotation);
            clubPickupComingFromDrop.GetComponent<Rigidbody>().velocity = GetComponent<playerMovement>().getLookDirection() * 8f;
            clubPickupComingFromDrop.GetComponent<Rigidbody>().angularVelocity = transform.right * 100f;

            club.SetActive(false);
        }


        timeOfLastDrop = Time.time;
    }

    public bool handOpen()
    {
        return !(gladius.activeInHierarchy || club.activeInHierarchy);
    }
}
