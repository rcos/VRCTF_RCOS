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