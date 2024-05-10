using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchScript : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public enum Player { controllerSbeve, keyboardSbeve }
    public Player player = Player.keyboardSbeve;
    private gameHandler GameManager;
    public float healthThreshold = 50f;
    private bool particleOn = true;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("Game Handler").GetComponent<gameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        float playerHealth = (player == Player.controllerSbeve) ? GameManager.controllerPlayerHealth : GameManager.keyboardPlayerHealth;

        if (playerHealth < healthThreshold && particleOn)
        {
            stopParticles();
        }

        else if (playerHealth >= healthThreshold && !particleOn)
        {
            startParticles();
        }
    }

    private void startParticles()
    {
        particleSystem.Play();
        particleOn = true;
    }

    private void stopParticles()
    {
        particleSystem.Stop();
        particleOn = false;
    }
}
