using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour {

    private void Start()
    {
        if (!Application.isEditor)
            Display.displays[1].Activate();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Main"); //idk what the main game scene is called, need to replace with that
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame()
    {
        Debug.Log("quit button pushed");

        Application.Quit();

        // uncomment this to make it quit in editor - testing
        // UnityEditor.EditorApplication.isPlaying = false;
    }
}