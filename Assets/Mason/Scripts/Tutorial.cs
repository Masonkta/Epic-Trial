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
    public float range = 5f;
    public float beginningMovementLockTime = 8f;

    [Header("CraftingTableStuff")]
    public GameObject WalkToTableText;
    public GameObject WalkToTableTextC;
    public bool playerHasTouchedTable = false;
    public List<GameObject> walls;

    public GameObject Craftingtable;
    public GameObject CraftingTutorial;
    public GameObject CraftingTutorial2;

    [Header("Weapon")]
    public GameObject Weapon;
    public GameObject WeaponTutorial;
    public GameObject WeaponTutorial2;

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
        bool playerKInRange = (dK < range);
        bool playerCInRange = (dC < range);

        CraftingTutorial.SetActive(playerKInRange);
        CraftingTutorial2.SetActive(playerCInRange);

        if ((playerKInRange || playerCInRange) && !playerHasTouchedTable)
        {
            playerHasTouchedTable = true;
            foreach (GameObject wall in walls)
                wall.SetActive(false);
        }
    }

    /*
    void checkPlayers_weapons()
    {
        float dK = Vector3.Distance(Weapon.transform.position, keyboardPlayer.transform.position);
        float dC = Vector3.Distance(Weapon.transform.position, controllerPlayer.transform.position);
        bool playerKInRange = (dK < range);
        bool playerCInRange = (dC < range);

        if (playerKInRange)
        {
            WeaponTutorial.SetActive(true);
        }
        else
        {
            WeaponTutorial.SetActive(false);

        }

        if (playerCInRange)
        {
            WeaponTutorial2.SetActive(true);
        }
        else
        {
            WeaponTutorial2.SetActive(false);

        }
    }*/

    void Update()
    {
        playersCanMove = Time.timeSinceLevelLoad > beginningMovementLockTime;
        /*if (Weapon != null)
        {
            checkPlayers_weapons();
        }
        else
        {
            WeaponTutorial.SetActive(false);
            WeaponTutorial2.SetActive(false);
        }*/
        checkPlayers_crafting();
    }
}
