using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

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
    public bool firstEnemyKilled = false;
    public float timeFirstEnemyIsKilled;

    bool GladiusReadyToBeInspected = true;

    [Header("Jumping Tips + Stuff")]
    public float jumpingTipsDelay = 3f;
    public GameObject jumpTip;
    public GameObject jumpTip2;
    public GameObject airDashTip;
    public GameObject airDashTip2;







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

    void Update()
    {
        checkInitialMovement();

        walkUpToCraftingTableAndDropWalls();

        firstEnemyNSwordStuff();

        learnDoubleJump();
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

    void walkUpToCraftingTableAndDropWalls()
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
                freezePlayers(gladiusPickup);
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
                freezePlayers(gladiusPickup);
                StartCoroutine(EnableMovementAfterDelay(pickupAweTime));
            }
        }
        else
            WeaponTutorial2.SetActive(false);
    }

    void firstEnemyNSwordStuff()
    {
        if (gladiusPickup != null)
        {
            checkPlayers_weapons();
        }
        else if (!gladiusPickedUp)
        {
            gladiusPickedUp = true;
            WeaponTutorial.SetActive(false);
            WeaponTutorial2.SetActive(false);
            StartCoroutine(EnableFirstEnemy());
        }

        if (gladiusPickedUp && !firstEnemyByRock && !firstEnemyKilled) // First Enemy Killed
        {
            firstEnemyKilled = true; timeFirstEnemyIsKilled = Time.time;
            playersCanJump = true;        // PLAYERS CAN NOW JUMP
            jumpTip.SetActive(true);
            jumpTip2.SetActive(true);
            //print("You can now jump");

        }
    }

    void learnDoubleJump()
    {
        if (firstEnemyKilled)
        {
            if (!playersCanAirDash && Time.time > timeFirstEnemyIsKilled + jumpingTipsDelay)
            {
                jumpTip.SetActive(false);  airDashTip.SetActive(true);
                jumpTip2.SetActive(false); airDashTip2.SetActive(true);
                playersCanAirDash = true;
            }

            if (airDashTip.activeInHierarchy && Time.time > timeFirstEnemyIsKilled + jumpingTipsDelay * 2f)
            {
                airDashTip.SetActive(true);
                airDashTip2.SetActive(true);
            }
        }
    }

    //////////////////////////////////////////////

    void freezePlayers()
    {
        playersCanMove = false;

        keyboardPlayer.GetComponent<playerMTutorial>().forwardMoveAm = 0f;
        keyboardPlayer.GetComponent<playerMTutorial>().sideMoveAm = 0f;
        keyboardPlayer.GetComponent<playerMTutorial>().horizontalTurnAmount = 0f;
        keyboardPlayer.GetComponent<playerMTutorial>().verticalTurnAmount = 0f;
        keyboardPlayer.GetComponent<playerMTutorial>().playerVelocity = Vector3.zero;
        keyboardPlayer.GetComponent<playerMTutorial>().isDodging = false;

        controllerPlayer.GetComponent<playerMTutorial>().forwardMoveAm = 0f;
        controllerPlayer.GetComponent<playerMTutorial>().sideMoveAm = 0f;
        controllerPlayer.GetComponent<playerMTutorial>().horizontalTurnAmount = 0f;
        controllerPlayer.GetComponent<playerMTutorial>().verticalTurnAmount = 0f;
        controllerPlayer.GetComponent<playerMTutorial>().playerVelocity = Vector3.zero;
        controllerPlayer.GetComponent<playerMTutorial>().isDodging = false;
    }

    void freezePlayers(GameObject focusOn)
    {
        playersCanMove = false;

        keyboardPlayer.GetComponent<playerMTutorial>().forwardMoveAm = 0f;
        keyboardPlayer.GetComponent<playerMTutorial>().sideMoveAm = 0f;
        keyboardPlayer.GetComponent<playerMTutorial>().horizontalTurnAmount = 0f;
        keyboardPlayer.GetComponent<playerMTutorial>().verticalTurnAmount = 0f;
        keyboardPlayer.GetComponent<playerMTutorial>().playerVelocity = Vector3.zero;
        keyboardPlayer.GetComponent<playerMTutorial>().isDodging = false;
        keyboardPlayer.GetComponent<playerMTutorial>().overrideCamera = true;
        keyboardPlayer.GetComponent<playerMTutorial>().cameraTransform.LookAt(focusOn.transform.position);

        controllerPlayer.GetComponent<playerMTutorial>().forwardMoveAm = 0f;
        controllerPlayer.GetComponent<playerMTutorial>().sideMoveAm = 0f;
        controllerPlayer.GetComponent<playerMTutorial>().horizontalTurnAmount = 0f;
        controllerPlayer.GetComponent<playerMTutorial>().verticalTurnAmount = 0f;
        controllerPlayer.GetComponent<playerMTutorial>().playerVelocity = Vector3.zero;
        controllerPlayer.GetComponent<playerMTutorial>().isDodging = false;
        controllerPlayer.GetComponent<playerMTutorial>().overrideCamera = true;
        controllerPlayer.GetComponent<playerMTutorial>().cameraTransform.LookAt(focusOn.transform.position);
    }

    IEnumerator EnableMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        playersCanMove = true;
        keyboardPlayer.GetComponent<playerMTutorial>().overrideCamera = false;
        controllerPlayer.GetComponent<playerMTutorial>().overrideCamera = false;
    }


    IEnumerator EnableFirstEnemy()
    {
        yield return new WaitForSeconds(0.7f);

        if (firstEnemyByRock) firstEnemyByRock.SetActive(true);
    }


}
