using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerB : MonoBehaviour
{
    public int Player1Health;
    public int Player1Defence;
    public int Player2Health;
    public int Player2Defence;


    // Start is called before the first frame update
    void Start()
    {
        Player1Health = 100;
        Player1Defence = 0;
        Player2Health = 100;
        Player2Defence = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
