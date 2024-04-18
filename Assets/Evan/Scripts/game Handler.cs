using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameHandler : MonoBehaviour
{
    public string playerOneControls = "---";
    public string playerTwoControls = "---";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPlayerOneAsController()
    {
        playerOneControls = "Controller";
        playerTwoControls = "Keyboard";

        print("Player One will now use controller.");
    }
    
    public void setPlayerOneAsKeyboard()
    {
        playerOneControls = "Keyboard";
        playerTwoControls = "Controller";

        print("Player One will now use keyboard.");
    }
}
