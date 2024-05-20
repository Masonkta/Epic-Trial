using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class handleUIMain : MonoBehaviour
{
    public gameHandler gameScript;

    [Header("Resources")]
    public TextMeshProUGUI playerKCloth;
    public TextMeshProUGUI playerCCloth;
    public TextMeshProUGUI playerKWood;
    public TextMeshProUGUI playerCWood;
    public TextMeshProUGUI playerKIron;
    public TextMeshProUGUI playerCIron;
    public TextMeshProUGUI playerKBerries;
    public TextMeshProUGUI playerCBerries;

    [Header("Usable")]
    public TextMeshProUGUI playerKBandages;
    public TextMeshProUGUI playerCBandages;
    public TextMeshProUGUI playerKPoisionPotion;
    public TextMeshProUGUI playerCPoisionPotion;
    public TextMeshProUGUI playerKDashPotion;
    public TextMeshProUGUI playerCDashPotion;
    public TextMeshProUGUI playerKFeathers;
    public TextMeshProUGUI playerCFeathers;

    // Start is called before the first frame update
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        // Resources
        playerKCloth.text = gameScript.clothPieces.ToString();
        playerCCloth.text = gameScript.clothPieces.ToString();
        playerKWood.text = gameScript.woodPieces.ToString();
        playerCWood.text = gameScript.woodPieces.ToString();
        playerKIron.text = gameScript.ironPieces.ToString();
        playerCIron.text = gameScript.ironPieces.ToString();
        playerKBerries.text = gameScript.Berries.ToString();
        playerCBerries.text = gameScript.Berries.ToString();
        playerKFeathers.text = gameScript.Feathers.ToString();
        playerCFeathers.text = gameScript.Feathers.ToString();

        if (gameScript.keyboardPlayerPoisonPotion)
        {
            playerKPoisionPotion.text = "1";
        }

        else

        {
            playerKPoisionPotion.text = "0";
        }

        if (gameScript.controllerPlayerPoisonPotion)
        {
            playerCPoisionPotion.text = "1";
        }

        else
        {
            playerCPoisionPotion.text = "0";
        }

        if (gameScript.keyboardPlayerDashPotion)
        {
            playerKDashPotion.text = "1";
        }

        else
        {
            playerKDashPotion.text = "0";
        }

        if (gameScript.controllerPlayerDashPotion)
        {
            playerCDashPotion.text = "1";
        }

        else
        {
            playerCDashPotion.text = "0";
        }

        // Useable
        playerKBandages.text = gameScript.keyboardPlayerBandages.ToString();
        playerCBandages.text = gameScript.controllerPlayerBandages.ToString();
    }
}
