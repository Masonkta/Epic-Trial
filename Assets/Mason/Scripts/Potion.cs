using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public GameObject poisonPrefab;
    public GameObject dashPrefab;
    gameHandler gameScript;
    playerAccessWeapons player1;
    GameObject p1gladiuscheck;
    GameObject p1clubcheck;
    Weapon p1weapon;
    bool fist;
    public GameObject tip;



    // Start is called before the first frame update
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
        player1 = GameObject.FindGameObjectWithTag("PlayerKeyboard").GetComponent<playerAccessWeapons>();
        p1gladiuscheck = player1.gladius;
        p1clubcheck = player1.club;
    }

    void Update()
    {
        if (p1gladiuscheck.activeInHierarchy)
        {
            p1weapon = p1gladiuscheck.GetComponent<Weapon>();
            fist = false;
        }
        if (p1clubcheck.activeInHierarchy)
        {
            p1weapon = p1clubcheck.GetComponent<Weapon>();
            fist = false;
        }
        if (!p1clubcheck.activeInHierarchy && !p1gladiuscheck.activeInHierarchy)
        {
            fist = true;
        }    
        if (Input.GetKey(KeyCode.T))
        {
            throwpoisonpotionkeyboard();
        }
        if (Input.GetKey(KeyCode.R))
        {
            throwdashpotionkeyboard();
        }
    }

    public void throwdashpotionkeyboard()
    {
        if (gameScript.keyboardPlayerDashPotion)
        {
            GameObject currentPotion = Instantiate(dashPrefab, transform.position + Vector3.up + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
            currentPotion.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);
            currentPotion.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 22f;
            gameScript.keyboardPlayerDashPotion = false;
            StartCoroutine(DestroyAfterDelay(1f, currentPotion));
            gameScript.playerKUsingDashPotion = true;
            StartCoroutine(DashEffect(10f));
        }
        else return;
    }
    public void throwpoisonpotionkeyboard()
    {
        if (!fist)
        {
            if (gameScript.keyboardPlayerPoisonPotion)
            {
                GameObject currentPotion = Instantiate(poisonPrefab, transform.position + Vector3.up + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
                float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
                currentPotion.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);
                currentPotion.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 22f;
                gameScript.keyboardPlayerPoisonPotion = false;
                StartCoroutine(DestroyAfterDelay(1f, currentPotion));
                if (p1weapon != null)
                {
                    p1weapon.applypoison();
                }
            }
            else
            {
                return;
            }
            
        }
        else
        {
            tip.SetActive(true);
            StartCoroutine(tipDuration(3f));

        }
    }
    IEnumerator DestroyAfterDelay(float delay, GameObject obj)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
    IEnumerator DashEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameScript.playerKUsingDashPotion = false;
    }
    IEnumerator tipDuration(float delay)
    {
        yield return new WaitForSeconds(delay);
        tip.SetActive(false);
    }
}
