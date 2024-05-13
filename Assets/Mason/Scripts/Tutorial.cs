using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

//using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UIElements;

public class Tutorial : MonoBehaviour
{
    public bool doEverythingInstantly;
    public gameHandler gameScript;
    public GameObject keyboardPlayer;
    public GameObject controllerPlayer;
    public bool playersCanMove = false;
    public float beginningMovementLockTime = 8f;
    public bool playersCanDodge;
    public bool playersCanShiftLock;
    public bool playersCanJump;
    public bool playersCanAirDash;
    public bool playersCanThrowWeapons;

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
    public GameObject shiftLockTip;
    public GameObject shiftLockTip2;
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

    [Header("Sand Area")]
    bool playerKOnPlatform;
    bool playerCOnPlatform;
    public bool bothPlayersOnPlatform = false;
    public GameObject closingFenceForSandPlatform;
    public GameObject WeaponThrowTip;
    public GameObject WeaponThrowTip2;

    [Header("On Sand Area")]
    public GameObject newWeaponDropTip;
    public GameObject newWeaponDropTip2;
    public GameObject newSword;
    public float stareAtNewSwordTime = 3f;
    public bool newSwordPickedUp = false;




    private float originalTimeScale; 

    private void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
        keyboardPlayer = gameScript.keyboardPlayer;
        controllerPlayer = gameScript.controllerPlayer;

        if (doEverythingInstantly)
        {
            beginningMovementLockTime = 0f;
            pickupAweTime = 0f;
            jumpingTipsDelay = 0f;
        }
    }

    void Update()
    {
        checkInitialMovement();

        walkUpToCraftingTableAndDropWalls();

        firstEnemyNSwordStuff();

        learnDoubleJump();

        initialStuffWhenPlayersGetToSandPlatform();

        sandPlatformTime();
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
                playersCanDodge = true;
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
                playersCanDodge = true;
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
            playersCanShiftLock = true;
            shiftLockTip.SetActive(true);
            shiftLockTip2.SetActive(true);
            StartCoroutine(turnOffShiftLockTip());

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

            // Turn off Shift Lock For Both Players
            keyboardPlayer.GetComponent<playerMTutorial>().shiftLock = false;
            controllerPlayer.GetComponent<playerMTutorial>().shiftLock = false;

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
                airDashTip.SetActive(false);
                airDashTip2.SetActive(false);
            }
        }
    }

    void initialStuffWhenPlayersGetToSandPlatform()
    {
        if (playersCanAirDash)
        {
            playerKOnPlatform = keyboardPlayer.transform.position.y > 12.5f && keyboardPlayer.transform.position.x > -33f;
            playerCOnPlatform = controllerPlayer.transform.position.y > 12.5f && controllerPlayer.transform.position.x > -33f;

            if (!bothPlayersOnPlatform && playerKOnPlatform && playerCOnPlatform) // Both Players Are Now on the platform
            {
                bothPlayersOnPlatform = true;
                closingFenceForSandPlatform.SetActive(true);
                StartCoroutine(waitSecondsAfterGateClose(4.5f));

                playersCanThrowWeapons = true;
                WeaponThrowTip.SetActive(true);
                WeaponThrowTip2.SetActive(true);
                StartCoroutine(turnOffWeaponThrowTip());
            }
        }
    }

    void sandPlatformTime()
    {
        if (bothPlayersOnPlatform)
        {
            if (!newSword && !newSwordPickedUp)
                newSwordPickedUp = true;






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

    IEnumerator enableMvmtNdisblWpnTip(float delay)
    {
        yield return new WaitForSeconds(delay);

        playersCanMove = true;
        keyboardPlayer.GetComponent<playerMTutorial>().overrideCamera = false;
        controllerPlayer.GetComponent<playerMTutorial>().overrideCamera = false;
        newWeaponDropTip.SetActive(false);
        newWeaponDropTip2.SetActive(false);
    }


    IEnumerator EnableFirstEnemy()
    {
        yield return new WaitForSeconds(doEverythingInstantly ? 0f : 0.5f);

        if (firstEnemyByRock) firstEnemyByRock.SetActive(true);
    }

    IEnumerator turnOffShiftLockTip()
    {
        yield return new WaitForSeconds(doEverythingInstantly ? 0f : 3f);

        shiftLockTip.SetActive(false);
        shiftLockTip2.SetActive(false);
    }
    
    IEnumerator turnOffWeaponThrowTip()
    {
        yield return new WaitForSeconds(doEverythingInstantly ? 0f : 3f);

        WeaponThrowTip.SetActive(false);
        WeaponThrowTip2.SetActive(false);
    }
    
    IEnumerator waitSecondsAfterGateClose(float delay)
    {

        yield return new WaitForSeconds(doEverythingInstantly ? 0f : delay);


        newWeaponDropTip.SetActive(true);
        newWeaponDropTip2.SetActive(true);
        Vector3 avgPos = keyboardPlayer.transform.position / 2f + controllerPlayer.transform.position / 2f;
        newSword.SetActive(true);
        newSword.transform.position = avgPos;
        newSword.GetComponent<Rigidbody>().velocity = Vector3.up * 18f;
        newSword.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitCircle * 50f;

        freezePlayers(newSword);
        StartCoroutine(enableMvmtNdisblWpnTip(stareAtNewSwordTime));
    }


}
