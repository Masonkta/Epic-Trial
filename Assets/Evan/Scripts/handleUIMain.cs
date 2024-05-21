using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public TextMeshProUGUI playerKFeathers;
    public TextMeshProUGUI playerCFeathers;

    [Header("Usable")]
    public TextMeshProUGUI playerKBandages;
    public TextMeshProUGUI playerCBandages;
    public Image playerKPoisionPotion;
    public Image playerCPoisionPotion;
    public Image playerKDashPotion;
    public Image playerCDashPotion;

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


        


        // Useable
        playerKBandages.text = gameScript.keyboardPlayerBandages.ToString();
        playerCBandages.text = gameScript.controllerPlayerBandages.ToString();

        // Poison Potion
        if (gameScript.keyboardPlayerPoisonPotion)
            playerKPoisionPotion.color = new Color(1f, 1f, 1f, 1f);
        else
            playerKPoisionPotion.color = new Color(1f, 1f, 1f, 0.25f);


        if (gameScript.controllerPlayerPoisonPotion)
            playerCPoisionPotion.color = new Color(1f, 1f, 1f, 1f);
        else
            playerCPoisionPotion.color = new Color(1f, 1f, 1f, 0.25f);

        // Dash Potion
        if (gameScript.keyboardPlayerDashPotion)
            playerKDashPotion.color = new Color(1f, 1f, 1f, 1f);
        else
            playerKDashPotion.color = new Color(1f, 1f, 1f, 0.25f);

        if (gameScript.controllerPlayerDashPotion)
            playerCDashPotion.color = new Color(1f, 1f, 1f, 1f);
        else
            playerCDashPotion.color = new Color(1f, 1f, 1f, 0.25f);
    }
}
