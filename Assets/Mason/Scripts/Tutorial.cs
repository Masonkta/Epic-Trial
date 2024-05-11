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
    public GameObject CraftingTutorial1;

    public GameObject Weapon;
    public GameObject WeaponTutorial;
    public GameObject WeaponTutorial1;

    private float originalTimeScale; 

    private void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();

        keyboardPlayer = gameScript.keyboardPlayer;
        controllerPlayer = gameScript.controllerPlayer;

        originalTimeScale = Time.timeScale;
        CraftingTutorial.SetActive(false);
        CraftingTutorial1.SetActive(false);
        WeaponTutorial.SetActive(false);
        WeaponTutorial1.SetActive(false);
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
            CraftingTutorial1.SetActive(true);
        }
        else
        {
            CraftingTutorial1.SetActive(false);

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
            WeaponTutorial1.SetActive(true);
        }
        else
        {
            WeaponTutorial1.SetActive(false);

        }
    }

    void Update()
    {
        if (Weapon != null)
        {
            checkPlayers_weapons();
        }
        else
        {
            WeaponTutorial.SetActive(false);
            WeaponTutorial1.SetActive(false);
        }
        checkPlayers_crafting();
    }
}
