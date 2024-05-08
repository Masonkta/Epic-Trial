using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;
    public GameObject Boss;
    private Enemy _enemy; // Store reference to the Enemy component
    private int _maxHealth; // Store maximum health

    void Start()
    {
        _enemy = Boss.GetComponent<Enemy>(); // Get reference to the Enemy component
        _maxHealth = _enemy.EnemyHealth; // Get maximum health from the Enemy component
    }

    void Update()
    {
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        // Calculate fill amount based on current health and maximum health
        float fillAmount = (float)_enemy.EnemyHealth / _maxHealth;


        // Update the fill amount of the health bar sprite
        _healthbarSprite.fillAmount = fillAmount;
    }
}
