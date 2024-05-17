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

    [Header("Usable")]
    public TextMeshProUGUI playerKBandages;
    public TextMeshProUGUI playerCBandages;


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

        // Useable
        playerKBandages.text = gameScript.keyboardPlayerBandages.ToString();
        playerCBandages.text = gameScript.controllerPlayerBandages.ToString();
    }
}
