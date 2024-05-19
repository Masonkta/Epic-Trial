using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using UnityEngine.InputSystem.Android.LowLevel;

public class makeItems : MonoBehaviour
{
    public gameHandler gameScript;

    public GameObject keyboardPlayer;
    public GameObject controllerPlayer;

    public float range = 7f;

    public bool playerKInRange = false;
    public bool playerCInRange = false;

    public GameObject KControls;
    public GameObject CControls;

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

        KControls.SetActive(playerKInRange);
        CControls.SetActive(playerCInRange);
    }

    void handleInput()
    {
        if (playerKInRange)
        {
            if (Input.GetKeyDown(KeyCode.Z)) // Bandages
                OnCraftBandagesKK();

            if (Input.GetKeyDown(KeyCode.X)) // Club
                OnCraftClubKK();
            
            if (Input.GetKeyDown(KeyCode.C)) // Gladius
                OnCraftGladiusKK();
        }
    }

    void OnCraftBandages()
    {
        if (gameScript.checkRecipe("Bandage") && playerCInRange)
        {
            GameObject bandage = Instantiate(gameScript.bandagesPrefab, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            bandage.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;

            gameScript.clothPieces -= 10;
        }
    }    

    void OnCraftClub()
    {
        if (gameScript.checkRecipe("Club") && playerCInRange)
        {
            GameObject bandage = Instantiate(gameScript.clubPickup, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            bandage.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;

            gameScript.clothPieces -= 10;
            gameScript.woodPieces -= 20;
        }
    }

    void OnCraftGladius()
    {
        if (gameScript.checkRecipe("Gladius") && playerCInRange)
        {
            GameObject bandage = Instantiate(gameScript.gladiusPickup, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            bandage.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;

            gameScript.clothPieces -= 10;
            gameScript.woodPieces -= 10;
            gameScript.ironPieces -= 25;
        }
    }

    void OnCraftBandagesKK()
    {
        if (gameScript.checkRecipe("Bandage"))
        {
            GameObject bandage = Instantiate(gameScript.bandagesPrefab, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            bandage.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;

            gameScript.clothPieces -= 10;
        }
    }

    void OnCraftClubKK()
    {
        if (gameScript.checkRecipe("Club"))
        {
            GameObject bandage = Instantiate(gameScript.clubPickup, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            bandage.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;

            gameScript.clothPieces -= 10;
            gameScript.woodPieces -= 25;
        }
    }


    void OnCraftGladiusKK()
    {
        if (gameScript.checkRecipe("Gladius"))
        {
            GameObject bandage = Instantiate(gameScript.gladiusPickup, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            bandage.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;

            gameScript.clothPieces -= 10;
            gameScript.woodPieces -= 10;
            gameScript.ironPieces -= 25;
        }
    }
}
