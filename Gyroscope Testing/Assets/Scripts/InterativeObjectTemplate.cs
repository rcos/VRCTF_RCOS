using UnityEngine;

public class InterativeObjectTemplate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Runs when the camera is first looking at an object
    public void OnPointerEnter()
    {
        Debug.Log("Pointer Enter");
    }

    // Runs when the camera is no longer looking at an object
    public void OnPointerExit()
    {
        Debug.Log("Pointer Exit");
    }

    // Runs when the phone is tapped on while looking at an object
    public void OnPointerClick()
    {
        Debug.Log("Pointer Click");
    }
}
