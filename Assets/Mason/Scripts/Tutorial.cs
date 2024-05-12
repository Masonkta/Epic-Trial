using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UIElements;

public class Tutorial : MonoBehaviour
{
    public gameHandler gameScript;
    public GameObject keyboardPlayer;
    public GameObject controllerPlayer;
    public bool playersCanMove = false;
    public float beginningMovementLockTime = 8f;
    public bool playersCanJump;
    public bool playersCanAirDash;
    public bool playersCanDodge;

    bool initialMovementGiven = false;

    [Header("CraftingTableStuff")]
    public GameObject WalkToTableText;
    public GameObject WalkToTableTextC;
    public List<GameObject> walls;
    public float craftingTableRange = 5f;
    public GameObject Craftingtable;

    bool playerHasTouchedTable = false;

    [Header("Sword Pickup")]
    public GameObject gladiusPickup;
    public GameObject WeaponTutorial;
    public GameObject WeaponTutorial2;
    public float pickupAweTime = 6f;
    public float gladiusRange = 15f;
    public bool gladiusPickedUp;
    public GameObject firstEnemyByRock;

    bool GladiusReadyToBeInspected = true;



    




    private float originalTimeScale; 

    private void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();

        keyboardPlayer = gameScript.keyboardPlayer;
        controllerPlayer = gameScript.controllerPlayer;

        originalTimeScale = Time.timeScale;
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


        if ((playerKInRange || playerCInRange) && !playerHasTouchedTable)
        {
            playerHasTouchedTable = true;
            foreach (GameObject wall in walls) // Make Walls Drop
                wall.GetComponent<descend>().droppingStarted = true;
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
            if (GladiusReadyToBeInspected)
            {
                GladiusReadyToBeInspected = false;
                freezePlayers();
                StartCoroutine(EnableMovementAfterDelay(pickupAweTime));
            }
        }
        else
            WeaponTutorial.SetActive(false);



        if (playerCInRange)
        {
            WeaponTutorial2.SetActive(true);

            if (GladiusReadyToBeInspected)
            {
                
                GladiusReadyToBeInspected = false;
                freezePlayers();
                StartCoroutine(EnableMovementAfterDelay(pickupAweTime));

            }
        }
        else
            WeaponTutorial2.SetActive(false);
    }



    void Update()
    {
        checkInitialMovement();
        

        
        checkPlayers_crafting();

        gladiusStuff();
    }

    void checkInitialMovement()
    {
        if (!initialMovementGiven)
        {
            if (Time.timeSinceLevelLoad > beginningMovementLockTime)
            {
                playersCanMove = true;
                initialMovementGiven = true;
            }
        }
    }

    void gladiusStuff()
    {
        if (gladiusPickup != null)
        {
            checkPlayers_weapons();
        }
        else
        {
            gladiusPickedUp = true;
            WeaponTutorial.SetActive(false);
            WeaponTutorial2.SetActive(false);
            StartCoroutine(EnableFirstEnemy());
        }
    }

    void freezePlayers()
    {
        playersCanMove = false;

        keyboardPlayer.GetComponent<playerMTutorial>().forwardMoveAm = 0f;
        keyboardPlayer.GetComponent<playerMTutorial>().sideMoveAm = 0f;
        keyboardPlayer.GetComponent<playerMTutorial>().horizontalTurnAmount = 0f;
        keyboardPlayer.GetComponent<playerMTutorial>().verticalTurnAmount = 0f;
        keyboardPlayer.GetComponent<playerMTutorial>().playerVelocity = Vector3.zero;

        controllerPlayer.GetComponent<playerMTutorial>().forwardMoveAm = 0f;
        controllerPlayer.GetComponent<playerMTutorial>().sideMoveAm = 0f;
        controllerPlayer.GetComponent<playerMTutorial>().horizontalTurnAmount = 0f;
        controllerPlayer.GetComponent<playerMTutorial>().verticalTurnAmount = 0f;
        keyboardPlayer.GetComponent<playerMTutorial>().playerVelocity = Vector3.zero;
    }

    IEnumerator EnableMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        playersCanMove = true;
    }

    IEnumerator EnableFirstEnemy()
    {
        yield return new WaitForSeconds(1f);

        if (firstEnemyByRock) firstEnemyByRock.SetActive(true);
    }
}
