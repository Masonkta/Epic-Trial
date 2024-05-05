using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum WeaponType
{
    Gladius,
    Spear,
    Club,
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
        // Check if collided object is an enemy
        if (!collision.gameObject.CompareTag("Enemy"))
            return;

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (buff)
            Damage *= 2;

        // Calculate damage based on weapon type
        switch (type)
        {
            case WeaponType.Gladius:
                Damage = 100;
                piercing = false;
                break;
            case WeaponType.Spear:
                Damage = 100;
                piercing = true;
                break;
            case WeaponType.Club:
                Damage = 100;
                piercing = false;
                break;
        }

        // Apply damage
        int damageDealt = Mathf.Max(0, Damage - enemy.EnemyDefence);
        if (piercing)
        {
            enemy.EnemyHealth -= Damage;
        }
        else
        {
            enemy.EnemyHealth -= damageDealt;
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
