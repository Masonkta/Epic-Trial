using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameHandler : MonoBehaviour
{
    public string playerOneControls = "---";
    public string playerTwoControls = "---";

    public bool mouseLocked = false;

    

    ////////////////////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
   
    public void setPlayerOneAsKeyboard()
    {
        playerOneControls = "Keyboard";
        playerTwoControls = "Controller";

        print("Player One will now use keyboard.");
    }

    public void setPlayerOneAsController()
    {
        playerOneControls = "Controller";
        playerTwoControls = "Keyboard";

        print("Player One will now use controller.");
    }
    void OnQuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }
}
