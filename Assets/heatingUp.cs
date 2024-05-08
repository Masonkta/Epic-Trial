using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class heatingUp : MonoBehaviour
{
    public int kills;

    public List<float> timesOfKills;

    public int scoreMultiplier;

    public TextMeshProUGUI smk;
    public TextMeshProUGUI smc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timesOfKills.Count > 0)
        {
            for (int i = 0; i < timesOfKills.Count; i++)
            {
                if (Time.time - 5f > timesOfKills[i])
                {
                    timesOfKills.Remove(timesOfKills[i]);
                }
            }
        }

        scoreMultiplier = Mathf.Max(1, timesOfKills.Count);

        smk.enabled = scoreMultiplier > 1;
        smk.text = "x" + scoreMultiplier.ToString();

        smc.enabled = scoreMultiplier > 1;
        smc.text = "x" + scoreMultiplier.ToString();
    }

    public void AddKill()
    {
        kills++;

        timesOfKills.Add(Time.time);
    }
}
