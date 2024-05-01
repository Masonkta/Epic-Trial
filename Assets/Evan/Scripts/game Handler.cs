using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameHandler : MonoBehaviour
{
    public GameObject playerOne;
    public GameObject playerTwo;
    public string playerOneControls = "---";
    public string playerTwoControls = "---";

    [Header("Resources")]
    public int clothPieces;



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

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void setPlayerOneAsController()
    {
        playerOneControls = "Controller";
        playerTwoControls = "Keyboard";

        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnToggleCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void OnQuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }

    public void getCloth()
    {
        clothPieces++;
    }
}
