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
            if (Input.GetKeyDown(KeyCode.Z)) // Bandages
                OnCraftBandagesKK();

            if (Input.GetKeyDown(KeyCode.X)) // Club
                OnCraftClubKK();
        }
    }

    void OnCraftBandages()
    {
        if (gameScript.checkIndividualRecipe(gameScript.bandagesRecipe) && playerCInRange)
        {
            //print("Made bandages");
            GameObject bandage = Instantiate(gameScript.bandagesPrefab, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            bandage.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;

            gameScript.clothPieces -= (int)gameScript.bandagesRecipe[0];
            gameScript.woodPieces -= (int)gameScript.bandagesRecipe[1];
            gameScript.ironPieces -= (int)gameScript.bandagesRecipe[2];
        }
    }

    void OnCraftClub()
    {
        if (gameScript.checkIndividualRecipe(gameScript.woodClubRecipe) && playerCInRange)
        {
            //print("Made Club");
            GameObject bandage = Instantiate(gameScript.clubPickup, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            bandage.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;

            gameScript.clothPieces -= (int)gameScript.woodClubRecipe[0];
            gameScript.woodPieces -= (int)gameScript.woodClubRecipe[1];
            gameScript.ironPieces -= (int)gameScript.woodClubRecipe[2];
        }
    }

    void OnCraftBandagesKK()
    {
        if (gameScript.checkIndividualRecipe(gameScript.bandagesRecipe))
        {
            print("Made bandages");
            GameObject bandage = Instantiate(gameScript.bandagesPrefab, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            bandage.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;

            gameScript.clothPieces -= (int)gameScript.bandagesRecipe[0];
            gameScript.woodPieces -= (int)gameScript.bandagesRecipe[1];
            gameScript.ironPieces -= (int)gameScript.bandagesRecipe[2];
        }
    }

    void OnCraftClubKK()
    {
        if (gameScript.checkIndividualRecipe(gameScript.woodClubRecipe))
        {
            print("Made Club");
            GameObject bandage = Instantiate(gameScript.clubPickup, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            bandage.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;

            gameScript.clothPieces -= (int)gameScript.woodClubRecipe[0];
            gameScript.woodPieces -= (int)gameScript.woodClubRecipe[1];
            gameScript.ironPieces -= (int)gameScript.woodClubRecipe[2];
        }
    }
}
