using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int EnemyHealth;
    public int EnemyDefence;
    public HighScoreTest hs;
    // Start is called before the first frame update
    void Start()
    {
        if (tag == ("Enemy"))
        {
            EnemyHealth = 10;
            EnemyDefence = 0;
        }
        if (tag == ("MediumEnemy"))
        {
            EnemyHealth = 20;
            EnemyDefence = 5;
        }
        if (tag == ("HeavyEnemy"))
        {
            EnemyHealth = 30;
            EnemyDefence = 10;
        }
        if (tag == ("Boss"))
        {
            EnemyHealth = 100;
            EnemyDefence = 20;
        }
    }



    // Update is called once per frame
    void Update()
    {
        string tag = gameObject.tag;
        if (EnemyHealth <= 0) {
            if (tag ==("Enemy"))
            {
                hs.score += 10;
            }
            if (tag == ("MediumEnemy"))
            {
                hs.score += 100;
            }
            if (tag == ("HeavyEnemy"))
            {
                hs.score += 1000;
            }
            if (tag == ("Boss"))
            {
                hs.score += 10000;
            }
            Debug.Log("Current Score is " + hs.score);
            Destroy(gameObject);
        }
    }
}
