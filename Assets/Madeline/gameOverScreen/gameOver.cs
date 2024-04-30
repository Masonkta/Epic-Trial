using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Button quitButton;
    public Button restartButton;

    private void Start()
    {
        quitButton = GameObject.Find("#quitButton").GetComponent<Button>();
        restartButton = GameObject.Find("#restartButton").GetComponent<Button>();

        quitButton.onClick.AddListener(QuitGame);
        restartButton.onClick.AddListener(RestartGame);
    }
    private void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("startScene");
    }
}
