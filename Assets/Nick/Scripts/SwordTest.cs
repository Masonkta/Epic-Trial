using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class SwordTest : MonoBehaviour
{
    public GameObject Sword;
    public float attackCoolDown = 1.0f;
    public bool canAtt = true;
    public Weapon Wep;
    public Transform rightHandTransform;

    public Animator anim;

    [Header("Audio")]
    public AudioSource swingSounds;
    public AudioClip swing1;
    public AudioClip swing2;


    // Start is called before the first frame update
    void Start()
    {
        Wep.enabled = false;
    }

    void Update()
    {
        //Debug.DrawRay(rightHandTransform.position, rightHandTransform.forward * 2f, Color.blue);
        //placeSwordInPlayersHand();    
    }

    void OnAttack()
    {
        if (canAtt)
            Attacking();
    }

    public void Attacking()
    {
        canAtt = false;
        Wep.enabled = true;
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAtt());

        //playSwordSwingSound();
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
        Wep.enabled = false;
        canAtt = true;
    }



    void placeSwordInPlayersHand()
    {
        Sword.transform.localPosition = rightHandTransform.localPosition;
    }
}
