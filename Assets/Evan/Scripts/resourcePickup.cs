using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourcePickup : MonoBehaviour
{
    public gameHandler gameScript;
    public string resourceType;

    float timeOfSpawn;
    float pickupDist = 3.2f;
    float pickupTimer = 1f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
        timeOfSpawn = Time.time;
        rb = GetComponent<Rigidbody>();

        if (resourceType == "Gold")
        {
            pickupDist = 2f;
            pickupTimer = 0.08f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float timeOnMap = Time.time - timeOfSpawn;

        if (timeOnMap > pickupTimer)
            checkWithinRangeOfBothPlayers();


        if (resourceType == "Gold" && timeOnMap > pickupTimer)
            checkPullToPlayers();
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

    void checkPullToPlayers()
    {
        foreach (GameObject currPlayer in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (Vector3.Distance(transform.position + Vector3.up, currPlayer.transform.position) < 20f)
            {
                // Pull To Player Within Range (Can Stack with both players)
                Vector3 pullDir = Vector3.Normalize(currPlayer.transform.position - transform.position);
                
                float mag = 1 - Mathf.Max(Vector3.Distance(currPlayer.transform.position, transform.position) / 20f, 0.4f); // Stronger closer to player
                //float mag = Mathf.Max(Vector3.Distance(currPlayer.transform.position, transform.position) / 20f, 0.4f);   // Stronger father from player

                float pullForce = mag * 100f;

                //Debug.DrawRay(transform.position, pull * mag );
                rb.AddForce(pullDir * pullForce * Time.deltaTime, ForceMode.Force);
            }
        }
    }

}