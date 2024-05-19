using HighScore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class gameHandler : MonoBehaviour
{
    HighScoreTest hs;
    GameObject hs_trigger;
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
    public int Berries;
    public int keyboardPlayerBandages;
    public bool keyboardPlayerdashPotions;
    public bool keyboardPlayerpoisonPotions;
    public bool controllerPlayerdashPotions;
    public bool controllerPlayerpoisonPotions;
    public int controllerPlayerBandages;

    [Header("Recipes")] // Cloth,  Wood,  Iron
    public Vector3 bandagesRecipe = new Vector3(5, 0, 0);
    public Vector3 woodClubRecipe = new Vector3(0, 25, 5);

    [Header("Death Cameras")]
    public GameObject BackUpCamera;
    public GameObject BackUpCamera1;
    public GameObject SpectatorCam;
    private float respawnTime = 15f;
    private float respawnTimer;
    private bool keyboardDead = false;
    private bool controllerDead = false;
    private Vector3 respawnPosition = new Vector3(39, 8, -19);
    ////////////////////////////////////////////////////////

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        ResourceTransform = GameObject.FindGameObjectWithTag("ResourceTransform").transform;
        if (BackUpCamera) BackUpCamera.SetActive(false);
        if (BackUpCamera) BackUpCamera.SetActive(false);
        if (BackUpCamera1) BackUpCamera1.SetActive(false);
        if (SpectatorCam) SpectatorCam.SetActive(false);
        // Activate Second Display
        if (!Application.isEditor)
            Display.displays[1].Activate();
        if (SceneManager.GetActiveScene().name != "Tutorial" && SceneManager.GetActiveScene().name != "startScreen" && SceneManager.GetActiveScene().name != "The Final")
        {
            hs_trigger = GameObject.FindGameObjectWithTag("HighScore");
            if (hs_trigger.GetComponent<HighScoreTest>())
                hs = hs_trigger.GetComponent<HighScoreTest>();
        }
        respawnTimer = respawnTime;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main" || SceneManager.GetActiveScene().name == "Tutorial" || SceneManager.GetActiveScene().name == "The Final")
        {

            if (SceneManager.GetActiveScene().name == "Main")
            {

                if (keyboardPlayerHealth <= 0)
                {
                    respawnTimer -= Time.deltaTime;
                    keyboardPlayer.SetActive(false);
                    SpectatorCam.SetActive(true);
                    keyboardDead = true;
                    if (respawnTimer <= 0)
                    {
                        RespawnPlayers();
                    }
                }

                if (controllerPlayerHealth <= 0)
                {
                    respawnTimer -= Time.deltaTime;
                    controllerPlayer.SetActive(false);
                    SpectatorCam.SetActive(true);
                    controllerDead = true;
                    if (respawnTimer <= 0)
                    {
                        RespawnPlayers();
                    }
                }

                if (keyboardPlayerHealth <= 0f && controllerPlayerHealth <= 0f)
                {
                    // BOTH PLAYERS DIED
                    SceneManager.LoadScene("gameOver");
                }
            }

            if (SceneManager.GetActiveScene().name == "The Final")
            {
                if (keyboardPlayerHealth <= 0)
                {
                    keyboardPlayer.SetActive(false);
                }

                if (controllerPlayerHealth <= 0)
                {
                    controllerPlayer.SetActive(false);
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
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("The Final");
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
        if (name == "Berries") Berries++;
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
        if (keyboardPlayerBandages > 0 && keyboardPlayerHealth < 100f)
        {
            keyboardPlayerHealth += 20f;
            if (keyboardPlayerHealth > 100f)
                keyboardPlayerHealth = 100f;
            keyboardPlayerBandages--;
        }
    }

    public void useControllerBandage()
    {
        if (controllerPlayerBandages > 0 && keyboardPlayerHealth < 100f)
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
            return textObject.GetComponentInChildren<TMPro.TextMeshProUGUI>(); // Get TextMeshProUGUI component
        return null;
    }
    public TMPro.TextMeshProUGUI GetPlayerScoreText2()
    {
        GameObject textObject2 = GameObject.Find(playerCScoreTextObjectName); // Find the GameObject by name
        if (textObject2 != null)
            return textObject2.GetComponentInChildren<TMPro.TextMeshProUGUI>(); // Get TextMeshProUGUI component
        return null;

    }

    public bool checkIndividualRecipe(Vector3 recipe)
    {
        return (clothPieces >= recipe[0] && woodPieces >= recipe[1] && ironPieces >= recipe[2]);
    }

    public bool checkRecipe(string name)
    {
        if (name == "Gladius")
        {
            return (clothPieces >= 0);
        }
        return true;
    }

    public float GetControllerHealth()
    {
        return controllerPlayerHealth;
    }

    void RespawnPlayers()
    {
        if (keyboardDead)
        {
            keyboardPlayer.SetActive(true);
            keyboardPlayer.transform.position = respawnPosition;
            keyboardPlayerHealth = 50f;
            keyboardDead = false;
        }

        if (controllerDead)
        {

            controllerPlayer.SetActive(true);
            controllerPlayer.transform.position = respawnPosition;
            controllerPlayerHealth = 50f;
            controllerDead = false;
        }

        SpectatorCam.SetActive(false);
        respawnTimer = respawnTime;
    }
}
