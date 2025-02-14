# Scene Travel. By David Li

## Overview
The script in `Assets/Scripts/TravelController.cs` contains the script to allow moving to another scene.

Attach `TravelController.cs` to any GameObject in the `Interactible` layer, preferably an UI object. In the Inspector window's Script Component, give the exact name of the desired scene the object will send the user (leave out .unity).

When running the scene, interacting with the object will cause a fade to black. During this, the scene will change keeping the current `Player` object.

Note that there must be a root GameObject named "Player" containing the camera and relevant assets. If there is already an object named "Player" in the new scene, it will be deleted and replaced with the current scene's `Player`.

## Code Documentation

#### IEnumerator LoadScene()

Called when OnPointerClick() is called. Asynchronously loads the desired scene, prepares to send the current scene's `Player` object, and unloads the current scene. `LoadScene()` also waits for the fade to black to fully block the players view for 0.5 seconds.

#### void OnPointerClick()

Is called when the Interact Button is pushed when the reticle is on this object. It calls `TransitionArea()` from `PlayerMovement.cs`, which is attached to `Main Camera`, in order to start the fade to black. Afterwards it calls LoadScene().



