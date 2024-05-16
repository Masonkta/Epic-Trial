using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using HighScore;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    GameObject hs_trigger;
    HighScoreTest hs;
    public TextMeshProUGUI points;
    public TextMeshProUGUI name1;
    public TextMeshProUGUI name2;
    // Start is called before the first frame update
    void Start()
    {
        hs_trigger = GameObject.FindGameObjectWithTag("highscore");
        if (hs_trigger.GetComponent<HighScoreTest>())
            hs = hs_trigger.GetComponent<HighScoreTest>();
        points.text = "Score: " + hs.score.ToString();

    }

    public void submit()
    {
        HS.SubmitHighScore(this, name1.text + " and " + name2.text, hs.score);
    }

    public void restart()
    {
        SceneManager.LoadScene("startScreen");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
