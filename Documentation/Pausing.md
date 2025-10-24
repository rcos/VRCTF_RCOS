# Pausing. By Nicholas Busaba

## Overview

To pause the game the user is teleported to a specific location, the default being <1000,1000,1000>, and can access various options when paused. As of right now its rather primitive and simple. There are some ways to get information on the current paused state and updating how 

## Relevant Files

`./Gyroscope Testing/Assets/Scripts/Pausing/PauseMenuButtonScript.cs` - When paused, this is the script attached to the buttons in front of you<br>
`./Gyroscope Testing/Assets/Scripts/Pausing/PauseOnHoldLogic.cs` - The logic to allow pausing after holding down interact for a second or 2<br>
`./Gyroscope Testing/Assets/Scripts/Pausing/StaticPausingFunctions` - All static functions to allow for pausing and unpausing. Also is used for opening up the settings menu within the pause area.<br><br>
`./Gyroscope Testing/Assets/Resources/Pausing/IndividualButton.prefab` - the Prefab for the buttons you see on the pause screen such as "Resume"<br>
`./Gyroscope Testing/Assets/Resources/Pausing/PauseArea.prefab` - the Prefab for the ENTIRE pause area. This is loaded dynamically if not already loaded and reused if already loaded.<br><br>

`./Gyroscope Testing/Assets/Materials/Materials/Pausing` - Contains all materials for the pause area. As of writing this they are all static colors meant to represent everything seen in the pause area.

## Customize Pausing

### Changing Pause Area location

Example: `StaticPausingFunctions.pauseAreaLocation = new Vector3(1000f,1000f,1000f)` <br><br>

By default the player teleports to the pause room which is located at coordinates <1000,1000,1000>. If you would like to change it's location just update the vector like I showed above.

### Update player location when unpaused

Example: `StaticPausingFunctions.lastKnownPlayerPosition = new Vector3(2f,5f,12f)` <br><br>

Anytime the game is paused this value is automatically set to the last player location. If you want to change that then modify the variable like I do above.

### Prevent and Allow pausing

Example (prevent pausing): `StaticPausingFunctions.allowedToPause = false` <br>
Example (prevent pausing): `StaticPausingFunctions.allowedToPause = true` <br><br>

If you want to prevent the player from pausing and allow it again to prevent potential problems, change this variable.

### Check pausing and unpausing status

Example (check if unpausing): `if (StaticPausingFunctions.currentlyUnPausing) {}` <br>
Example (check if paused): `if (StaticPausingFunctions.currentlyPaused) {}` <br>
Example (check if pausing): `if (StaticPausingFunctions.currentlyPausing) {}` <br><br>

These variables can be used to get information on the current state of pausing. So info like if you are currently paused, currently teleporting to the pause room, and if you're teleporting away from the pause room can be grabbed here.

### Toggle settings display in pause menu

Example: `StaticPausingFunctions.toggleSettings()` <br><br>

Not recommended to be called manually, but this changes weather the settings menu in the pause area is present or not.

### Pause game

Example: `StaticPausingFunctions.PauseGame()` <br><br>

Call this to pause the game. The screen will fade to black and you will teleport to the pause area. If it doesn't exist already it will be added to the scene. If it does exist that instance of the pause area is used.

### Unpause game

Example: `StaticPausingFunctions.UnpauseGame()` <br><br>

Call this to unpause the game. The screen will fade to black and you will teleport back to where you were previously. The variable that stores the last location is `StaticPausingFunctions.lastKnownPlayerPosition`.

### Modifying button options when paused

Example (Resume button and Settings button): `StaticPausingFunctions.allPauseOptions = { ("Resume", typeof(PauseMenu_Resume)), ("Settings", typeof(PauseMenu_Settings)) }` <br>
```C#
public class PauseMenu_Resume : PauseMenu_ButtonParent {
    public override void Pressed() { StaticPausingFunctions.UnpauseGame(); }
}
public class PauseMenu_Settings : PauseMenu_ButtonParent {
    public override void Pressed() { StaticPausingFunctions.toggleSettings(); }
}
```
<br><br>

By creating a class that inherits from `PauseMenu_ButtonParent` and has a `Pressed` function we can create custom buttons. Just modify the array `allPauseOptions` to have a string and a class like the two shown above. Write the class in `StaticPausingFunction.cs` to keep it with the others