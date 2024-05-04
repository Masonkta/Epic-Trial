using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourcePickup : MonoBehaviour
{
    public gameHandler gameScript;
    public string resourceType;
    public List<GameObject> players = new List<GameObject>();

    float timeOfSpawn;
    float pickupDist = 3.5f;
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
            pickupDist = 2.2f;
            pickupTimer = 0.1f;
        }

        players.Add(gameScript.keyboardPlayer);
        players.Add(gameScript.controllerPlayer);

    }

    // Update is called once per frame
    void Update()
    {
        float timeOnMap = Time.time - timeOfSpawn;

        if (timeOnMap > pickupTimer)
            checkWithinRangeOfBothPlayers();


        if (resourceType == "Gold" && timeOnMap > pickupTimer * 2f)
            checkPullToPlayers();
    }

    void checkWithinRangeOfBothPlayers()
    {
        // Keyboard
        if (Vector3.Distance(transform.position + Vector3.up, gameScript.keyboardPlayer.transform.position) < pickupDist)
        {
            gameScript.collectResource(resourceType);
            Destroy(gameObject);
        }

        // Controller
        if (Vector3.Distance(transform.position + Vector3.up, gameScript.controllerPlayer.transform.position) < pickupDist)
        {
            gameScript.collectResource(resourceType);
            Destroy(gameObject);
        }
    }

    void checkPullToPlayers()
    {
        float PullRange = 40f;

        float distToClosestPlayer = Mathf.Infinity;
        Vector3 force = Vector3.zero;
        

        // Controller
        foreach (GameObject currPlayer in players)
        {
            float distToCurrentPlayer = Vector3.Distance(transform.position + Vector3.up, currPlayer.transform.position);

            if (distToCurrentPlayer < PullRange && distToCurrentPlayer < distToClosestPlayer)
            {
                distToClosestPlayer = distToCurrentPlayer;

                Vector3 pullDir = Vector3.Normalize(currPlayer.transform.position - transform.position);

                //float mag = 1 - Mathf.Max(Vector3.Distance(gameScript.controllerPlayer.transform.position, transform.position) / PullRange, 0.2f); // Stronger closer to player
                float mag = Mathf.Max(Vector3.Distance(currPlayer.transform.position, transform.position) / PullRange, 0.2f);   // Stronger father from player

                float pullForce = mag * 1200f;

                //Debug.DrawRay(transform.position, pull * mag );
                rb.AddForce(pullDir * pullForce * Time.deltaTime, ForceMode.Force);
            }
        }
        


        /*// Keyboard
        if (Vector3.Distance(transform.position + Vector3.up, gameScript.keyboardPlayer.transform.position) < PullRange && target == 0)
        {
            target = 2;
            Vector3 pullDir = Vector3.Normalize(gameScript.keyboardPlayer.transform.position - transform.position);

            //float mag = 1 - Mathf.Max(Vector3.Distance(gameScript.keyboardPlayer.transform.position, transform.position) / PullRange, 0.2f); // Stronger closer to player
            float mag = Mathf.Max(Vector3.Distance(gameScript.keyboardPlayer.transform.position, transform.position) / PullRange, 0.2f);   // Stronger father from player

            float pullForce = mag * 1200f;

            //Debug.DrawRay(transform.position, pull * mag );
            rb.AddForce(pullDir * pullForce * Time.deltaTime, ForceMode.Force);
        }*/


    }

}