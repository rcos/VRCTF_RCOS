using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class MachineInteractScene_ExampleScript : MonoBehaviour
{
    public GameObject wireYouInteractWith;
    public GameObject Socket;
    public TextMeshPro tmp;

    private InputAction detachSocket_FromSocket;
    private InputAction detachSocket_FromWire;
    private InputAction attachSocket_FromSocket;
    private InputAction attachSocket_FromWire;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnEnable()
    {
        detachSocket_FromSocket = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/m");
        detachSocket_FromWire = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/j");
        attachSocket_FromSocket = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/n");
        attachSocket_FromWire = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/h");

        detachSocket_FromSocket.Enable();
        detachSocket_FromWire.Enable();
        attachSocket_FromSocket.Enable();
        attachSocket_FromWire.Enable();
    }

    void OnDisable()
    {
        detachSocket_FromSocket.Disable();
        detachSocket_FromWire.Disable();
        attachSocket_FromSocket.Disable();
        attachSocket_FromWire.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (detachSocket_FromSocket.WasPerformedThisFrame())
        {
            Socket.GetComponent<Socket>().detachFromWire();
        }
        if (detachSocket_FromWire.WasPerformedThisFrame())
        {
            wireYouInteractWith.GetComponent<WireInteraction>().detachFromSocket();
        }


        if (attachSocket_FromSocket.WasPerformedThisFrame())
        {
            Socket.GetComponent<Socket>().AttachToWire(wireYouInteractWith);
        }
        if (attachSocket_FromWire.WasPerformedThisFrame())
        {
            wireYouInteractWith.GetComponent<WireInteraction>().setAttachedToSocket(Socket);
        }


        if (wireYouInteractWith.GetComponent<WireInteraction>().isConnectedToSomething())
        {
            tmp.text = "Power :)";
        }
        else
        {
            tmp.text = "No Power :(";
        }
    }
}
