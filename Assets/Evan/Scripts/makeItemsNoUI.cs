using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using UnityEngine.InputSystem.Android.LowLevel;

public class makeItemsNoUI : MonoBehaviour
{
    public gameHandler gameScript;

    public GameObject keyboardPlayer;
    public GameObject controllerPlayer;

    public float range = 7f;

    public bool playerKInRange = false;
    public bool playerCInRange = false;
    public bool actuallyCraft = false;

    // Start is called before the first frame update
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();

        keyboardPlayer = gameScript.keyboardPlayer;
        controllerPlayer = gameScript.controllerPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayers();

        handleInput();
    }

    void checkPlayers()
    {
        float dK = Vector3.Distance(transform.position, keyboardPlayer.transform.position);
        float dC = Vector3.Distance(transform.position, controllerPlayer.transform.position);
        playerKInRange = (dK < range);
        playerCInRange = (dC < range);
    }

    void handleInput()
    {
        if (playerKInRange)
        {
            if (actuallyCraft) {
                if (Input.GetKeyDown(KeyCode.Alpha1)) // Bandages
                    OnCraftBandagesKK();

                if (Input.GetKeyDown(KeyCode.Alpha2)) // Club
                    OnCraftClubKK();

                if (Input.GetKeyDown(KeyCode.Alpha3)) // Gladius
                    OnCraftGladiusKK();
            }
        }
    }

    void OnCraftBandages()
    {
        if (actuallyCraft){
            GameObject bandage = Instantiate(gameScript.bandagesPrefab, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            bandage.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;
        }
    }

    void OnCraftClub()
    {
        if (actuallyCraft)
        {
            GameObject bandage = Instantiate(gameScript.clubPickup, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            bandage.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;
        }

    }
    void OnCraftGladius()
    {
        if (actuallyCraft)
        {
            GameObject bandage = Instantiate(gameScript.gladiusPickup, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            bandage.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;
        }

    }

    void OnCraftBandagesKK()
    {
        GameObject bandage = Instantiate(gameScript.bandagesPrefab, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
        bandage.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;

    }

    void OnCraftClubKK()
    {
        GameObject bandage = Instantiate(gameScript.clubPickup, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
        bandage.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;

    }
    void OnCraftGladiusKK()
    {
        GameObject bandage = Instantiate(gameScript.gladiusPickup, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
        bandage.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;

    }
}
