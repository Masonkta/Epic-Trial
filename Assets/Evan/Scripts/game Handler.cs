using HighScore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameHandler : MonoBehaviour
{
    HighScoreTest hs;
    private const string playerKScoreTextObjectName = "ScoreK";
    private const string playerCScoreTextObjectName = "ScoreC";

    public GameObject keyboardPlayer;
    public float keyboardPlayerHealth;
    public Image _healthbarSpriteK;

    public GameObject controllerPlayer;
    public float controllerPlayerHealth;
    public Image _healthbarSpriteC;
    public RumbleHaptics controllerRumble;

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
    public int ResourceDropRate = 3;
    public int gold;
    public int clothPieces;
    public int woodPieces;
    public int ironPieces;
    public int keyboardPlayerBandages;
    public int controllerPlayerBandages;

    [Header("Recipes")] // Cloth,  Wood,  Iron
    public Vector3 bandagesRecipe = new Vector3(5, 0, 0);
    public Vector3 armorRecipe = new Vector3(30, 0, 10);
    public Vector3 spearRecipe = new Vector3(0, 10, 5);
    public Vector3 woodClubRecipe = new Vector3(0, 25, 5);

    [Header("Death Cameras")]
    public GameObject BackUpCamera;
    public GameObject BackUpCamera1;

    ////////////////////////////////////////////////////////

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        ResourceTransform = GameObject.FindGameObjectWithTag("ResourceTransform").transform;
        if (BackUpCamera) BackUpCamera.SetActive(false);
        if (BackUpCamera) BackUpCamera.SetActive(false);
        if (BackUpCamera1) BackUpCamera1.SetActive(false);
        // Activate Second Display
        if (!Application.isEditor)
            Display.displays[1].Activate();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main" || SceneManager.GetActiveScene().name == "Tutorial")
        {

            if (SceneManager.GetActiveScene().name == "Main")
            {

                if (keyboardPlayerHealth <= 0)
                {
                    keyboardPlayer.SetActive(false);
                    BackUpCamera.SetActive(true);
                }

                if (controllerPlayerHealth <= 0)
                {
                    controllerPlayer.SetActive(false);
                    BackUpCamera1.SetActive(true);
                }

                if (keyboardPlayerHealth <= 0f && controllerPlayerHealth <= 0f)
                {
                    // BOTH PLAYERS DIED
                    SceneManager.LoadScene("gameOver");
                }
            }

            _healthbarSpriteK.fillAmount = keyboardPlayerHealth / 100f;
            _healthbarSpriteC.fillAmount = controllerPlayerHealth / 100f;


            if (Input.GetKeyDown(KeyCode.R))
                useControllerBandage();
        }
        

        ///////////////////////////////////////////////////////////////////////

        if (Input.GetKeyDown(KeyCode.Escape))
            OnQuitGame();

    }

    //////////////////////////////////////////// INPUT /////////////////////////////////////////////////

    void OnToggleCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
        else if (Cursor.lockState == CursorLockMode.None)
            Cursor.lockState = CursorLockMode.Locked;
    }
    public IEnumerator WaitAndChangeScenetask()
    {
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("victoryScene");
    }

    public void WaitAndChangeScene()
    {
        StartCoroutine(WaitAndChangeScenetask());
    }

    void OnQuitGame()
    {
        Application.Quit();
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

    public void collectKeyboardBandages()
    {
        keyboardPlayerBandages++;
    }

    public void collectControllerBandages()
    {
        controllerPlayerBandages++;
    }

    public void useKeyboardBandage()
    {
        if (keyboardPlayerBandages > 0)
        {
            keyboardPlayerHealth += 20f;
            if (keyboardPlayerHealth > 100f)
                keyboardPlayerHealth = 100f;
            keyboardPlayerBandages--;
        }
    }

    public void useControllerBandage()
    {
        if (controllerPlayerBandages > 0)
        {
            controllerPlayerHealth += 20f;
            if (controllerPlayerHealth > 100f)
                controllerPlayerHealth = 100f;
            controllerPlayerBandages--;
        }

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

    public float GetControllerHealth()
    {
        return controllerPlayerHealth;
    }
}
