using HighScore;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    HighScoreTest hs;
    public TextMeshProUGUI highScoreText;

    /*public Button quitButton;
    public Button restartButton;*/

    private void Start()
    {
        /*quitButton = GameObject.Find("#quitButton").GetComponent<Button>();
        restartButton = GameObject.Find("#restartButton").GetComponent<Button>();

        quitButton.onClick.AddListener(QuitGame);
        restartButton.onClick.AddListener(RestartGame);*/

        hs = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<HighScoreTest>();

    }

    private void Update()
    {
        highScoreText.text = "High Score: " + hs.score;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("startScreen");
    }
}
