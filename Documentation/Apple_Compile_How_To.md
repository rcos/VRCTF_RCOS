# Apple Compile. By Nicholas Busaba

## Current Status

Can get at least a simulator open with the application, but the window size is too small (black bars around the cardboard view) and no way to use accelerometer/gyroscope sensors in a simulator.<br>
Potentially Helpful?:
 - https://github.com/ColinEberhardt/SimulatorEnhancements
 - https://discussions.unity.com/t/cardboard-vr-game-on-iphone-just-fills-half-the-screen/201641

## How to Compile
 - Get an Apple account
 - Make a developer Apple Account (I think here: https://developer.apple.com/programs/enroll/)
 - Get XCode. Use the Apple store if you can. If on an older mac use https://xcodereleases.com/ to get the right version
 - In XCode you need to set up a team with yourself. I can't remember the process because I did it a while ago now but it should work.
 - Make sure everything is pulled
 - Open the Unity Project
 - Project Settings -> Company Name = RPI RCC
 - Project Settings -> Settings for IOS -> Other Settings -> Bundle Identifier = set the team name correctly. I use com.vrctf.vrctf
 - Project Settings -> Settings for IOS -> Other Settings -> Signing Team ID = I think it depends on your XCode team. I use "Personal Team"
 - Project Settings -> Settings for IOS -> Other Settings -> Target Device = set to iPhone Only
 - Project Settings -> Settings for IOS -> Other Settings -> Target SDK = set to Simulator or Device depending on what you want
 - Project Settings -> Settings for IOS -> Other Settings -> Simulator Architecture = Universal
 - Project Settings -> Settings for IOS -> Other Settings -> Enable ProMotion = True
 - Project Settings -> XR Plug-in Management -> Settings for IOS -> Cardboard XR Plugin = True
 - If XCode gives a hard time, with errors chatGPT them. Worked for me. Can't remember the process anymore its been a bit
 - Select an Apple Device and then Compile and Build


## Continued by Keira Ang
- (After opening project on XCode) >> Targets >> Unity-iPhone >> Signing & Capabilities 
    - Automatically manage signing: ON
    - Team: Personal Team (this is automatically made when you sign in)
    - create unique bundle identifier
    - repeat for Targets >> UnityFramework
- Phone settings:
    - I chose to run on my iPhone. First, the phone needs to actually be plugged into the laptop through a cable.
    - Go to Settings >> Privacy & Security >> Security (scroll all the way down) >> Developer Mode: ON
        - this might ask you to restart your phone. do it.
    - This direction applies for after the build works, but for this one you hit up Settings >> General >> VPN & Device Management
        - Tap your developer certificate → Trust.
- Running
    - Make sure you click on Unity-iPhone in the directory on the left.
    - There is a bar at the top that lets you decide what to run and where you run it. This format is:
        - What2Run > Any iOS device (arm64)
        - make sure on the left side of the arrow, you're running "Unity-iPhone". My mistake was accidentally running UnityFramework; the build succeeded but there wasn't an actual app on my phone until I made it run the right file.
        - on the right side of the arrow, change "Any iOS deice (arm64)" to your iPhone name. This can also work directly on the mac, and it will work. For that, you can choose "My Mac (Designed for iPad)"
    - press run
        - it takes a bit; it's a big project. XCode will still be up, and has a live console for what is being displayed. If you are displaying on the iPhone, the app will open itself. 
        - YAY!!! make sure you double check everything. otherwise it might not work :0
