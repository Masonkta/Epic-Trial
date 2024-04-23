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
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
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
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Atack");
        StartCoroutine(ResetAtt());
    }

    IEnumerator ResetAtt()
    {
        yield return new WaitForSeconds(timer);
        canAtt = true;
    }
}
