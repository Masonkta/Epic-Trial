using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum WeaponType
{
    Gladius,
    Spear,
}

public class Weapon : MonoBehaviour
{
    private bool buff;
    private bool tired;
    private bool piercing;
    public int Damage;
    public WeaponType type;


    // Start is called before the first frame update
    void Start()
    {
        buff = false;
    }

    private IEnumerator buffed()
    {
        if (tired == false)
        {
            buff = true;
            yield return new WaitForSeconds(10);
            buff = false;
        }
        tired = true;
        yield return new WaitForSeconds(60);
        tired = false;
        yield break;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (WeaponType.Gladius == type)
            {
                Damage = 5;
                piercing = false;
                if (buff == true)  Damage *= 2;
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (piercing == true)
                {
                    enemy.EnemyHealth -= Damage;
                    //Debug.Log("Dealt " + Damage + " damage to the " + enemy.Etype.ToString() + " enemy.");
                }
                else
                {
                    int damageDealt = Mathf.Max(0, Damage - enemy.EnemyDefence);
                    enemy.EnemyHealth -= damageDealt;
                    //Debug.Log("Dealt " + damageDealt + " damage to the " + enemy.Etype.ToString() + " enemy.");
                }

            }

            if (WeaponType.Spear == type)
            {
                Damage = 10;
                piercing = true;
                if (buff == true)  Damage *= 2;
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (piercing == true)
                {
                    enemy.EnemyHealth -= Damage;
                    //Debug.Log("Dealt " + Damage + " damage to the " + enemy.Etype.ToString() + " enemy.");
                }
                else
                {
                    int damageDealt = Mathf.Max(0, Damage - enemy.EnemyDefence);
                    enemy.EnemyHealth -= damageDealt;
                    //Debug.Log("Dealt " + damageDealt + " damage to the " + enemy.Etype.ToString() +  " enemy.");

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnKeyboardBuff()
    {
        //print("BUFF");
        buffed();
    }
}
