using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree : MonoBehaviour
{
    private gameHandler gameScript;
    private playerAccessWeapons playerKHand;
    private playerAccessWeapons playerCHand;
    private float dK;
    private float dC;
    private GameObject woodPiece;

    public bool canBeHit = true;
    private float nextHitTime;
    public float treeCooldown = 0.8f;
    private float moveDownDist = 0.2f;
    public bool funny;
    public int hitsDone = 0;

    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
        playerKHand = gameScript.keyboardPlayer.GetComponent<playerAccessWeapons>();
        playerCHand = gameScript.controllerPlayer.GetComponent<playerAccessWeapons>();

        woodPiece = gameScript.woodPrefab;
    }

    void Update()
    {
        if (canBeHit)
        {
            dK = Vector3.Distance(transform.position, playerKHand.getHandObject().transform.position);
            dC = Vector3.Distance(transform.position, playerCHand.getHandObject().transform.position);

            if (dK < 5f && playerKHand.swinging)
                takeHitAndDropWood();
            if (dC < 5f && playerCHand.swinging)
                takeHitAndDropWood();
        }

        canBeHit = Time.time > nextHitTime;
    }

    void takeHitAndDropWood()
    {
        canBeHit = false;
        nextHitTime = Time.time + treeCooldown;
        hitsDone++;

        for (int i = 0; i < gameScript.ResourceDropRate; i++)
        {
            GameObject currentWood = Instantiate(woodPiece, transform.position + Vector3.up + Random.insideUnitSphere * 2f, Quaternion.identity, gameScript.ResourceTransform);
            float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
            currentWood.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);
            currentWood.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 22f;
        }

        transform.localScale *= 0.9f;
        if (funny) transform.Translate(transform.up * moveDownDist * 1.7f);
        else transform.Translate(Vector3.down * moveDownDist);

        if (hitsDone >= 5)
            Destroy(gameObject);
    }
}
