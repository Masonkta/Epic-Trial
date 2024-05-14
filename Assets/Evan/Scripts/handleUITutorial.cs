using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class handleUITutorial : MonoBehaviour
{
    public gameHandler gameScript;
    public TextMeshProUGUI playerKCloth;
    public TextMeshProUGUI playerCCloth;
    public TextMeshProUGUI playerKWood;
    public TextMeshProUGUI playerCWood;
    public TextMeshProUGUI playerKIron;
    public TextMeshProUGUI playerCIron;

    // Start is called before the first frame update
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        playerKCloth.text = gameScript.clothPieces.ToString();
        playerCCloth.text = gameScript.clothPieces.ToString();
        playerKWood.text  = gameScript.woodPieces.ToString();
        playerCWood.text  = gameScript.woodPieces.ToString();
        playerKIron.text  = gameScript.ironPieces.ToString();
        playerCIron.text  = gameScript.ironPieces.ToString();
    }
}
