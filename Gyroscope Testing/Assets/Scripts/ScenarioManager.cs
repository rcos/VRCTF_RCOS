using UnityEngine;
using UnityEngine.Events;

/*
 Very very simple, easy for demonstration purpose implementation. I'm just guessing at what methods will be useful for future
 scenarios. There's a lot of potential ways to do this (Actions/delegations/events/unityevents/eventsystem/eventmanager
 and a ton of other keywords I haven't looked into)
 Also might need to figure out a way to easily configure custom flags (ex. set beginning description/tip, set finish flag)
 Currently hardcoded for the first scenario - David Li ello
*/
public class ScenarioManager : MonoBehaviour
{
    [SerializeField] private GameObject endingWindow;
    private bool flagSet; // whether the user has finished the scenario or not might not be needed
    private GameObject currentInspect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flagSet = false;
        currentInspect = null;
        endingWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp(GameObject inspect)
    {
        if (currentInspect != null)
        {
            currentInspect.GetComponent<InspectController>().ForceStop();
        }
        currentInspect = inspect;
    }

    public void PutDown()
    {
        currentInspect = null;
    }

    public void FlagTriggered()
    {
        flagSet = true;
        endingWindow.SetActive(true);
    }
}
