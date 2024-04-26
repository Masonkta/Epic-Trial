using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyType
{
    Weak,
    Medium,
    Heavy,
    Boss,
}

public class Enemy : MonoBehaviour
{
    public int EnemyHealth;
    public int EnemyDefence;
    public HighScoreTest hs;
    public EnemyType Etype;
 

    // Start is called before the first frame update
    void Start()
    {
        if (EnemyType.Weak == Etype)
        {
            EnemyHealth = 10;
            EnemyDefence = 0;
        }
        if (EnemyType.Medium == Etype)
        {
            EnemyHealth = 20;
            EnemyDefence = 5;
        }
        if (EnemyType.Heavy == Etype)
        {
            EnemyHealth = 30;
            EnemyDefence = 10;
        }
        if (EnemyType.Boss == Etype)
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
            if (EnemyType.Weak == Etype)
            {
                hs.score += 10;
            }
            if (EnemyType.Medium == Etype)
            {
                hs.score += 100;
            }
            if (EnemyType.Heavy == Etype)
            {
                hs.score += 1000;
            }
            if (EnemyType.Boss == Etype)
            {
                hs.score += 10000;
            }
            Debug.Log("Current Score is " + hs.score);
            Destroy(gameObject);
        }
    }
}
