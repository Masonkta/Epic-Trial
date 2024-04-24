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

    // Start is called before the first frame update
    void Start()
    {
        Sword.GetComponent<BoxCollider>().enabled = false;
    }

    void OnAttack()
    {
        if (canAtt)
        {
            Attacking();
        }
    }

    public void Attacking()
    {
        canAtt = false;
        Sword.GetComponent<BoxCollider>().enabled = true;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAtt());
    }

    IEnumerator ResetAtt()
    {
        yield return new WaitForSeconds(timer);
        Sword.GetComponent<BoxCollider>().enabled = false;
        canAtt = true;
    }
}
