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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (tag == ("Starter Sword")) {
                piercing = false;
                if (buff == true) {
                    Damage = 10;
                }
                else {
                    Damage = 5;
                }
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (piercing == true)
                {
                    enemy.EnemyHealth -= (Damage);
                }
                else
                {
                    enemy.EnemyHealth -= (Damage - enemy.EnemyDefence);
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
                    enemy.EnemyHealth -= (Damage);
                }
                else
                {
                    enemy.EnemyHealth -= (Damage - enemy.EnemyDefence);
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
