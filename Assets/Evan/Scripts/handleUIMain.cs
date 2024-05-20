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
    public TextMeshProUGUI placerKPoisionPotion;
    public TextMeshProUGUI placerCPoisionPotion;
    public TextMeshProUGUI placerKDashPotion;
    public TextMeshProUGUI placerCDashPotion;

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

        if (gameScript.keyboardPlayerPoisonPotion)
        {
            placerKPoisionPotion.text = "1";
        }

        else

        {
            placerKPoisionPotion.text = "0";
        }

        if (gameScript.controllerPlayerPoisonPotion)
        {
            placerCPoisionPotion.text = "1";
        }

        else
        {
            placerCPoisionPotion.text = "0";
        }

        if (gameScript.keyboardPlayerDashPotion)
        {
            placerKDashPotion.text = "1";
        }

        else
        {
            placerKDashPotion.text = "0";
        }

        if (gameScript.controllerPlayerDashPotion)
        {
            placerCDashPotion.text = "1";
        }

        else
        {
            placerCDashPotion.text = "0";
        }

        // Useable
        playerKBandages.text = gameScript.keyboardPlayerBandages.ToString();
        playerCBandages.text = gameScript.controllerPlayerBandages.ToString();
    }
}
