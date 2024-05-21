using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealthBars : MonoBehaviour
{
    gameHandler gameScript;
    public Image _healthbarforK;
    public Image _healthbarforC;

    // Start is called before the first frame update
    void Start()
    {
        gameScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<gameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        _healthbarforK.fillAmount = gameScript.keyboardPlayerHealth / 100f;
        _healthbarforC.fillAmount = gameScript.controllerPlayerHealth / 100f;
    }
}
