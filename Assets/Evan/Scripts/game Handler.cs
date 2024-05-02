using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameHandler : MonoBehaviour
{
    //public GameObject playerOne;
    //public GameObject playerTwo;
    //public string playerOneControls = "---";
    //public string playerTwoControls = "---";

    public GameObject keyboardPlayer;
    public float keyboardPlayerHealth;
    public GameObject controllerPlayer;
    public float controllerPlayerHealth;

    [Header("Needed Prefabs")]
    public GameObject goldPrefab;
    public GameObject clothPrefab;
    public GameObject woodPrefab;
    public GameObject ironPrefab;

    [Header("Resources")]
    public int resourceDropRate = 1;
    public int gold;
    public int clothPieces;
    public int woodPieces;
    public int ironPieces;

    

    [Header("Recipes")]
    public Vector3 bandagesRecipe = new Vector3(5, 0, 0);
    public Vector3 armorRecipe = new Vector3(30, 0, 10);
    public Vector3 spearRecipe = new Vector3(0, 10, 5);
    public Vector3 woodClubRecipe = new Vector3(0, 25, 5);


    ////////////////////////////////////////////////////////

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        
    }

    /*public void setPlayerOneAsKeyboard()
    {
        playerOneControls = "Keyboard";
        playerTwoControls = "Controller";
    }

    public void setPlayerOneAsController()
    {
        playerOneControls = "Controller";
        playerTwoControls = "Keyboard";
    }*/


    //////////////////////////////////////////// INPUT /////////////////////////////////////////////////

    void OnToggleCursor()
    {
        print("TAB");
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
        if (name == "Gold") gold++;
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

    bool checkIndividualRecipe(Vector3 recipe)
    {
        return (clothPieces > recipe[0] && woodPieces > recipe[1] && ironPieces > recipe[2]);
    }


}
