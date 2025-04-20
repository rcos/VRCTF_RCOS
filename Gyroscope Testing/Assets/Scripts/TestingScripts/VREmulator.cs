
using UnityEngine;
using UnityEngine.InputSystem;

public class VREmulator : MonoBehaviour
{
    public float sensitivity = 4.0f;
    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;
    private float rotX;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        float z = 0;
        float y = Mouse.current.delta.ReadValue().x * sensitivity;
        rotX += Mouse.current.delta.ReadValue().y * sensitivity;
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            z += 10.0f;
            if (z > 360f)
            {
                z = 0f;
            }
        }
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            z -= 10.0f;
            if (z > 360f)
            {
                z = 0f;
            }
        }
        
       
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, transform.eulerAngles.z+z);
#endif        
    }
}
