# Apple Compile. By Nicholas Busaba, Keira Ang

## Current Status: 12/10
By Keire Ang
From what I've seen, it won't be possible to build it on your phone and run it on the simulator simulatenously. For this reason, I've updated this document to say "Device SDK", because otherwise it won't run on the phone at all. Should someone be able to function it on the simulator, the documentation may be udpated. But the only use of running it on the simulator is for people with a macbook and no iPhone, which usually is not the case; it is easier to just open it on an iPhone.
On the iPhone itself, the build now runs. The VR aspect with the camera works. But what doesn't work is the click functionality itself; after the first click is made (which I'm unsure if this is based on what is in the center of the screen, or with the crosshair function), no other clicks are processed. The first interaction does work, though, which means that the SDK probably isn't the problem itself, and it is not crashing as it has in the past.


## Past Status
By Nicholas Busaba
Can get at least a simulator open with the application, but the window size is too small (black bars around the cardboard view) and no way to use accelerometer/gyroscope sensors in a simulator.<br>

Potentially Helpful?:
 - https://github.com/ColinEberhardt/SimulatorEnhancements
 - https://discussions.unity.com/t/cardboard-vr-game-on-iphone-just-fills-half-the-screen/201641

## Xcode Account
 - Get an Apple account
 - Make a developer Apple Account (I think here: https://developer.apple.com/programs/enroll/)
 - Get XCode. Use the Apple store if you can. If on an older mac use https://xcodereleases.com/ to get the right version
 - In XCode you need to set up a team with yourself. I can't remember the process because I did it a while ago now but it should work.
 
 ## Unity
 - Make sure everything is pulled
 - Open the Unity Project
 - Project Settings -> Company Name = RPI RCC
 - Project Settings -> Settings for IOS -> Other Settings -> Bundle Identifier: com.vrctf.vrctf
 - Project Settings -> Settings for IOS -> Other Settings -> Signing Team ID = I think it depends on your XCode team. I use "Personal Team"
 - Project Settings -> Player -> Other Settings -> Identification -> Automatically Sign: True
 - Project Settings -> Settings for IOS -> Other Settings -> Target Device: iPhone Only
 - Project Settings -> Settings for IOS -> Other Settings -> Target SDK: Device SDK
 - Project Settings -> Settings for IOS -> Other Settings -> Enable ProMotion: True
 - Project Settings -> XR Plug-in Management -> Settings for IOS -> Cardboard XR Plugin: True
 - Build Profile/Settings -> Build Scenes -> Select Home Scene and Situation 1 (or whichever scenes you want to run)
 - Select an Apple Device and then Compile and Build
    - Build is saved as "Unity-iPhone.xcodeproj"
    - If you want to re-build, no need to delete original; save in the same location, and click "Replace" when prompted


## Xcode
- Open the project on XCode
- Targets >> Unity-iPhone >> Signing & Capabilities 
    - Automatically manage signing: TRUE
    - Team: Personal Team (this is automatically made when you sign in)
    - create unique bundle identifier
    - repeat for Targets >> UnityFramework
- Running
    - Make sure you click on Unity-iPhone in the directory on the left.
    - There is a bar at the top that lets you decide what to run and where you run it. This format is:
        - What2Run > Any iOS device (arm64)
        - make sure on the left side of the arrow, you're running "Unity-iPhone". My mistake was accidentally running UnityFramework; the build succeeded but there wasn't an actual app on my phone until I made it run the right file.
        - on the right side of the arrow, change "Any iOS deice (arm64)" to your iPhone name. This can also work directly on the mac, and it will work. For that, you can choose "My Mac (Designed for iPad)"
    - press run
        - it takes a bit; it's a big project. XCode will still be up, and has a live console for what is being displayed. If you are displaying on the iPhone, the app will open itself. 

## Phone Settings 
- You need to download TestFlight for this to work.
- I chose to run on my iPhone. First, the phone needs to actually be plugged into the laptop through a cable. This cannot be done wirelessly.
- Go to Settings >> Privacy & Security >> Security (scroll all the way down) >> Developer Mode: ON
    - this might ask you to restart your phone. do it.
- This direction applies for after the build works, but for this one you go to: Settings >> General >> VPN & Device Management
    - Tap your developer certificate → Trust
- This project must be rebuilt on the phone every time; while the app apears on the iPhone as its own app, it will not allow you to open it after too long.
- 
