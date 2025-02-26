using UnityEngine;

public class InventoryManagement: MonoBehaviour
{
    public GameObject storedItem; // Currently holds one, will figure out multiple items.
    private int timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0;
        storedItem = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.eulerAngles.x > 65f && transform.rotation.eulerAngles.x < 270f && storedItem?.activeSelf == false && timer > 100)
        {
            PresentInventory();
            timer = 0;
        }
        else if (transform.rotation.eulerAngles.x > 65f && transform.rotation.eulerAngles.x < 270f && storedItem?.activeSelf == false)
        {
            timer++;
        }
        else if (transform.rotation.eulerAngles.x < 65f)
        {
            storedItem?.SetActive(false);
            timer = 0;
        }
    }

    void PresentInventory()
    {
        // Ideally it could fade in the closer you look to ground maybe
        storedItem.transform.position = Camera.main.transform.position + Camera.main.transform.forward * (1f + Camera.main.nearClipPlane);
        storedItem.transform.position = new Vector3(storedItem.transform.position.x, 1f, storedItem.transform.position.z); // Eventually set it to spawn the same distance away
        storedItem.transform.rotation = Camera.main.transform.rotation;
        storedItem.transform.Rotate(90f, 0f, 0f);
        storedItem.SetActive(true);
    }

    public void EnterIntoInventory(GameObject item)
    {
        storedItem = item;
    }

    
}