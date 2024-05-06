using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MiniWorldUi : MonoBehaviour
{
    VisualElement root;

    public Transform Enemy1;
    public Transform Enemy2;
    public Transform Enemy3;

    VisualElement healthBar1;
    VisualElement healthBar2;
    VisualElement healthBar3;

    public VisualTreeAsset healthBarAsset; // Reference to the HealthBar UI prefab

    public Camera player1Camera; // Reference to the camera for Player 1
    public Camera player2Camera; // Reference to the camera for Player 2

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        // Instantiate health bars from the UI prefab
        healthBar1 = healthBarAsset.Instantiate();
        healthBar2 = healthBarAsset.Instantiate();
        healthBar3 = healthBarAsset.Instantiate();

        // Add health bars to the root visual element
        root.Add(healthBar1);
        root.Add(healthBar2);
        root.Add(healthBar3);
    }

    private void Update()
    {
        UpdateHealthBarPosition(Enemy1, healthBar1, player1Camera);
        UpdateHealthBarPosition(Enemy2, healthBar2, player1Camera);
        UpdateHealthBarPosition(Enemy3, healthBar3, player2Camera);
    }

    private void UpdateHealthBarPosition(Transform enemyTransform, VisualElement healthBar, Camera playerCamera)
    {
        if (enemyTransform == null || healthBar == null || playerCamera == null)
            return;

        // Convert world position of enemy to screen position relative to the player's camera
        Vector3 screenPos = playerCamera.WorldToScreenPoint(enemyTransform.position);

        // Set the position of the health bar relative to the screen
        healthBar.style.left = screenPos.x - (healthBar.layout.width / 2);
        healthBar.style.top = (Screen.height - screenPos.y) - 100; // Adjust as needed for vertical positioning
    }
}
