using System.Collections;
using System.Collections.Generic;
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
        }
        if (p1clubcheck.activeInHierarchy)
        {
            p1weapon = p1clubcheck.GetComponent<Weapon>();
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
        }
        else return;
    }
    public void throwpoisonpotionkeyboard()
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
    }
    IEnumerator DestroyAfterDelay(float delay, GameObject obj)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
}
