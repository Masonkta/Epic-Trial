using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion2 : MonoBehaviour
{
    public GameObject poisonPrefab;
    public GameObject dashPrefab;
    gameHandler gameScript;
    playerAccessWeapons player2;
    GameObject p2gladiuscheck;
    GameObject p2clubcheck;
    Weapon p2weapon;
    bool fist;

    // Start is called before the first frame update
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
        player2 = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<playerAccessWeapons>();
        p2gladiuscheck = player2.gladius;
        p2clubcheck = player2.club;
    }

    private void Update()
    {
        if (p2gladiuscheck.activeInHierarchy)
        {
            p2weapon = p2gladiuscheck.GetComponent<Weapon>();
            fist = false;
        }
        if (p2clubcheck.activeInHierarchy)
        {
            p2weapon = p2clubcheck.GetComponent<Weapon>();
            fist = false;
        }
        if (!p2clubcheck.activeInHierarchy && !p2gladiuscheck.activeInHierarchy)
        {
            fist = true;
        }
    }
    void Onthrowpoisonpotion()
    {
        if (gameScript.controllerPlayerPoisonPotion && !fist)
        {
            GameObject currentPotion = Instantiate(gameScript.DashPrefab, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            currentPotion.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;
            gameScript.controllerPlayerPoisonPotion = false;
            StartCoroutine(DestroyAfterDelay(1f, currentPotion));
            if (p2weapon != null)
            {
                p2weapon.applypoison();
            }
        }
        else return;
    }
    void Onthrowdashpotion()
    {
        if (gameScript.controllerPlayerDashPotion)
        {
            GameObject currentPotion = Instantiate(gameScript.DashPrefab, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            currentPotion.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;
            gameScript.controllerPlayerDashPotion = false;
            StartCoroutine(DestroyAfterDelay(1f, currentPotion));
            gameScript.playerCUsingDashPotion = true;
            StartCoroutine(DashEffect(10f));
        }
        else return;
    }

    IEnumerator DestroyAfterDelay(float delay, GameObject obj)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
    IEnumerator DashEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameScript.playerCUsingDashPotion = false;
    }
}
