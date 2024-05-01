using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinPickup : MonoBehaviour
{
    public gameHandler gameScript;

    // Start is called before the first frame update
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject currPlayer in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (Vector3.Distance(transform.position, currPlayer.transform.position) < 3f)
            {
                gameScript.getCloth();
                Destroy(gameObject);
                break;
            }
        }
        
    }

}
