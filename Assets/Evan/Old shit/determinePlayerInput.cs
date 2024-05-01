using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.InputSystem;

public class determinePlayerInput : MonoBehaviour
{
    public gameHandler gameScript;
    public string playerOneControls = "None";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnControllerInput(InputValue value)
    {
        if (playerOneControls == "None")
        {
            playerOneControls = "Controller";
            //gameScript.setPlayerOneAsController();
        }
    }

    private void OnKeyboardInput(InputValue value)
    {
        if (playerOneControls == "None")
        {
            playerOneControls = "Keyboard";
            //gameScript.setPlayerOneAsKeyboard();
        }
    }


}
