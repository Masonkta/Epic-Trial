using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    private bool buff;
    private bool tired;
    private bool piercing;
    public int Damage;
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
            if (tag == ("Gladius"))
            {
                piercing = false;
                if (buff == true)
                {
                    Damage = 10;
                }
                else
                {
                    Damage = 5;
                }
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (piercing == true)
                {
                    enemy.EnemyHealth -= Damage;
                    Debug.Log("Dealt " + Damage + " damage to the enemy.");
                }
                else
                {
                    int damageDealt = Mathf.Max(0, Damage - enemy.EnemyDefence);
                    enemy.EnemyHealth -= damageDealt;
                    Debug.Log("Dealt " + damageDealt + " damage to the enemy.");
                }

            }

            if (tag == ("Spear"))
            {
                piercing = true;
                if (buff == true)
                {
                    Damage = 20;
                }
                else
                {
                    Damage = 10;
                }
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (piercing == true)
                {
                    enemy.EnemyHealth -= Damage;
                    Debug.Log("Dealt " + Damage + " damage to the enemy.");
                }
                else
                {
                    int damageDealt = Mathf.Max(0, Damage - enemy.EnemyDefence);
                    enemy.EnemyHealth -= damageDealt;
                    Debug.Log("Dealt " + damageDealt + " damage to the enemy.");

                }
            }
        }
        if (collision.gameObject.CompareTag("MediumEnemy"))
        {
            if (tag == ("Gladius"))
            {
                piercing = false;
                if (buff == true)
                {
                    Damage = 10;
                }
                else
                {
                    Damage = 5;
                }
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (piercing == true)
                {
                    enemy.EnemyHealth -= Damage;
                    Debug.Log("Dealt " + Damage + " damage to the enemy.");
                }
                else
                {
                    int damageDealt = Mathf.Max(0, Damage - enemy.EnemyDefence);
                    enemy.EnemyHealth -= damageDealt;
                    Debug.Log("Dealt " + damageDealt + " damage to the enemy.");
                }

            }

            if (tag == ("Spear"))
            {
                piercing = true;
                if (buff == true)
                {
                    Damage = 20;
                }
                else
                {
                    Damage = 10;
                }
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (piercing == true)
                {
                    enemy.EnemyHealth -= Damage;
                    Debug.Log("Dealt " + Damage + " damage to the enemy.");
                }
                else
                {
                    int damageDealt = Mathf.Max(0, Damage - enemy.EnemyDefence);
                    enemy.EnemyHealth -= damageDealt;
                    Debug.Log("Dealt " + damageDealt + " damage to the enemy.");

                }
            }
        }
        if (collision.gameObject.CompareTag("HeavyEnemy"))
        {
            if (tag == ("Gladius"))
            {
                piercing = false;
                if (buff == true)
                {
                    Damage = 10;
                }
                else
                {
                    Damage = 5;
                }
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (piercing == true)
                {
                    enemy.EnemyHealth -= Damage;
                    Debug.Log("Dealt " + Damage + " damage to the enemy.");
                }
                else
                {
                    int damageDealt = Mathf.Max(0, Damage - enemy.EnemyDefence);
                    enemy.EnemyHealth -= damageDealt;
                    Debug.Log("Dealt " + damageDealt + " damage to the enemy.");
                }

            }

            if (tag == ("Spear"))
            {
                piercing = true;
                if (buff == true)
                {
                    Damage = 20;
                }
                else
                {
                    Damage = 10;
                }
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (piercing == true)
                {
                    enemy.EnemyHealth -= Damage;
                    Debug.Log("Dealt " + Damage + " damage to the enemy.");
                }
                else
                {
                    int damageDealt = Mathf.Max(0, Damage - enemy.EnemyDefence);
                    enemy.EnemyHealth -= damageDealt;
                    Debug.Log("Dealt " + damageDealt + " damage to the enemy.");

                }
            }
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            if (tag == ("Gladius"))
            {
                piercing = false;
                if (buff == true)
                {
                    Damage = 10;
                }
                else
                {
                    Damage = 5;
                }
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (piercing == true)
                {
                    enemy.EnemyHealth -= Damage;
                    Debug.Log("Dealt " + Damage + " damage to the enemy.");
                }
                else
                {
                    int damageDealt = Mathf.Max(0, Damage - enemy.EnemyDefence);
                    enemy.EnemyHealth -= damageDealt;
                    Debug.Log("Dealt " + damageDealt + " damage to the enemy.");
                }

            }

            if (tag == ("Spear"))
            {
                piercing = true;
                if (buff == true)
                {
                    Damage = 20;
                }
                else
                {
                    Damage = 10;
                }
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (piercing == true)
                {
                    enemy.EnemyHealth -= Damage;
                    Debug.Log("Dealt " + Damage + " damage to the enemy.");
                }
                else
                {
                    int damageDealt = Mathf.Max(0, Damage - enemy.EnemyDefence);
                    enemy.EnemyHealth -= damageDealt;
                    Debug.Log("Dealt " + damageDealt + " damage to the enemy.");

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        string tag = gameObject.tag;
    }
}
