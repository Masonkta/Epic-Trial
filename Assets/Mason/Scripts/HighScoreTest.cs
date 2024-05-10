using HighScore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreTest : MonoBehaviour
{
    public int score;
    private float delay;
    // Start is called before the first frame update
    void Start()
    {
        HS.Init(this, "Ring of Hell");
        delay = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
            if (delay < 0)
            {
                HS.SubmitHighScore(this, "Mason", score);
            }
        }
    }
}
