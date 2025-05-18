# Wires. By Nicholas Busaba

## Overview

Wires are objects that can have two ends, can optionally be interacted be interacted with by the player, and can connect to any object specified as a Socket. Relevant Scripts:
 - `Assets/Scripts/WiringInteractions/WireSpawner.cs`
 - `Assets/Scripts/WiringInteractions/WireInteraction.cs`
 - `Assets/Scripts/WiringInteractions/Socket.cs`
<br><br>

All scripts check for certain conditions and send errors to Unity's log when they failed. These will help set up things correctly.<br>

Check out `Assets/Scenes/MachineInteractTest.unity` to see these scripts in action and get an example.


### WireSpawner.cs

This script is responsible for creating the wire. It can be placed on any gameobject and has no public methods. You only change public variables to affect how the wire is made.<br>
&ensp; `public Transform start` - The position of where the wire will start. Preferably a gameobject
&ensp; `public Vector3 start_offset` - An offset from the `start` variable where the wire will be created from. Use this if you don't want the wire appearing at the origin of the gameobject
&ensp; `public Transform end` - The position of where the wire will end. Preferably a gameobject
&ensp; `public Vector3 end_offset` - Same deal as `start_offset` expect now for the end of the wire.
&ensp; `public int wireSegmentCount` - The wire is made up of individual invisible spheres where a line is connected to them. This controls how many of those invisible spheres there will be. 2 is a straight line.
&ensp; `public float MaxWireLengthBetweenSegments` - The max distance between each individual invisible sphere that make up the wire. Larger numbers mean longer wires but also means they will be less rounded.
&ensp; `public float tensionLimitTillResetWholeWire` - When this tension limit is reached the entire wire to reset to how it was when the scene was created. 0 means this never happens.
&ensp; `public Material wireColorWhenUnplugged` - Color of the wire when one of the ends isn't connected to something
&ensp; `public Material wireColorWhenPlugged` - Color of the wire when both ends are connected to something
&ensp; `public float wireWidth` - The visual width of the wire. aka how thick is the wire
&ensp; `public float wireMaxLength` - The maximum length of the wire. Any movements past that distance will make it bounce back.

### WireInteraction.cs

Attach to the ends of the wire you want the user to be able to interact with and move around.
&ensp; `public float TensionTillDropped` - The amount of tension the wire can take before it bounces back to the other end and the player loses control over its position temporarily. Set to 0 to ignore it.
&ensp; `public GameObject socketToConnectToAtStart` - The socket this wire will connect to when the scene is created. If null nothing happens, otherwise the wire is connected to the socket the same way it would if a player moved the wire to the socket.

#### WireInteraction.cs - public void setNearestWireAndOppositeEnd(GameObject wireObject, Transform oppositeEnd)

Not recommended to be called outside of the `WireSpawner.cs` script. This sets the closest wire object to this end of the wire so its tension can checked, and sets the opposite end of the wire so it knows where to bounce back to when the tension limit was too high or the max distance of the wire (set in `WireSpawner.cs`) was exceeded.

#### WireInteraction.cs - public void setMaxDistanceFromOppositeEnd(float maxDistance)

Same deal as `setNearestWireAndOppositeEnd`. Except now it sets the maximum distance the wire can move before bouncing back.

#### WireInteraction.cs - public bool isConnectedToSomething()

Returns true if this end of the wire is connected to a socket, otherwise returns false.

#### WireInteraction.cs - public bool setAttachedToSocket(GameObject socket)

This does error checking and sets the  wire to be connected and moved to the socket in the parameter.
&ensp; `GameObject socket` - The socket that the wire will be connected to

#### WireInteraction.cs - public bool detachFromSocket(bool applyBounceBack = true)

This does error checking and removes the wire from the socket. It can also optionally bounce back from the outlet.
&ensp; `bool applyBounceBack` - If true the wire bounces back from the outlet towards the other end of the wire, otherwise it doesn't. Recommended to be false when the player did interact with the end of the wire, and true otherwise.

### Socket.cs

Attach this to objects on the "WireSocket" layer and they will act as points where wires can connect to.
&ensp; `public GameObject[] CanConnectTo` - An Array of all wire objects that can connect to this socket. Recommend to setup inside the Unity Editor. If a wire end isn't in this list, it's ignored when trying to connect it to this socket.
&ensp; `public bool InvertAboveArray` - When false the array above acts as a whitelist, when set to true it acts as a blacklist. So set to true and make the array empty when you want everything to connect to it.
&ensp; `public Vector3 Offset` - If you don't want the wire connecting to the origin of the gameobject this script is attached to, then use this to offset where it's connected to. Similar to `start_offset` and `end_offset` in `WireSpawner.cs`

#### Socket.cs - public void setWireConnectedTo(GameObject wireEnd)

This sets a private method to parameter for error checking later.
&ensp; `GameObject wireEnd` - wire that this socket is connected to now

#### Socket.cs - public GameObject getWireConnectedTo()

Returns the GameObject that is currently connected to the socket. Returns `Null` if none.

#### Socket.cs - public bool detachFromWire()

Handles error checking and removes the wire currently attached to the Socket. Returns `true` if it was able to be removed, `false` otherwise.

#### Socket.cs - public bool AttachToWire(GameObject wireEnd)

Handles error checking and attaches the wire in the function parameter. Returns `true` if it was able to be added, `false` otherwise.
&ensp; `GameObject wireEnd` - wire that this socket will be connected to
