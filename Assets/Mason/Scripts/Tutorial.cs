using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject craftMenuK;
    public GameObject craftMenuC;
    public GameObject WalkToTableText;
    public GameObject WalkToTableTextC;
    public List<GameObject> walls;
    public bool FIRSTWEAPONATTENTION = false;
    public float craftingTableRange = 5f;
    public GameObject Craftingtable;

    bool playerHasTouchedTable = false;

    [Header("Sword Pickup")]
    public GameObject walkToRockTip;
    public GameObject walkToRockTip2;
    public GameObject gladiusPickup;
    public GameObject WeaponTutorial;
    public GameObject WeaponTutorial2;
    public float pickupAweTime = 6f;
    public float gladiusRange = 15f;
    public GameObject shiftLockTip;
    public GameObject shiftLockTip2;
    public bool gladiusPickedUp;
    public AudioSource swordDrawSource;
    public GameObject firstEnemyByRock;
    public bool firstEnemyKilled = false;
    public float timeFirstEnemyIsKilled;
    public float shiftLockTipTime = 3.8f;

    bool GladiusReadyToBeInspected = true;

    [Header("Jumping Tips + Stuff")]
    public float jumpingTipsDelay = 3f;
    public GameObject jumpTip;
    public GameObject jumpTip2;
    public GameObject airDashTip;
    public GameObject airDashTip2;

    [Header("Sand Area")]
    public GameObject walkToSandTip;
    public GameObject walkToSandTip2;
    public float timeOfOpening;
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
    public GameObject SbenemyHeavyK;
    public GameObject SbenemyHeavyC;
    public bool secondWaveDone = false;

    [Header("Farming Time")]
    public GameObject letsGetResourcesTxt;
    public GameObject letsGetResourcesTxt2;
    public float resourceTipTimer = 4.6f;

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
        craftMenuK.SetActive(playerKInRange && !FIRSTWEAPONATTENTION);
        craftMenuC.SetActive(playerCInRange && !FIRSTWEAPONATTENTION);

        if ((playerKInRange || playerCInRange) && !playerHasTouchedTable)
        {
            playerHasTouchedTable = true;
            foreach (GameObject wall in walls) // Make Walls Drop
            {
                wall.GetComponent<descend>().droppingStarted = true;
                wall.GetComponent<descend>().timeOfDrop = Time.time;
            }
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
        else if (!gladiusPickedUp) // Gladius was pulled from sword
        {
            swordDrawSource.PlayOneShot(swordDrawSource.clip);
            playersCanShiftLock = true;
            shiftLockTip.SetActive(true);
            shiftLockTip2.SetActive(true);
            StartCoroutine(turnOffShiftLockTip());

            gladiusPickedUp = true;
            playersCanDodge = true;
            WeaponTutorial.SetActive(false);
            WeaponTutorial2.SetActive(false);
            StartCoroutine(EnableFirstEnemy());
        }

        if (gladiusPickedUp && !firstEnemyByRock && !firstEnemyKilled) // First Enemy Killed
        {
            firstEnemyKilled = true; timeFirstEnemyIsKilled = Time.time;
            playersCanJump = true;
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
            if (!playersCanAirDash && Time.time > (doEverythingInstantly ? 0f : (timeFirstEnemyIsKilled + jumpingTipsDelay)))
            {
                jumpTip.SetActive(false);  airDashTip.SetActive(true);
                jumpTip2.SetActive(false); airDashTip2.SetActive(true);
                playersCanAirDash = true;
            }

            if (airDashTip.activeInHierarchy && Time.time > (doEverythingInstantly ? 0f : (timeFirstEnemyIsKilled + jumpingTipsDelay * 2f)))
            {
                airDashTip.SetActive(false);
                airDashTip2.SetActive(false); // Can now double jump, get up to sand platform
                timeOfOpening = Time.time;
            }
        }
    }

    void initialStuffWhenPlayersGetToSandPlatform()
    {
        if (playersCanAirDash)
        {
            if (!bothPlayersOnPlatform && !airDashTip.activeSelf)
            {
                walkToSandTip.SetActive(true);
                walkToSandTip2.SetActive(true);
                walkToSandTip.GetComponent<TextMeshProUGUI>().text = "Get On to the sand platform";
                walkToSandTip2.GetComponent<TextMeshProUGUI>().text = "Get On to the sand platform";
            }
            playerKOnPlatform = keyboardPlayer.transform.position.y > 12.5f && keyboardPlayer.transform.position.x > -33f;
            playerCOnPlatform = controllerPlayer.transform.position.y > 12.5f && controllerPlayer.transform.position.x > -33f;

            if (!bothPlayersOnPlatform && playerKOnPlatform && playerCOnPlatform) // Both Players Are Now on the platform
            {
                bothPlayersOnPlatform = true;
                walkToSandTip.SetActive(false);
                walkToSandTip2.SetActive(false);

                closingFenceForSandPlatform.SetActive(true);
                StartCoroutine(waitSecondsAfterGateClose(4.5f));

                playersCanThrowWeapons = true;
                WeaponThrowTip.SetActive(true);
                WeaponThrowTip2.SetActive(true);
                StartCoroutine(turnOffWeaponThrowTip());
            }

            if (!bothPlayersOnPlatform)
            {
                if (!airDashTip.activeSelf)
                {
                    walkToSandTip.SetActive(!playerKOnPlatform);
                    walkToSandTip2.SetActive(!playerCOnPlatform);
                }

                if (Time.time > timeOfOpening + 10f)
                {
                    walkToSandTip.GetComponent<TextMeshProUGUI>().text = "Remember to Air Dash by double jumping";
                    walkToSandTip2.GetComponent<TextMeshProUGUI>().text = "Remember to Air Dash by double jumping";
                }

                if (Time.time > timeOfOpening + 20f)
                {
                    walkToSandTip.GetComponent<TextMeshProUGUI>().text = "try making sure you are sprinting as well";
                    walkToSandTip2.GetComponent<TextMeshProUGUI>().text = "try making sure you are sprinting as well";
                }
            }
        }

    }

    void sandPlatformTime()
    {
        if (bothPlayersOnPlatform)
        {
            if (!newSword && !newSwordPickedUp) // Club is picked up
            {
                newSwordPickedUp = true;
                StartCoroutine(SpawnSomeMoreEnemiesAfterDelay());
            }

            if (!SbenemyHeavyK && !SbenemyHeavyC && !secondWaveDone)
            {
                secondWaveDone = true;

                letsGetResourcesTxt.SetActive(true);
                letsGetResourcesTxt2.SetActive(true);
                StartCoroutine(turnOffNeedResourcesUI());
                StartCoroutine(healPlayers());
            }


        }
    }





    ////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////






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

    IEnumerator EnableMovementAfterDelayNDisabeAHGLADIUS(float delay)
    {
        yield return new WaitForSeconds(delay);

        playersCanMove = true;
        keyboardPlayer.GetComponent<playerMTutorial>().overrideCamera = false;
        controllerPlayer.GetComponent<playerMTutorial>().overrideCamera = false;
        walkToRockTip.SetActive(false);
        walkToRockTip2.SetActive(false);
        FIRSTWEAPONATTENTION = false;
    }

    IEnumerator EnableMovementAfterDelaySand(float delay)
    {
        yield return new WaitForSeconds(delay);
        walkToRockTip.SetActive(false);
        walkToRockTip2.SetActive(false);
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
        newSword.GetComponent<ClubPickup>().enabled = true;
    }

    public void wallsAreDone()
    {
        walkToRockTip.SetActive(true);
        walkToRockTip2.SetActive(true);
        FIRSTWEAPONATTENTION = true;

        freezePlayers(gladiusPickup);
        StartCoroutine(EnableMovementAfterDelayNDisabeAHGLADIUS(pickupAweTime));
    }

    IEnumerator EnableFirstEnemy()
    {
        yield return new WaitForSeconds(0.5f);

        if (firstEnemyByRock) firstEnemyByRock.SetActive(true);
    }

    IEnumerator SpawnSomeMoreEnemiesAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);

        SbenemyHeavyK.SetActive(true);
        SbenemyHeavyC.SetActive(true);
        SbenemyHeavyK.transform.position = keyboardPlayer.transform.position + Vector3.up + Random.insideUnitSphere * 2f;
        SbenemyHeavyC.transform.position = controllerPlayer.transform.position + Vector3.up + Random.insideUnitSphere * 2f;

    }

    IEnumerator turnOffShiftLockTip()
    {
        yield return new WaitForSeconds(doEverythingInstantly ? 0f : shiftLockTipTime);

        Time.timeScale = 1f;
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
        newSword.GetComponent<ClubPickup>().enabled = false;

        freezePlayers(newSword);
        StartCoroutine(enableMvmtNdisblWpnTip(stareAtNewSwordTime));
    }


    IEnumerator turnOffNeedResourcesUI()
    {
        yield return new WaitForSeconds(doEverythingInstantly ? 0f : resourceTipTimer);

        letsGetResourcesTxt.SetActive(false);
        letsGetResourcesTxt2.SetActive(false);
    }
    IEnumerator healPlayers()
    {
        yield return new WaitForSeconds(doEverythingInstantly ? 0f : resourceTipTimer * 1.3f);

        Vector3 middle = keyboardPlayer.transform.position / 2f + controllerPlayer.transform.position / 2f;

        for (int i = 0; i < 200; i++)
        {
            GameObject currentCloth = Instantiate(gameScript.bandagesPrefab, middle + Vector3.up + Random.insideUnitSphere, Random.rotation, gameScript.ResourceTransform);
            float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
            currentCloth.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);
            currentCloth.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 22f;
        }
    }

}
