using UnityEngine;

public class InventoryManagement: MonoBehaviour
{
    public GameObject storedItem;
    private int timer;
    private Vector3 storedPosition;
    private Vector3 storedDirection;
    private Quaternion storedRotation;
    private Vector3 lastCameraPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0;
        storedItem = null;
        lastCameraPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.rotation.eulerAngles.x >= 65f && transform.rotation.eulerAngles.x < 270f && storedItem?.activeSelf == false) // If camera is looking down
        {
            // Starts counting down for 100 frames
            if (timer > 100) // Show inventory after 100 frames
            {
                PresentInventory();
            }
            else if (timer == 0) // Calculate spawn location on the first frame the camera looks down
            {
                PrepInventory();
                timer++;
            }
            else // Count up this frame
            {
                timer++;
            }
        }
        else if (transform.rotation.eulerAngles.x < 65f || lastCameraPosition != transform.position) // Looking up or post teleport reset timer.
        {
            lastCameraPosition = transform.position;
            storedItem?.SetActive(false);
            timer = 0;
        }
    }
    
    // Prepare inventory spawn location
    void PrepInventory()
    {
        storedDirection = transform.forward;
        storedPosition = transform.position + transform.forward * (1f + Camera.main.nearClipPlane);
        storedPosition = new Vector3(storedPosition.x, 1f, storedPosition.z); // Eventually set it to spawn the same distance away
        storedRotation = transform.rotation;
    }
    
    // Show prepared inventory
    void PresentInventory()
    {
        // Ideally it could fade in the closer you look to ground maybe
        storedItem.transform.position = storedPosition;
        storedItem.transform.rotation = storedRotation;
        storedItem.transform.Rotate(90f, 0f, 0f);
        storedItem.SetActive(true);
    }

    // Stores item to be shown in inventory (Add multi-item inventory later)
    public void EnterIntoInventory(GameObject item)
    {
        storedItem = item;
    }
    
}