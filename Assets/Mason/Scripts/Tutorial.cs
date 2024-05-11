using UnityEditor.ShaderGraph;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public gameHandler gameScript;

    public GameObject keyboardPlayer;
    public GameObject controllerPlayer;

    public float range = 5f;

    public bool playerKInRange = false;
    public bool playerCInRange = false;

    public GameObject Craftingtable;
    public GameObject CraftingTutorial;
    public GameObject CraftingTutorial2;

    public GameObject Weapon;
    public GameObject WeaponTutorial;
    public GameObject WeaponTutorial2;

    [Header("Walk to table hint -- 6 seconds")]
    public GameObject tableHint;
    public GameObject tableHint2;

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
        float dK = Vector3.Distance(Craftingtable.transform.position, keyboardPlayer.transform.position);
        float dC = Vector3.Distance(Craftingtable.transform.position, controllerPlayer.transform.position);
        playerKInRange = (dK < range);
        playerCInRange = (dC < range);

        if (playerKInRange)
        {
            CraftingTutorial.SetActive(true);
        }
        else
        {
            CraftingTutorial.SetActive(false);
        }

        if (playerCInRange)
        {
            CraftingTutorial2.SetActive(true);
        }
        else
        {
            CraftingTutorial2.SetActive(false);

        }
    }
    void checkPlayers_weapons()
    {
        float dK = Vector3.Distance(Weapon.transform.position, keyboardPlayer.transform.position);
        float dC = Vector3.Distance(Weapon.transform.position, controllerPlayer.transform.position);
        playerKInRange = (dK < range);
        playerCInRange = (dC < range);

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
    }

    void Update()
    {
        tableHint.SetActive(Time.timeSinceLevelLoad < 6f);
        tableHint2.SetActive(Time.timeSinceLevelLoad < 6f);

        if (Weapon != null)
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
