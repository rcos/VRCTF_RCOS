using UnityEngine;

public class Socket : MonoBehaviour
{
    private GameObject WireEndConnectedTo = null; // The wire end that is connected to this socket
    public GameObject[] CanConnectTo = null; // Only these objects can connect to socket
    public bool InvertAboveArray = true; //Now cant connect to above array, connects to everything else
    public Vector3 Offset = Vector3.zero; //where to have wire go to when connected to this socket (offset from center of gameobject)

    public void setWireConnectedTo(GameObject wireEnd)
    {
        WireEndConnectedTo = wireEnd;
    }
    public GameObject getWireConnectedTo()
    {
        return WireEndConnectedTo;
    }
    public bool detachFromWire() {
        if (WireEndConnectedTo == null) return false;
        if (WireEndConnectedTo.GetComponent<WireInteraction>() == null) {
            Debug.LogError("The wire you are trying to disconnect doesn't have the \"WireInteraction\" script attached to it." +
                            "Check out \"" + WireEndConnectedTo.name + "\" and make sure it has the script.", WireEndConnectedTo);
            return false;
        }
        return WireEndConnectedTo.GetComponent<WireInteraction>().detachFromSocket(true);
    }
    public bool AttachToWire(GameObject wireEnd) {
        if (wireEnd.GetComponent<WireInteraction>() == null) {
            Debug.LogError("The wire you are trying to connect doesn't have the \"WireInteraction\" script attached to it." +
                            "Check out \"" + wireEnd.name + "\" and make sure it has the script.", wireEnd);
            return false;
        }
        return wireEnd.GetComponent<WireInteraction>().setAttachedToSocket(this.gameObject);
    }

    void Start() {
        // check if right layer ("WireSocket")
        if (gameObject.layer != LayerMask.NameToLayer("WireSocket")) {
            Debug.LogError("The object " + gameObject.name + " is not on the correct layer despite having the script to act as a Socket. Please set it to 'WireSocket' layer.", gameObject);
        }

        // check if a collider exists (aka can it be interacted with)
        Component[] components = GetComponents<Component>();
        bool colliderExist = false;
        foreach (Component component in components) {
            if (component is Collider) {
                colliderExist = true;
                break;
            }
        }
        if (!colliderExist) {
            Debug.LogError("A Socket doesn't have a collider. Please attach a collider to the object called: " + gameObject.name + ".\n"+
                "If this message came up despite having a collider (aka the player can interact with the object), then ignore this and report it as a github issue.");
        }
    }
}