using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class SwordTest : MonoBehaviour
{
    gameHandler gameScript;
    public GameObject Sword;
    public float timer = 1.0f;
    public bool canAtt = true;
    public Weapon Wep;

    [Header("Audio")]
    public AudioSource swingSounds;
    public AudioClip swing1;
    public AudioClip swing2;


    // Start is called before the first frame update
    void Start()
    {
        Wep.enabled = false;
    }

    void OnKeyboardAttack()
    {
        if (canAtt)
        {
            Attacking();
        }
    }
    
    void OnControllerAttack()
    {
        if (canAtt)
        {
            Attacking();
        }
    }

    public void Attacking()
    {
        canAtt = false;
        Wep.enabled = true;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAtt());
    }

    IEnumerator ResetAtt()
    {
        yield return new WaitForSeconds(timer);
        Wep.enabled = false;
        canAtt = true;
    }
}
