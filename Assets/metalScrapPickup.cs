using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class metalScrapPickup : MonoBehaviour
{
    public gameHandler gameScript;
    public float pickupDist;
    public float pickupTimer;

    float timeOfSpawn;

    // Start is called before the first frame update
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
        timeOfSpawn = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float timeAlive = Time.time - timeOfSpawn;

        if (timeAlive > pickupTimer)
        {
            foreach (GameObject currPlayer in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (Vector3.Distance(transform.position + Vector3.up, currPlayer.transform.position) < pickupDist)
                {
                    gameScript.collectResource("Metal Scrap");
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }

}
