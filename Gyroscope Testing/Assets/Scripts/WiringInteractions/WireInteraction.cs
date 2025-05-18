using System;
using UnityEngine;
using UnityEngine.Assertions;

public class WireInteraction : MonoBehaviour
{
    private bool CurrentlyHeld = false;
    private GameObject nearestWireObject = null; // Needed to get fixed joints tension with closest wire object to this object
    private Transform oppositeEnd = null; // Needed to have it bounce back in the right direction

    private GameObject socket_ConnectedTo = null;
    private GameObject socket_LookingAt = null;
    private GameObject previousSocket = null; // Needed to check if the socket is the same as the previous one

    public float TensionTillDropped = 0f; // 0 means it wont drop
    public GameObject socketToConnectToAtStart = null; // The socket to connect to at the start of the wire (if any. null means no socket)
    private float MaxDistanceFromOppositeEnd = 0f; // max distance from both ends of the wire before it drops (0 means it wont drop)

    public void setNearestWireAndOppositeEnd(GameObject wireObject, Transform oppositeEnd)
    {
        nearestWireObject = wireObject;
        this.oppositeEnd = oppositeEnd;
    }
    public void setMaxDistanceFromOppositeEnd(float maxDistance)
    {
        MaxDistanceFromOppositeEnd = maxDistance;
    }
    public bool isConnectedToSomething() {
        return socket_ConnectedTo != null;
    }
    public bool setAttachedToSocket(GameObject socket) {
        if (socket == null) return false;
        if (socket.GetComponent<Socket>() == null) {
            Debug.LogError("It seems a 'Socket' script is not attached to the socket object you are trying to connect to (" + socket.name +
                            "). Please attach a 'Socket' script to this object.", socket);
            return false;
        }
        if (socket.GetComponent<Socket>().getWireConnectedTo() != null) {
            Debug.LogError("It seems you are trying to attach a wire to a socket already in use (" + socket.name + "). This should not ever be happening", socket);
            return false;
        }
        if (socket.GetComponent<Socket>().InvertAboveArray == Array.Exists(socket.GetComponent<Socket>().CanConnectTo, element => element == this.gameObject)) {
            Debug.LogError("It seems you are trying to attach a wire to a socket that is not compatible with the wire end you are trying to connect to it (" + socket.name + "). This should not ever be happening as it should be checked earlier.", socket);
            return false;
        }
        if (socket.layer != LayerMask.NameToLayer("WireSocket")) {
            Debug.LogError("It seems you are trying to attach a wire to a socket that is not on the WireSocket layer (" + socket.name + "). Make sure the socket object's layer is set to \"WireSocket\" in the Unity Editor.", socket);
            return false;
        }
        socket_ConnectedTo = socket;
        previousSocket = socket;
        socket.GetComponent<Socket>().setWireConnectedTo(this.gameObject);
        transform.position = socket.transform.position + socket.GetComponent<Socket>().Offset; // move wire to socket position + offset
        return true;
    }
    public bool detachFromSocket(bool applyBounceBack = true) {
        if (socket_ConnectedTo == null) return false;
        if (socket_ConnectedTo.GetComponent<Socket>() == null) {
            Debug.LogError("It seems a 'Socket' script is not attached to the socket object you are trying to disconnect from (" + socket_ConnectedTo.name +
                            "). Please attach a 'Socket' script to this object. This error message shouldn't even be seen. How did you do this?????", socket_ConnectedTo);
            return false;
        }
        if (socket_ConnectedTo.GetComponent<Socket>().getWireConnectedTo() != this.gameObject) {
            Debug.LogError("The wire end you are trying to disconnect is not connected to this socket. Some variables that should be the same aren't. A logic error of some kind has occurred.", socket_ConnectedTo);
            return false;
        }
        socket_LookingAt = null;
        socket_ConnectedTo.GetComponent<Socket>().setWireConnectedTo(null);
        socket_ConnectedTo = null;
        if (applyBounceBack) { bounceBack(); }
        return true;
    }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GetComponent<Rigidbody>() == null) {
            Debug.LogError("A rigidbody isn't attached to a moveable part of a wire. Please attach a rigidbody to the object called: " + gameObject.name + " (recommended to turn off gravity, make it a trigger, and enable all constraints).");
        }
        if (gameObject.layer != LayerMask.NameToLayer("Interactive")) {
            Debug.LogError("A moveable part of the wire isn't on the Interactive layer. Please set the layer of the object called: " + gameObject.name + " to Interactive.");
        }

        // Check if collider exists (aka can it be interacted with)
        Component[] components = GetComponents<Component>();
        bool colliderExist = false;
        foreach (Component component in components) {
            if (component is Collider) {
                colliderExist = true;
                break;
            }
        }
        if (!colliderExist) {
            Debug.LogError("A moveable part of the wire doesn't have a collider. Please attach a collider to the object called: " + gameObject.name + ".\n"+
                "If this message came up despite having a collider (aka the player can interact with the object), then ignore this and report it as a github issue.");
        }

        // connect to socket if one is specified
        setAttachedToSocket(socketToConnectToAtStart);
    }

    // Update is called once per frame
    void Update()
    {
        if (nearestWireObject == null || oppositeEnd == null) {
            Debug.LogError("It seems a 'WireSpawner' script is not attached to either end other of the wire. Please attach a 'WireSpawner' script to this object or the other end and make sure " +
                        "this object appears as either the start variable or the end variable.", transform);
            return;
        }

        if (!CurrentlyHeld) { socket_LookingAt = null; return; }

        if (CheckTension()) return;

        // Find closest colliders a raycast hits. See if sockets were hit as well
        float shortestDistance, closestSocketDistance;
        GameObject socketObject = null;
        (shortestDistance, closestSocketDistance, socketObject) = getClosestColliders();

        // Move wire to where you are looking (or nearest wall you're looking at)
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * shortestDistance;

        // Check if the wire is too far from the opposite end
        float distanceFromOppositeEnd = Vector3.Distance(transform.position, oppositeEnd.position);
        if (MaxDistanceFromOppositeEnd != 0 && distanceFromOppositeEnd > MaxDistanceFromOppositeEnd) bounceBack(distanceFromOppositeEnd - MaxDistanceFromOppositeEnd);
        
        // Check if looking at a valid socket. If so move wire to socket position + offset and call OnPointerClick()
        checkClosestSocketInfo(socketObject);
    }

    private void checkClosestSocketInfo(GameObject socketObject) {
        //check if looking at socket
        if (socketObject != null) {
            if (socketObject.GetComponent<Socket>() == null) {
                Debug.LogError("It seems a 'Socket' script is not attached to the socket object you are currently looking at (" + socketObject.name +
                                "). Please attach a 'Socket' script to this object.", socketObject);
                return;
            }
            if (previousSocket == socketObject) {
                //You were looking at the same socket as before, so don't do anything until you stop looking at it and look at something else
                return;
            }
            if (socketObject.GetComponent<Socket>().getWireConnectedTo() != null) {
                //something else is connected to it
                return;
            }
            if (socketObject.GetComponent<Socket>().InvertAboveArray == Array.Exists(socketObject.GetComponent<Socket>().CanConnectTo, element => element == this.gameObject)) {
                //this socket is not compatible with the wire end you are trying to connect to it
                return;
            }
            socket_LookingAt = socketObject;
            OnPointerClick();
        } else {
            socket_LookingAt = null;
            previousSocket = null;
        }
    }

    private (float, float, GameObject) getClosestColliders() {
        float shortestDistance = 1.2f;
        float closestSocketDistance = 1.6f;
        GameObject socketObject = null;
        RaycastHit[] hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, 1.2f, ~0);
        foreach (RaycastHit hit in hits) {
            if (hit.collider.gameObject == gameObject) continue; // ignore self
            if (hit.collider.gameObject.name.Contains("WireSegment_") && hit.collider.gameObject.name.Contains("_Clone")) continue; // ignore wire segments

            shortestDistance = Math.Min(shortestDistance, hit.distance);
            if (closestSocketDistance >= hit.distance && hit.collider.gameObject.layer == LayerMask.NameToLayer("WireSocket")) {
                closestSocketDistance = hit.distance;
                socketObject = hit.collider.gameObject;
            }
        }
        return (shortestDistance, closestSocketDistance, socketObject);
    }

    private void bounceBack(float additionalDistance = 0f) {
        // bounce back to other end of object
        Vector3 direction = (oppositeEnd.position - transform.position).normalized;
        if (additionalDistance != 0f) transform.position += direction*(0.07f+additionalDistance);
        else transform.position += direction*0.3f;

        if (CurrentlyHeld) OnPointerClick();
    }

    private bool CheckTension() {
        // get tension of fixed joint and bounce it back if it is too high
        FixedJoint joint = nearestWireObject.GetComponent<FixedJoint>();
        float tension = joint.currentForce.magnitude;
        if (TensionTillDropped != 0 && tension > TensionTillDropped) {
            bounceBack();
            return true;
        }
        return false;
    }




    // Runs when the camera is first looking at an object
    public void OnPointerEnter()
    {
        
    }

    // Runs when the camera is no longer looking at an object
    public void OnPointerExit()
    {
        
    }

    // Runs when the phone is tapped on while looking at an object
    public void OnPointerClick()
    {
        if (CurrentlyHeld == true && socket_LookingAt != null) setAttachedToSocket(socket_LookingAt);
        if (CurrentlyHeld == false && socket_ConnectedTo != null) detachFromSocket(false);
        CurrentlyHeld = !CurrentlyHeld;
    }
}
