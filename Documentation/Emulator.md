# Emulator. By Nicholas Busaba and David Li

## Overview

The scripts in `Assets/Scripts/TestingScripts` and in `./Scripts` allow for testing within the Unity editor. `VREmulator.cs` in in `Assets/Scripts/TestingScripts` allows for camera movement and `Emulator_Add.sh`/`Emulator_Remove.sh` in in `./Scripts` turn on/off interactions. <br> <br>

## Setup

Step 1 <br>
Open up Unity. Go to Window > Package Manager. Click the "+" sign near the top right of the "Package Manager" window. If you see a Cardboard SDK already there, ignore this tep. Otherwise select "Install package from git URL..." and type in `https://github.com/rcos/VRCTF_RCOS-cardboard-xr-plugin.git`. It should then be installed.

Step 2 <br>
Add `VREmulator.cs` to the main camera object. You can adjust sensitivity in inspector.