using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree : MonoBehaviour
{
    public gameHandler gameScript;
    public playerAccessWeapons playerKHand;
    public playerAccessWeapons playerCHand;

    public bool canBeHit = true;

    // Start is called before the first frame update
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
        playerKHand = gameScript.keyboardPlayer.GetComponent<playerAccessWeapons>();
        playerCHand = gameScript.controllerPlayer.GetComponent<playerAccessWeapons>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canBeHit)
        {
            float dK = Vector3.Distance(transform.position, playerKHand.getHandObject().transform.position);
            float dC = Vector3.Distance(transform.position, playerCHand.getHandObject().transform.position);

            if (dK < 5 && playerKHand.swinging)
            {
                canBeHit = false;
                resetTree();
                print("HIT");

            }
        }

    }

    IEnumerator resetTree()
    {
        yield return new WaitForSeconds(1f);

        canBeHit = true;
    }
}
