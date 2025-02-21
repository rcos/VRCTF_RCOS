# Inspector. By David Li

## Overview
The script in `Assets/Scripts/InspectController.cs` contains everything needed to inspect an object.

Attach `InspectController.cs` to any GameObject in the `Interactible` layer. In the Inspector window's Script Component, set the location (x, y, z) and scale (x, y, z) the object will appear as when inspecting. Also
set the active and inactive materials for when the reticle points towards or away from the object. Note that the scene must have a object designated with the "GameManager" tag and `ScenaroManager.cs`. See 
`ScenaroManager.md` for details. 


When running the scene, interacting with the object will cause it to transform to the specified position and scale. Viewing the object from an angle will cause it to turn towards the reticle. Interact again to end
inspection and return the object to its starting location and scale.

## Code Documentation

#### private bool spinning;

Is toggled when object is interacted with. When `true`, smoothRotate(...) is called each frame.

### public void smoothRotate(Quaternion rotationDifference, Vector3 rotationDirection)

Determines and then rotates the correct angles and axis of rotation the object should make this frame. <br>
Quaternion rotationDifference is the difference in rotation between the current camera direction and the direction from the camera to the object. <br>
Vector3 rotationDirection is the direction and axes from the camera to the object.

### public void ForceStop()

Called from the GameManager to force the object to stop being inspected.



