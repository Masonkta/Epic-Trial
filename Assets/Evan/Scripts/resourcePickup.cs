using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourcePickup : MonoBehaviour
{
    public gameHandler gameScript;
    public string resourceType;

    float timeOfSpawn;
    float pickupDist = 6.5f;
    float pickupTimer = 1f;
    Rigidbody rb;
    MeshRenderer rendered;

    // Start is called before the first frame update
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
        timeOfSpawn = Time.time;
        rb = GetComponent<Rigidbody>();
        rendered = GetComponent<MeshRenderer>();

        if (resourceType == "Gold")
        {
            pickupDist = 2.2f;
            pickupTimer = 0.1f;
        }

        if (resourceType == "Berries")
            pickupDist = 2.6f;


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
        float realRange = Input.GetKey(KeyCode.O) && Input.GetKeyUp(KeyCode.P) ? 500f : pickupDist;

        // Keyboard
        if (Vector3.Distance(transform.position + Vector3.up, gameScript.keyboardPlayer.transform.position) < realRange)
        {
            if (resourceType != "Berries")
            {
                gameScript.collectResource(resourceType);
                Destroy(gameObject);
            }
            else
            {
                if (rendered.enabled == true)
                {
                    gameScript.collectResource(resourceType);
                    rendered.enabled = false;
                    StartCoroutine(RespawnBerry());
                }
            }
        }

        // Controller
        if (Vector3.Distance(transform.position + Vector3.up, gameScript.controllerPlayer.transform.position) < realRange)
        {
            if (resourceType != "Berries")
            {
                gameScript.collectResource(resourceType);
                Destroy(gameObject);
            }
            else
            {
                if (rendered.enabled == true)
                {
                    gameScript.collectResource(resourceType);
                    rendered.enabled = false;
                    StartCoroutine(RespawnBerry());
                }
            }
        }
    }

    void checkPullToPlayers()
    {
        float PullRange = 40f;

        // Controller
        float dK = Vector3.Distance(transform.position, gameScript.keyboardPlayer.transform.position);
        float dC = Vector3.Distance(transform.position, gameScript.controllerPlayer.transform.position);

        if (dK < PullRange && dK <= dC)
        {
            Vector3 pullDir = Vector3.Normalize(gameScript.keyboardPlayer.transform.position - transform.position);
            //float mag = 1 - Mathf.Max(Vector3.Distance(gameScript.controllerPlayer.transform.position, transform.position) / PullRange, 0.2f); // Stronger closer to player
            float mag = Mathf.Max(Vector3.Distance(gameScript.keyboardPlayer.transform.position, transform.position) / PullRange, 0.2f);   // Stronger father from player
            float pullForce = mag * 1200f;
            rb.AddForce(pullDir * pullForce * Time.deltaTime, ForceMode.Force);
        }

        if (dC < PullRange && dC < dK)
        {
            Vector3 pullDir = Vector3.Normalize(gameScript.controllerPlayer.transform.position - transform.position);
            //float mag = 1 - Mathf.Max(Vector3.Distance(gameScript.controllerPlayer.transform.position, transform.position) / PullRange, 0.2f); // Stronger closer to player
            float mag = Mathf.Max(Vector3.Distance(gameScript.controllerPlayer.transform.position, transform.position) / PullRange, 0.2f);   // Stronger father from player
            float pullForce = mag * 1200f;
            rb.AddForce(pullDir * pullForce * Time.deltaTime, ForceMode.Force);
        }
    }

    IEnumerator RespawnBerry()
    {
        yield return new WaitForSeconds(5f);
        rendered.enabled = true;
    }

}