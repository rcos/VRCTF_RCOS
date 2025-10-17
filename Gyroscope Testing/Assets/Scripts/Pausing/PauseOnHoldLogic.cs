using UnityEngine;
using UnityEngine.InputSystem;

public class PauseOnHoldLogic : MonoBehaviour
{
    private GameObject wayToOpenSettings = null;
    private bool isSettingsOpen = false;

    public float AllowedTimeForDroppedHoldInput = 0.2f; // in seconds
    public float totalTimeHold = 1.5f; // in seconds
    private float currentTimeHeld = 0.0f;
    private float currentTimeDropped = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void checkHold() {
        bool isButtonPressed = Google.XR.Cardboard.Api.IsTriggerPressed || Mouse.current.leftButton.isPressed;
        
        currentTimeHeld = (currentTimeHeld > 0.01f || isButtonPressed) ? (currentTimeHeld + Time.deltaTime) : 0;
        currentTimeDropped = (currentTimeHeld > 0.01f && !isButtonPressed) ? (currentTimeDropped + Time.deltaTime) : 0;
        if (currentTimeDropped >= AllowedTimeForDroppedHoldInput) {
            currentTimeHeld = 0.0f;
            currentTimeDropped = 0.0f;
        }

        if (currentTimeHeld > 1f) {
            StaticPausingFunctions.PauseGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkHold();
    }
}
