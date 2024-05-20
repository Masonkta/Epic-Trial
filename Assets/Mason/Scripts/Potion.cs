using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public GameObject poisonPrefab;
    public GameObject dashPrefab;
    gameHandler gameScript;
    Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            throwpoisonpotionkeyboard();

        }
        if (Input.GetKey(KeyCode.R))
        {
            throwdashpotionkeyboard();
        }
    } 
    public void throwpoisonpotion()
    {

        if (gameScript.controllerPlayerPoisonPotion == true)
        {
            GameObject potion = Instantiate(gameScript.DashPrefab, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            potion.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;
            gameScript.controllerPlayerPoisonPotion = false;
        }
        else return;
    }
    public void throwdashpotion()
    {
        if (gameScript.controllerPlayerDashPotion)
        {
            GameObject potion = Instantiate(gameScript.DashPrefab, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            potion.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;
            gameScript.controllerPlayerDashPotion = false;
        }
        else return;
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
            StartCoroutine(DestroyAfterDelay(2f, currentPotion));
            
        }
    }
    IEnumerator DestroyAfterDelay(float delay, GameObject obj)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
}
