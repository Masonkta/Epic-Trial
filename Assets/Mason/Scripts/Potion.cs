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
        if (Input.GetKeyDown(KeyCode.V))
        {
            throwdashpotionkeyboard();
            throwpoisonpotionkeyboard();

        }
    } 
    public void throwpoisonpotion()
    {
        if (gameScript.controllerPlayerpoisonPotions)
        {
            GameObject currentPotion = Instantiate(poisonPrefab, transform.position + Vector3.up + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
            currentPotion.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);
            currentPotion.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 22f;
            gameScript.controllerPlayerpoisonPotions = false;
        }
    }
    public void throwdashpotion()
    {
        if (gameScript.controllerPlayerdashPotions)
        {
            GameObject currentPotion = Instantiate(dashPrefab, transform.position + Vector3.up + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
            currentPotion.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);
            currentPotion.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 22f;
            gameScript.controllerPlayerdashPotions = false;
        }
    }
    public void throwdashpotionkeyboard()
    {
        if (gameScript.keyboardPlayerpoisonPotions)
        {
            GameObject currentPotion = Instantiate(dashPrefab, transform.position + Vector3.up + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
            currentPotion.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);
            currentPotion.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 22f;
            gameScript.keyboardPlayerdashPotions = false;
        }
    }
    public void throwpoisonpotionkeyboard()
    {
        if (gameScript.keyboardPlayerdashPotions)
        {
            GameObject currentPotion = Instantiate(poisonPrefab, transform.position + Vector3.up + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            float angle = Random.Range(0, Mathf.PI * 2); float mag = Random.Range(2f, 5f);
            currentPotion.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(angle) * mag, 10f, Mathf.Cos(angle) * mag);
            currentPotion.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 22f;
            gameScript.keyboardPlayerpoisonPotions = false;
        }
    }
}
