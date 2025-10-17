using UnityEngine;

public class PauseMenuButtonScript : MonoBehaviour
{
    public Material notLookingAtColor = null;
    public Material lookingAtColor = null;
    public PauseMenu_ButtonParent scriptToCall = null;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<MeshRenderer>().material = notLookingAtColor;
    }

    // Update is called once per frame
    void Update()
    {
        // if (scriptToCall == null) Destroy(gameObject);
    }

    // Runs when the camera is first looking at an object
    public void OnPointerEnter()
    {
        GetComponent<MeshRenderer>().material = lookingAtColor;
    }

    // Runs when the camera is no longer looking at an object
    public void OnPointerExit()
    {
        GetComponent<MeshRenderer>().material = notLookingAtColor;
    }

    // Runs when the phone is tapped on while looking at an object
    public void OnPointerClick()
    {
        scriptToCall.Pressed();
    }
}
