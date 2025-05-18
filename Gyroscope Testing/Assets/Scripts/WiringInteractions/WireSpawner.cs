using UnityEngine;

public class WireSpawner : MonoBehaviour
{
    public Transform start;
    public Vector3 start_offset = Vector3.zero;
    public Transform end;
    public Vector3 end_offset = Vector3.zero;

    [Range(2, 500)]
    public int wireSegmentCount = 10; // 2 means straight line
    public float MaxWireLengthBetweenSegments = 0.1f; // length of each wire segment

    public float tensionLimitTillResetWholeWire = 0f; // 0 means it wont reset

    public Material wireColorWhenUnplugged;
    public Material wireColorWhenPlugged;
    public float wireWidth = 0.1f; // width of the wire (visual width)
    public float wireMaxLength = 1f; // max length of the wire

    private GameObject[] wires;
    private Vector3 initial_startPos;
    private Vector3 initial_endPos;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initial_startPos = start.position;
        initial_endPos = end.position;
        makeAllWires();

        // check if total wire length is enough
        if (wireSegmentCount == 2) return;

        float minWireLength = Vector3.Distance(start.position, end.position);
        if (minWireLength > wireSegmentCount * MaxWireLengthBetweenSegments) {
            Debug.LogError("The two objects that make the wire are too far apart. Bring them closer to each other, add more wire segments, or increase wire length.\n"+
                            "The error is coming from gameObject: " + gameObject.name, gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //update all points in line renderer to match spheres
        GetComponent<LineRenderer>().positionCount = wireSegmentCount+2;
        GetComponent<LineRenderer>().SetPosition(0, start.position+start_offset);
        int i;
        for (i = 0; i < wireSegmentCount; i++)
        {
            GetComponent<LineRenderer>().SetPosition(i+1, wires[i].transform.position);
        }
        GetComponent<LineRenderer>().SetPosition(i+1, end.position+end_offset);

        // Check if both ends of wire are connected to something
        bool isStartConnected = start.GetComponent<WireInteraction>() == null ? true : start.GetComponent<WireInteraction>().isConnectedToSomething();
        bool isEndConnected = end.GetComponent<WireInteraction>() == null ? true : end.GetComponent<WireInteraction>().isConnectedToSomething();
        if (isStartConnected && isEndConnected) {
            LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
            lineRenderer.material = wireColorWhenPlugged;
            lineRenderer.startColor = wireColorWhenPlugged.color;
            lineRenderer.endColor = wireColorWhenPlugged.color;
        } else {
            LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
            lineRenderer.material = wireColorWhenUnplugged;
            lineRenderer.startColor = wireColorWhenUnplugged.color;
            lineRenderer.endColor = wireColorWhenUnplugged.color;
        }

        // FAILSAFE!!! If wire tension is wayyyyyy to high we reset the wire
        float tension = wires[0].GetComponent<FixedJoint>().currentForce.magnitude;
        if (tensionLimitTillResetWholeWire != 0 && tension > tensionLimitTillResetWholeWire)
        {
            Debug.Log("Wire tension too high, resetting wire positions.");
            start.position = initial_startPos;
            end.position = initial_endPos;
            if (start.GetComponent<WireInteraction>() != null && start.GetComponent<WireInteraction>().socketToConnectToAtStart != null) {
                start.GetComponent<WireInteraction>().setAttachedToSocket(start.GetComponent<WireInteraction>().socketToConnectToAtStart);
            }
            if (end.GetComponent<WireInteraction>() != null && end.GetComponent<WireInteraction>().socketToConnectToAtStart != null) {
                end.GetComponent<WireInteraction>().setAttachedToSocket(end.GetComponent<WireInteraction>().socketToConnectToAtStart);
            }
            for (int j = 0; j < wireSegmentCount; j++)
            {
                float xPos, yPos, zPos;
                (xPos, yPos, zPos) = getWirePosition(j);
                wires[j].transform.position = new Vector3(xPos, yPos, zPos);
            }
        }
    }

    private (float, float, float) getWirePosition(int index)
    {
        float xPos = (start.position.x + start_offset.x) + ((end.position.x + end_offset.z) - (start.position.x + start_offset.x)) / (wireSegmentCount-1) * index;
        float yPos = (start.position.y + start_offset.y) + ((end.position.y + end_offset.z) - (start.position.y + start_offset.y)) / (wireSegmentCount-1) * index;
        float zPos = (start.position.z + start_offset.z) + ((end.position.z + end_offset.z) - (start.position.z + start_offset.z)) / (wireSegmentCount-1) * index;
        return (xPos, yPos, zPos);
    }

    private void makeAllWires()
    {
        GameObject wirePrefab = Resources.Load<GameObject>("WireObjectToClone/Sphere");
        if (gameObject.GetComponent<LineRenderer>() == null) gameObject.AddComponent<LineRenderer>();
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.startWidth = wireWidth;
        lineRenderer.endWidth = wireWidth;
        lineRenderer.material = wireColorWhenUnplugged;
        lineRenderer.startColor = wireColorWhenPlugged.color;
        lineRenderer.endColor = wireColorWhenPlugged.color;
        
        wires = new GameObject[wireSegmentCount];
        for (int i = 0; i < wireSegmentCount; i++)
        {
            wires[i] = Instantiate(wirePrefab, this.transform);
            wires[i].name = "WireSegment_" + i + "_Clone";
            float xPos, yPos, zPos;
            (xPos, yPos, zPos) = getWirePosition(i);
            wires[i].transform.position = new Vector3(xPos, yPos, zPos);
            wires[i].transform.localScale = new Vector3(1f, 1f, 1f);

            if (wires[i].GetComponent<Rigidbody>() == null) wires[i].AddComponent<Rigidbody>();

            if (i == 0) continue;

            wires[i].AddComponent<ConfigurableJoint>();
            ConfigurableJoint joint = wires[i].GetComponent<ConfigurableJoint>();
            joint.connectedBody = wires[i-1].GetComponent<Rigidbody>();
            joint.axis = new Vector3(0, 1, 0);
            joint.autoConfigureConnectedAnchor = false;
            joint.xMotion = ConfigurableJointMotion.Limited;
            joint.yMotion = ConfigurableJointMotion.Limited;
            joint.zMotion = ConfigurableJointMotion.Limited;
            joint.angularXMotion = ConfigurableJointMotion.Locked;
            joint.angularYMotion = ConfigurableJointMotion.Locked;
            joint.angularZMotion = ConfigurableJointMotion.Locked;
            joint.linearLimit = new SoftJointLimit { limit = MaxWireLengthBetweenSegments, bounciness = 0.01f, contactDistance = 0.01f };

            // didn't work as planed but I'll leave the code here
            // float X_anchor = (wires[i-1].transform.position.x - wires[i].transform.position.x) / transform.localScale.x / 2;
            // float Y_anchor = (wires[i-1].transform.position.y - wires[i].transform.position.y) / transform.localScale.y / 2;
            // float Z_anchor = (wires[i-1].transform.position.z - wires[i].transform.position.z) / transform.localScale.z / 2;
            // joint.anchor = new Vector3(X_anchor, Y_anchor, Z_anchor);
            // joint.connectedAnchor = new Vector3(-X_anchor, -Y_anchor, -Z_anchor);
            joint.anchor = new Vector3(0, 0, 0);
            joint.connectedAnchor = new Vector3(-0, -0, -0);
        }

        // add fixed joints to end
        wires[0].AddComponent<FixedJoint>();
        FixedJoint jointStart = wires[0].GetComponent<FixedJoint>();
        jointStart.connectedBody = start.GetComponent<Rigidbody>();
        if (start.GetComponent<WireInteraction>() != null) {
            start.GetComponent<WireInteraction>().setNearestWireAndOppositeEnd(wires[0], end);
            start.GetComponent<WireInteraction>().setMaxDistanceFromOppositeEnd(wireMaxLength);
        }
        wires[0].GetComponent<SphereCollider>().isTrigger = true; // Prevent its collider from taking priority over the wire the player interacts with

        // add fixed joints to end
        wires[wireSegmentCount-1].AddComponent<FixedJoint>();
        FixedJoint jointEnd = wires[wireSegmentCount-1].GetComponent<FixedJoint>();
        jointEnd.connectedBody = end.GetComponent<Rigidbody>();
        if (end.GetComponent<WireInteraction>() != null) {
            end.GetComponent<WireInteraction>().setNearestWireAndOppositeEnd(wires[wireSegmentCount-1], start);
            end.GetComponent<WireInteraction>().setMaxDistanceFromOppositeEnd(wireMaxLength);
        }
        wires[wireSegmentCount-1].GetComponent<SphereCollider>().isTrigger = true; // Prevent its collider from taking priority over the wire the player interacts with
    }
}