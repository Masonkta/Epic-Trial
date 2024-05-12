using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UIElements;

public class Tutorial : MonoBehaviour
{
    public gameHandler gameScript;
    public GameObject keyboardPlayer;
    public GameObject controllerPlayer;
    public bool playersCanMove = false;
    public float beginningMovementLockTime = 8f;

    [Header("CraftingTableStuff")]
    public GameObject WalkToTableText;
    public GameObject WalkToTableTextC;
    public bool playerHasTouchedTable = false;
    public List<GameObject> walls;
    public float craftingTableRange = 5f;

    public GameObject Craftingtable;
    public GameObject CraftingTutorial;
    public GameObject CraftingTutorial2;

    [Header("Gladius Pickup")]
    public GameObject gladiusPickup;
    public GameObject WeaponTutorial;
    public GameObject WeaponTutorial2;
    public float gladiusRange = 15f;

    bool firstgladius = true;
    bool gamejuststarted = true;

    private float originalTimeScale; 

    private void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();

        keyboardPlayer = gameScript.keyboardPlayer;
        controllerPlayer = gameScript.controllerPlayer;

        originalTimeScale = Time.timeScale;
        CraftingTutorial.SetActive(false);
        CraftingTutorial2.SetActive(false);
        WeaponTutorial.SetActive(false);
        WeaponTutorial2.SetActive(false);
    }

    void checkPlayers_crafting()
    {
        WalkToTableText.SetActive(Time.timeSinceLevelLoad < beginningMovementLockTime);
        WalkToTableTextC.SetActive(Time.timeSinceLevelLoad < beginningMovementLockTime);

        float dK = Vector3.Distance(Craftingtable.transform.position, keyboardPlayer.transform.position);
        float dC = Vector3.Distance(Craftingtable.transform.position, controllerPlayer.transform.position);
        bool playerKInRange = (dK < craftingTableRange);
        bool playerCInRange = (dC < craftingTableRange);

        CraftingTutorial.SetActive(playerKInRange);
        CraftingTutorial2.SetActive(playerCInRange);

        if ((playerKInRange || playerCInRange) && !playerHasTouchedTable)
        {
            playerHasTouchedTable = true;
            foreach (GameObject wall in walls)
                wall.SetActive(false);
        }
    }

    void checkPlayers_weapons()
    { 

        float dK = Vector3.Distance(gladiusPickup.transform.position, keyboardPlayer.transform.position);
        float dC = Vector3.Distance(gladiusPickup.transform.position, controllerPlayer.transform.position);
        bool playerKInRange = (dK < gladiusRange);
        bool playerCInRange = (dC < gladiusRange);

        if (playerKInRange)
        {
            WeaponTutorial.SetActive(true);
            if (firstgladius)
            {
                playersCanMove = false;
                firstgladius = false;
                StartCoroutine(EnableMovementAfterDelay());
            }
        }
        else
        {
            WeaponTutorial.SetActive(false);

        }

        if (playerCInRange)
        {
            WeaponTutorial2.SetActive(true);

            if (firstgladius)
            {
                playersCanMove = false;
                firstgladius = false;
                StartCoroutine(EnableMovementAfterDelay());

            }
        }
        else
        {
            WeaponTutorial2.SetActive(false);
        }
    }

    IEnumerator EnableMovementAfterDelay()
    {
        
        yield return new WaitForSeconds(6f);

        
        playersCanMove = true;
    }

    void Update()
    {
        if (gamejuststarted)
        {
            if (Time.timeSinceLevelLoad > beginningMovementLockTime)
            {
                playersCanMove = true;
                gamejuststarted = false;
            }
        }
        if (gladiusPickup != null)
        {
            checkPlayers_weapons();
        }
        else
        {
            WeaponTutorial.SetActive(false);
            WeaponTutorial2.SetActive(false);
        }
        checkPlayers_crafting();
    }
}
