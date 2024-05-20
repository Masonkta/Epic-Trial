using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class SwordTest : MonoBehaviour
{
    [Header("Settings")]
    public float attackCoolDown = 1.0f;
    public bool canAtt = true;
    public bool ImStuck = false;
    public Transform rightHandTransform;

    [Header("Scripts")]
    public GameObject Sword;
    public Weapon Wep;

    [Header("Animators")]
    public Animator anim;
    public Animator Armor;

    [Header("Audio")]
    public AudioSource swingSounds;
    public AudioClip swing1;
    public AudioClip swing2;

    public playerAccessWeapons wpnScrpt;

    // Start is called before the first frame update

    void Start()
    {
        Wep.enabled = false;
    }

    void OnAttack()
    {
        if (canAtt && !anim.IsInTransition(0))
            Attacking();
    }

    public void Attacking()
    {
        wpnScrpt.swinging = true;
        canAtt = false;
        Wep.enabled = true;

        anim.SetTrigger("Attack");
        if (Armor)  
            Armor.SetTrigger("Attack");

        StartCoroutine(ResetAtt());
        playSwordSwingSound();
    }

    void playSwordSwingSound()
    {
        swingSounds.clip = Random.value > 0.5f ? swing1 : swing2;
        swingSounds.pitch = Random.Range(0.9f, 1.11f);

        swingSounds.PlayOneShot(swingSounds.clip);
    }

    IEnumerator ResetAtt()
    {
        yield return new WaitForSeconds(attackCoolDown);

        anim.SetTrigger("StopAttack");
        if (Armor)
            Armor.SetTrigger("StopAttack");

        Wep.enabled = false;
        canAtt = true;
        wpnScrpt.swinging = false;
    }
}
