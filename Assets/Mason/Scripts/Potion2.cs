using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion2 : MonoBehaviour
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

    void Onthrowpoisonpotion()
    {
        Debug.Log("Got this far");
        if (gameScript.controllerPlayerPoisonPotion)
        {
            GameObject currentPotion = Instantiate(gameScript.DashPrefab, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            currentPotion.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;
            gameScript.controllerPlayerPoisonPotion = false;
            StartCoroutine(DestroyAfterDelay(1f, currentPotion));
        }
        else return;
    }
    void Onthrowdashpotion()
    {
        Debug.Log("Got this far");
        if (gameScript.controllerPlayerDashPotion)
        {
            GameObject currentPotion = Instantiate(gameScript.DashPrefab, transform.position + Vector3.up * 4f + Random.insideUnitSphere, Quaternion.identity, gameScript.ResourceTransform);
            currentPotion.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;
            gameScript.controllerPlayerDashPotion = false;
            StartCoroutine(DestroyAfterDelay(1f, currentPotion));
        }
        else return;
    }

    IEnumerator DestroyAfterDelay(float delay, GameObject obj)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
}
