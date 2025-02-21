
using UnityEngine;

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
        float z = 0;
        float y = Input.GetAxis("Mouse X") * sensitivity;
        rotX += Input.GetAxis("Mouse Y") * sensitivity;
        if (Input.GetKey(KeyCode.Q))
        {
            z += 10.0f;
            if (z > 360f)
            {
                z = 0f;
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            z -= 10.0f;
            if (z > 360f)
            {
                z = 0f;
            }
        }
        
       
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, transform.eulerAngles.z+z);
        
    }
}
