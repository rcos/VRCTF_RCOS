using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrl : MonoBehaviour
{
    public float speed = 3.5f;
    public float mouseSensitivity = 2f;
    
    private Vector2 moveInput;
    private Vector2 lookInput;

    void Update()
    {
        if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            HandleKeyboardMovement();
            HandleMouseLook();
        }
        else
        {
            HandleVRMovement();
        }
    }

    void HandleKeyboardMovement()
    {
        moveInput = Keyboard.current != null ? new Vector2(
            (Keyboard.current.aKey.isPressed ? -1 : 0) + (Keyboard.current.dKey.isPressed ? 1 : 0),
            (Keyboard.current.wKey.isPressed ? 1 : 0) + (Keyboard.current.sKey.isPressed ? -1 : 0)
        ) : Vector2.zero;

        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        if (Mouse.current == null) return;

        lookInput = Mouse.current.delta.ReadValue() * mouseSensitivity * Time.deltaTime;

        transform.Rotate(0, lookInput.x, 0);
        Camera.main.transform.localEulerAngles += new Vector3(-lookInput.y, 0, 0);
    }

    void HandleVRMovement()
    {
        Vector3 forwardDirection = Camera.main.transform.forward;
        forwardDirection.y = 0;
        forwardDirection.Normalize();

        if (Touchscreen.current != null && Touchscreen.current.press.isPressed)
        {
            Vector3 velocity = forwardDirection * speed;
            transform.Translate(velocity * Time.deltaTime, Space.World);
        }
    }
}