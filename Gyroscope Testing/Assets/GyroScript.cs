// Template from Unity Gyroscope Docs

using UnityEngine;

public class GyroScript : MonoBehaviour
{
    private Quaternion center;
    private GameObject holdingObject;
    private float radius = 0;
    void Start()
    {
        Input.gyro.enabled = true;
        Input.gyro.updateInterval = 1.0f / 120.0f;
        holdingObject = null;
        center = new Quaternion();
    }
    
    protected void Update()
    {
        GyroModifyCamera();
        if (Input.touchCount > 0)
        {
            center = GyroToUnity();
        }
        
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Interact();
        }

        if (holdingObject is not null)
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
            holdingObject.transform.position = ray.GetPoint(radius);
        }
    }

    
    protected void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;

        GUILayout.Label("Orientation: " + Screen.orientation);
        GUILayout.Label("input.gyro.attitude: " + Input.gyro.attitude);
        GUILayout.Label("iphone width/font: " + Screen.width + " : " + GUI.skin.label.fontSize);
    }
    
    void GyroModifyCamera()
    {
        transform.rotation = Quaternion.Inverse(center) * GyroToUnity();
    }

    private static Quaternion GyroToUnity()
    {
        Quaternion gyro = Input.gyro.attitude;
        Vector3 gravity = Input.acceleration.normalized;

        Quaternion accel = Quaternion.LookRotation(Vector3.Cross(gravity, Vector3.right),gravity);
        
        Quaternion q = Quaternion.Slerp(accel, gyro,0.98f);
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    private void Interact()
    {
        if (holdingObject is not null)
        {
            holdingObject = null;
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position ,transform.forward, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                Debug.Log(hit.distance);
                holdingObject = hit.collider.gameObject;
                radius = hit.distance;
            }
        }
        
    }
}