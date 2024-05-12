using HighScore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameHandler : MonoBehaviour
{
    //public GameObject playerOne;
    //public GameObject playerTwo;
    //public string playerOneControls = "---";
    //public string playerTwoControls = "---";

    gameHandler gameScript;
    HighScoreTest hs;
    private const string playerKScoreTextObjectName = "ScoreK";
    private const string playerCScoreTextObjectName = "ScoreC";

    public GameObject keyboardPlayer;
    public float keyboardPlayerHealth;
    public Image _healthbarSpriteK;

    public GameObject controllerPlayer;
    public float controllerPlayerHealth;
    public Image _healthbarSpriteC;

    [Header("Needed Prefabs")]
    public GameObject goldPrefab;
    public GameObject clothPrefab;
    public GameObject woodPrefab;
    public GameObject ironPrefab;
    public GameObject bandagesPrefab;
    public GameObject skullPrefab;
    public GameObject gladiusPickup;
    public GameObject clubPickup;

    [Header("Resources")]
    public Transform ResourceTransform;
    public int ResourceDropRate = 1;
    public int gold;
    public int clothPieces;
    public int woodPieces;
    public int ironPieces;

    

    [Header("Recipes")] // Cloth,  Wood,  Iron
    public Vector3 bandagesRecipe = new Vector3(5, 0, 0);
    public Vector3 armorRecipe = new Vector3(30, 0, 10);
    public Vector3 spearRecipe = new Vector3(0, 10, 5);
    public Vector3 woodClubRecipe = new Vector3(0, 25, 5);


    ////////////////////////////////////////////////////////

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Cursor.lockState = CursorLockMode.Locked;
        ResourceTransform = GameObject.FindGameObjectWithTag("ResourceTransform").transform;
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
        hs = gameScript.GetComponent<HighScoreTest>();
    }

    void Update()
    {
        keyboardPlayer.SetActive(keyboardPlayerHealth >= 0);
        controllerPlayer.SetActive(controllerPlayerHealth >= 0);

        if (keyboardPlayerHealth <= 0f && controllerPlayerHealth <= 0f)
            SceneManager.LoadScene("victoryscene");

        _healthbarSpriteK.fillAmount = keyboardPlayerHealth / 100f;
        _healthbarSpriteC.fillAmount = controllerPlayerHealth / 100f;
    }

    //////////////////////////////////////////// INPUT /////////////////////////////////////////////////

    void OnToggleCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
        else if (Cursor.lockState == CursorLockMode.None)
            Cursor.lockState = CursorLockMode.Locked;
    }

    void OnQuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////
    
    public void collectResource(string name)
    {
        if (name == "Gold")
        {
            gold++;
            TMPro.TextMeshProUGUI playerScoreText = GetPlayerScoreText();
            TMPro.TextMeshProUGUI playerScoreText2 = GetPlayerScoreText2();
            if (hs)
            {
                hs.score += 1;
                playerScoreText.text = "Score: " + hs.score.ToString();
                playerScoreText2.text = "Score: " + hs.score.ToString();
            }


        }
        if (name == "Cloth") clothPieces++;
        if (name == "Wood") woodPieces++;
        if (name == "Metal Scrap") ironPieces++;
    }

    public void possibleRecipes()
    {
        // Check Bandages
        if (checkIndividualRecipe(bandagesRecipe))
            print("CAN make Bandages");

        // Check Armor
        if (checkIndividualRecipe(armorRecipe))
            print("CAN make Armor");

        // Check Spear
        if (checkIndividualRecipe(spearRecipe))
            print("CAN make Spear");

        // Check Wooden Club
        if (checkIndividualRecipe(woodClubRecipe))
            print("CAN make Wooden Club");


    }

    public TMPro.TextMeshProUGUI GetPlayerScoreText()
    {
        GameObject textObject = GameObject.Find(playerKScoreTextObjectName); // Find the GameObject by name
        if (textObject != null)
        {
            return textObject.GetComponentInChildren<TMPro.TextMeshProUGUI>(); // Get TextMeshProUGUI component
        }
        else
        {
            Debug.LogWarning("Player score text object not found with name: " + playerKScoreTextObjectName);
            return null;
        }
    }
    public TMPro.TextMeshProUGUI GetPlayerScoreText2()
    {
        GameObject textObject2 = GameObject.Find(playerCScoreTextObjectName); // Find the GameObject by name
        if (textObject2 != null)
        {
            return textObject2.GetComponentInChildren<TMPro.TextMeshProUGUI>(); // Get TextMeshProUGUI component
        }
        else
        {
            Debug.LogWarning("Player score text object not found with name: " + playerCScoreTextObjectName);
            return null;
        }

    }

    public bool checkIndividualRecipe(Vector3 recipe)
    {
        return (clothPieces >= recipe[0] && woodPieces >= recipe[1] && ironPieces >= recipe[2]);
    }


}
