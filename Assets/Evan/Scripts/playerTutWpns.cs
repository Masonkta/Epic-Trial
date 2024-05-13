using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerTut : MonoBehaviour
{
    public bool handIsOpen;
    public bool readyToGrab;

    public bool swinging;

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

        readyToGrab = Time.time > timeOfLastDrop + 0.6f;
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
        if (!swinging)
        {
            readyToGrab = false;



            if (gladius.activeInHierarchy)
            {
                GameObject gladiusPickupComingFromDrop = Instantiate(gladiusPickup, gladius.transform.position, GetComponent<playerMTutorial>().cameraTransform.rotation);
                gladiusPickupComingFromDrop.GetComponent<Rigidbody>().velocity = GetComponent<playerMTutorial>().getLookDirection() * 8f;
                gladiusPickupComingFromDrop.GetComponent<Rigidbody>().angularVelocity = transform.right * 100f;


                gladius.SetActive(false);
            }


            if (club.activeInHierarchy)
            {
                GameObject clubPickupComingFromDrop = Instantiate(clubPickup, gladius.transform.position, GetComponent<playerMTutorial>().cameraTransform.rotation);
                clubPickupComingFromDrop.GetComponent<Rigidbody>().velocity = GetComponent<playerMTutorial>().getLookDirection() * 8f;
                clubPickupComingFromDrop.GetComponent<Rigidbody>().angularVelocity = transform.right * 100f;
                


                club.SetActive(false);
            }


            timeOfLastDrop = Time.time;
        }
    }

    public bool handOpen()
    {
        return !(gladius.activeInHierarchy || club.activeInHierarchy);
    }
}
