using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour {
    public void PlayGame()
    {
        SceneManager.LoadScene("Evan Scene"); //idk what the main game scene is called, need to replace with that
    }

    public void QuitGame()
    {
        Debug.Log("quit button pushed");

        Application.Quit();

        // uncomment this to make it quit in editor - testing
        // UnityEditor.EditorApplication.isPlaying = false;
    }
}