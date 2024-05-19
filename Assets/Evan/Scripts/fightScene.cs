using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fightScene : MonoBehaviour
{
    gameHandler gameScript;
    GameObject playerKeyboard;
    public Transform swordTipK;
    GameObject playerController;
    public Transform swordTipC;


    public float kSwordToC;
    public float cSwordToK;

    // Start is called before the first frame update
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
        playerKeyboard = gameScript.keyboardPlayer;
        playerController = gameScript.controllerPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        kSwordToC = Vector3.Distance(swordTipK.position, playerController.transform.position);
        cSwordToK = Vector3.Distance(swordTipC.position, playerKeyboard.transform.position);

        if (kSwordToC < 2f) gameScript.controllerPlayerHealth -= Time.deltaTime * 20f;
        
        if (cSwordToK < 2f) gameScript.keyboardPlayerHealth -= Time.deltaTime * 20f;
    }
}
