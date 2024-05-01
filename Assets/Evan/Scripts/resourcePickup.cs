using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourcePickup : MonoBehaviour
{
    public gameHandler gameScript;
    public string resourceType;

    float timeOfSpawn;
    float pickupDist = 2.5f;
    float pickupTimer = 1.2f;

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
        foreach (GameObject currPlayer in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (Vector3.Distance(transform.position + Vector3.up, currPlayer.transform.position) < pickupDist)
            {
                gameScript.collectResource(resourceType);
                Destroy(gameObject);
                break;
            }
        }
    }

}