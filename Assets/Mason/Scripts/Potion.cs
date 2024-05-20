using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public GameObject poisonPrefab;
    public GameObject dashPrefab;
    gameHandler gameScript;

    // Start is called before the first frame update
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            throwpoisonpotionkeyboard();

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            throwdashpotionkeyboard();
        }
    } 
    public void throwpoisonpotion()
    {
        if (gameScript.controllerPlayerPoisonPotion)
        {
            GameObject currentPotion = Instantiate(poisonPrefab, transform.position + Vector3.up + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
            currentPotion.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);
            currentPotion.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 22f;
            gameScript.controllerPlayerPoisonPotion = false;
        }
        else return;
    }
    public void throwdashpotion()
    {
        if (gameScript.controllerPlayerDashPotion)
        {
            GameObject currentPotion = Instantiate(dashPrefab, transform.position + Vector3.up + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
            currentPotion.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);
            currentPotion.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 22f;
            gameScript.controllerPlayerDashPotion = false;
        }
        else return;
    }
    public void throwdashpotionkeyboard()
    {
        if (gameScript.keyboardPlayerPoisonPotion)
        {
            GameObject currentPotion = Instantiate(dashPrefab, transform.position + Vector3.up + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
            currentPotion.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);
            currentPotion.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 22f;
            gameScript.keyboardPlayerPoisonPotion = false;
        }
        else return;
    }
    public void throwpoisonpotionkeyboard()
    {
        if (gameScript.keyboardPlayerDashPotion)
        {
            GameObject currentPotion = Instantiate(poisonPrefab, transform.position + Vector3.up + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
            currentPotion.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);
            currentPotion.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 22f;
            gameScript.keyboardPlayerDashPotion = false;
            StartCoroutine(DestroyAfterDelay(7f));
        }
        else return;
    }
    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
