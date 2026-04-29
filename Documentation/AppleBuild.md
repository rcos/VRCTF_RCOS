# Apple Compile
By Aidan Hoover
Based off Apple Compile By Nicholas Busaba

## Current Status
 - Window size is too small (black bars around the cardboard view) and off centered.
 - Scenario 1, 2, and main menu should work with iOS.

## How to Compile
 - Get an Apple account
 - Make a developer Apple Account (https://developer.apple.com/programs/enroll/)
 - Get XCode. Use the Apple store if you can. If on an older mac use https://xcodereleases.com/ to get the right version
 - In XCode you need to set up a team with yourself. 
 - Make sure everything is pulled
 - Open the Unity Project

## Setting Up Unity
 - Projects -> Add -> select "Gyroscope Testing"
 - File -> Build Profiles -> iOS -> 
     - See "No iOS module loaded." -> Click "Install with Unity Hub"
     - Install iOS Build Support, reload editor
 - File -> Build Profiles -> iOS -> "Switch Platform"
 - File -> Open Scene -> Assets -> Scenes -> Home.unity 


**Unity Produced File Modifications**
 - Search for file CardboardReticlePointer.cs (Packages/com.google.xr.cardboard/Runtime/CardboardReticlePointer.cs)
     - in Update():
         - find line: 
            ```
            if (Google.XR.Cardboard.Api.IsTriggerPressed || Mouse.current.leftButton.isPressed)
            ```
         - replace with: 
            ```
            if (Google.XR.Cardboard.Api.IsTriggerPressed || (Mouse.current != null && Mouse.current.leftButton.isPressed))
            ```
     - The issue this fixes is allowing a touch to iPhone screen to work as an interaction.

**Unity Settings and Build**
 - Edit -> Project Settings -> 
     - -> Player
        - Company Name = RPI RCC
        - Settings for IOS -> Other Settings -> 
            - Bundle Identifier = set the team name correctly. ex: com.vrctf.vrctf
            - Target Device = set to iPhone Only
            - Target SDK = set to Simulator or Device depending on what you want
            - Enable ProMotion = True
     - -> XR Plug-in Management -> Settings for IOS -> Cardboard XR Plugin = True
 - File -> Build Profiles -> iOS (active) or switch platform to it if not active
 - Build, choose new folder

## XCode Settings and Run
 - Finder -> Build Folder -> Info.plist
     -  (+) Privacy - NSCameraUsageDescription or Privacy - Camera Usage Description
     - Set string to "Camera is used to scan VR headset QR codes for Cardboard configuration."
     - *This will reset every build but after the first run with it on an iPhone that iPhone won't need it again.*
 - Finder -> Build Folder -> open Unity-iPhone.xcworkspace
 - In XCode:
     - Unity-iPhone (target)
         - Signing and Capabilities
             - Automatically manage signing = true
             - Set "team" to personal team
     - Select iOS device and run


## Next Steps
 - GitHub actions to perform compliance checks to ensure futher development doesn't break compatibility.


## Repo Files Modified
- Gyroscope Testing/Assets/Scripts/Pausing/UserPauseStatus.cs 
    - In ```RunEveryFrame(GameObject cur)```:
        - line 84: 
            ```
            bool isButtonPressed = Google.XR.Cardboard.Api.IsTriggerPressed || Mouse.current.leftButton.isPressed;
            ```
        - replaced with: 
            ```
            bool isButtonPressed = Google.XR.Cardboard.Api.IsTriggerPressed || (Mouse.current != null && Mouse.current.leftButton.isPressed);
            ```
    - The issue this fixes is taking a touch to iPhone screen as a pause invoking interact.
 - Gyroscope Testing/Assets/Scripts/KeyboardScripts/Keyboard_3D.cs
    - In ```Keyboard_3D_Static::makeNewKeyboardObject()```:
        - added the following after ```if (prefab == null) {...}```:
            ```
            instance = Object.Instantiate(prefab);
            if (instance.GetComponent<Keyboard_3D>() == null) {
                Debug.LogError("Keyboard_3D missing from prefab. Adding at runtime.");
                instance.AddComponent<Keyboard_3D>();
            }
            ```
        - replaced: ```return Object.Instantiate(prefab);``` with ```return instance;```
    - The issue this fixes is for iOS builds, Keyboard_Base.prefab may instantiate without the Keyboard_3D component due to Unity build inconsistencies. This checks and forces the component to be added at runtime. 