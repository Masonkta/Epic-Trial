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
    public bool wasHit = false;
    public bool isHitting = false;
    public int Damage;
    public WeaponType type;
    public SwordTest Att;
    public AudioSource deflectSound;



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
        if (Att.canAtt == false && isHitting == false)
        {
            // Checks if the collided object is enemy armor
            if (collision.gameObject.CompareTag("armor"))
            {
                if (deflectSound) deflectSound.Play();
                //Debug.Log("Enemy armor deflected!");
                Enemy heavy = collision.gameObject.GetComponent<Enemy>();
                // Calculate damage based on weapon type
                switch (type)
                {
                    case WeaponType.Gladius:
                        Damage = 4; ////////////////////
                        piercing = false;
                        isHitting = true;
                        StartCoroutine(damaging());
                        break;
                    case WeaponType.Spear:
                        Damage = 10;
                        piercing = true;
                        isHitting = true;
                        break;
                    case WeaponType.Club:
                        Damage = 7;
                        piercing = true;
                        isHitting = true;
                        StartCoroutine(damaging());
                        break;
                }

                // Apply damage
                int damageDeflect = Mathf.Max(0, Mathf.RoundToInt(Damage * 0.3f));
                if (piercing)
                {
                    wasHit = true;
                    if (heavy)
                        heavy.EnemyHealth -= Damage;
                    //Debug.Log("HIT");
                    wasHit = false;
                }
                else
                {
                    wasHit = true;
                    if (heavy)
                        heavy.EnemyHealth -= damageDeflect;
                    wasHit = false;
                }
                return;
            }
            else
            { // Check if collided object is an enemy
                if (!collision.gameObject.CompareTag("Enemy"))
                    return;

                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                BasicEnemy BNMY = collision.gameObject.GetComponent<BasicEnemy>();
                //Debug.Log(BNMY);
                if (buff)
                    Damage *= 2;

                // Calculate damage based on weapon type
                switch (type)
                {
                    case WeaponType.Gladius:
                        Damage = 4; ////////////////////
                        piercing = false;
                        isHitting = true;
                        StartCoroutine(damaging());
                        break;
                    case WeaponType.Spear:
                        Damage = 10;
                        piercing = true;
                        isHitting = true;
                        break;
                    case WeaponType.Club:
                        Damage = 7;
                        piercing = true;
                        isHitting = true;
                        StartCoroutine(damaging());
                        break;
                }


                // Apply damage
                float damageReductionPercentage = enemy.EnemyDefence * 0.02f;
                float damageMultiplier = 1 - damageReductionPercentage;
                int damageDealt = Mathf.Max(0, Mathf.RoundToInt(Damage * damageMultiplier));
                if (piercing)
                {
                    wasHit = true;
                    enemy.EnemyHealth -= Damage;
                    if (enemy.gameObject.GetComponent<AudioSource>())
                        enemy.gameObject.GetComponent<AudioSource>().PlayOneShot(enemy.gameObject.GetComponent<AudioSource>().clip);
                    //Debug.Log("HIT");
                    wasHit = false;
                }
                else
                {
                    wasHit = true;
                    enemy.EnemyHealth -= damageDealt;
                    if (enemy.gameObject.GetComponent<AudioSource>())
                        enemy.gameObject.GetComponent<AudioSource>().PlayOneShot(enemy.gameObject.GetComponent<AudioSource>().clip);

                    //The funny happens here
                    //enemy.transform.localScale = Vector3.one;
                    //Vector3 direction = (gameObject.transform.position - enemy.transform.position).normalized;
                    //enemy.transform.position -= direction;
                        /*
                        if (BNMY)
                        {
                            BNMY.CanAtt = false;
                            Vector3 direction = (gameObject.transform.position - enemy.transform.position).normalized;
                            enemy.transform.position -= direction;
                            BNMY.EnemyAnimator.SetTrigger("STOOOP");
                            BNMY.CanAtt = true;
                            Debug.Log(BNMY.CanAtt);
                        }
                        */
                    wasHit = false;
                }
            
            }
        }
    }
 
 
    private void knockBack(GameObject target, Vector3 direction, float length, float overTime)
    {
        direction = direction.normalized;
        StartCoroutine(knockBackCoroutine(target, direction, length, overTime));
    }

    IEnumerator damaging()
    {
        yield return new WaitForSeconds(.0025f);
        isHitting = false;
    }

    IEnumerator knockBackCoroutine(GameObject target, Vector3 direction, float length, float overTime)
    {
        float timeleft = overTime;
        while (timeleft > 0)
        {

            if (timeleft > Time.deltaTime)
                target.transform.Translate(direction * Time.deltaTime / overTime * length);
            else
                target.transform.Translate(direction * timeleft / overTime * length);
            timeleft -= Time.deltaTime;

            yield return null;
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
