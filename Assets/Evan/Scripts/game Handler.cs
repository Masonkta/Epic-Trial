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
    public GameObject PoisonPrefab;
    public GameObject DashPrefab;
    public GameObject FeatherPrefab;

    public GameObject gladiusPickup;
    public GameObject clubPickup;

    [Header("Armor Stuff")]
    public GameObject ArmorKPrefab;
    public GameObject playerKsArmor;
    public bool playerKHasArmor;
    public float playerKDmgMult = 1f;

    public GameObject ArmorCPrefab;
    public GameObject playerCsArmor;
    public bool playerCHasArmor;
    public float playerCDmgMult = 1f;

    [Header("Resources")]
    public Transform ResourceTransform;
    public int ResourceDropRate = 3;
    public int gold;
    public int clothPieces;
    public int woodPieces;
    public int ironPieces;
    public int Berries;
    public int Feathers;
    public int keyboardPlayerBandages;
    public int controllerPlayerBandages;
    public bool keyboardPlayerDashPotion;
    public bool keyboardPlayerPoisonPotion;
    public bool controllerPlayerDashPotion;
    public bool controllerPlayerPoisonPotion;
    public bool playerKUsingDashPotion;
    public bool playerCUsingDashPotion;
    public bool playerKUsingPoisonPotion;
    public bool playerCUsingPoisonPotion;

    [Header("Death Cameras")]
    public GameObject BackUpCamera;
    public GameObject BackUpCamera1;
    public GameObject SpectatorCam;
    public GameObject SpectatorCam2;
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
                    SpectatorCam2.SetActive(true);
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

                // Check for armor
                playerKHasArmor = playerKsArmor.activeInHierarchy;
                playerCHasArmor = playerCsArmor.activeInHierarchy;
                playerKDmgMult = playerKHasArmor ? 0.33f : 1f;
                playerCDmgMult = playerCHasArmor ? 0.33f : 1f;
            }

            if (SceneManager.GetActiveScene().name == "The Final")
            {
                if (keyboardPlayerHealth <= 0)
                {
                    keyboardPlayer.SetActive(false);
                    WaitAndChangeToVictory();
                }

                if (controllerPlayerHealth <= 0)
                {
                    controllerPlayer.SetActive(false);
                    WaitAndChangeToVictory();
                }
            }

            _healthbarSpriteK.fillAmount = keyboardPlayerHealth / 100f;
            _healthbarSpriteC.fillAmount = controllerPlayerHealth / 100f;


            if (Input.GetKeyDown(KeyCode.F))
                useKeyboardBandage();
        }
        

        ///////////////////////////////////////////////////////////////////////

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

    public IEnumerator WaitAndChangeVictoryScene()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("victoryScene");
    }

    public void WaitAndChangeScene()
    {
        StartCoroutine(WaitAndChangeScenetask());
    }

    public void WaitAndChangeToVictory()
    {
        StartCoroutine(WaitAndChangeVictoryScene());
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
        if (name == "Feathers") Feathers++;
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

    public void collectKeyboardPoisonPotion()
    {
        keyboardPlayerPoisonPotion = true;
    }

    public void collectControllerPoisonPotion()
    {
        controllerPlayerPoisonPotion = true;
    }

    public void collectKeyboardDashPotion()
    {
        keyboardPlayerDashPotion = true;
    }

    public void collectControllerDashPotion()
    {
        controllerPlayerDashPotion = true;
    }

    public void turnOnArmorK()
    {
        playerKsArmor.SetActive(true);
        print("Player K now has armor");
    }

    public void turnOnArmorC()
    {
        playerCsArmor.SetActive(true);
        print("Player C now has armor");
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

    public bool checkRecipe(string name)
    {
        if (name == "Club")
            return (woodPieces >= 25 && clothPieces >= 10);
        
        if (name == "Gladius")
            return (clothPieces >= 10 && woodPieces >= 10 && ironPieces >= 25);
        
        if (name == "Bandage")
            return (clothPieces >= 10);

        if (name == "Potion Poison")
            return (Berries >= 15);

        if (name == "Potion Dash")
            return (Feathers >= 5);

        if (name == "Armor")
            return (clothPieces >= 15 && Feathers >= 10 && ironPieces >= 50);

        return false;
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
        SpectatorCam2.SetActive(false);
        respawnTimer = respawnTime;
    }
}
