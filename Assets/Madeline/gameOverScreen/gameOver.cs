using HighScore;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    HighScoreTest hs;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI name1;
    public TextMeshProUGUI name2;

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
        highScoreText.text = "Score: " + hs.score;
    }

    public void submit()
    {
        HS.SubmitHighScore(this, name1.text + " and " + name2.text, hs.score);
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
