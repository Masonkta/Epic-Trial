using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using System.Collections; 
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;
using TMPro;

public class RumbleHaptics : MonoBehaviour
{
    private Gamepad gamepad;
    private bool isRumbling = false;
    private float previousHealth;
    bool toggle;
    public Toggle uiToggle;

    private gameHandler GameHandler;

    void Start()
    {
        // Find the Gamepad object in the scene
        gamepad = Gamepad.current;

        if (gamepad == null)
        {
            Debug.LogWarning("No Xbox controller connected!");
            return;
        }

        // Find the GameHandler object in the scene
        GameHandler = FindObjectOfType<gameHandler>();

        if (GameHandler == null)
        {
            Debug.LogWarning("GameHandler script not found in the scene!");
            return;
        }

        // Store initial player health
        previousHealth = GameHandler.GetControllerHealth();

        // Subscribe to Input System events
        InputSystem.onDeviceChange += OnDeviceChange;
        
        // Subscribe to scene change events
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        // Check if the added device is a Gamepad
        if (device is Gamepad && change == InputDeviceChange.Added && gamepad == null)
        {
            gamepad = (Gamepad)device;
            Debug.Log("Xbox controller connected!");
        }
    }

    public void OnToggleChange(bool tickOn)
    {
        if(tickOn)
        {
        }
        else
        {
        }
        toggle = tickOn;
    }

    void Update()
    {
        float currentHealth = GameHandler.GetControllerHealth();
        uiToggle = GetComponent<Toggle>();
        if (toggle == true)
        {
            // Check for changes in player health
            if (currentHealth < previousHealth)
            {
                // Player health has dropped
                StartRumble(0.4f, 0.2f);
            }
        }

        else if (currentHealth > previousHealth)
        {
            // Player health has increased (regained health)
            // You can add additional logic here if needed
        }

        // Update previousHealth for the next frame
        previousHealth = currentHealth;
    }

    public void StartRumble(float intensity, float duration)
    {
        if (gamepad != null && !isRumbling)
        {
            // Make the controller rumble
            gamepad.SetMotorSpeeds(intensity, intensity);
            isRumbling = true;
            print(isRumbling);
            StartCoroutine(StopRumble(duration));
        }
        else
        {
            Debug.LogWarning("No Xbox controller connected or already rumbling!");
        }
    }


    System.Collections.IEnumerator StopRumble(float duration)
    {
        yield return new WaitForSeconds(duration);
        // Stop the rumble after the specified duration
        gamepad.SetMotorSpeeds(0, 0);
        isRumbling = false;
        print(isRumbling);
    }

    public void StopRumbleFunc()
    {
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(0, 0);
            isRumbling = false;
            print(isRumbling);
        }
    }

    public void toggleOn()
    {
        toggle = true;
    }

    public void toggleOff()
    {
        toggle = false;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Stop the rumble when the scene changes
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(0, 0);
            isRumbling = false;
            print(isRumbling);
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from Input System events
        InputSystem.onDeviceChange -= OnDeviceChange;

        // Unsubscribe from scene change events
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}