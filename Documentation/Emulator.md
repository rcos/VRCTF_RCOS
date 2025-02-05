# Emulator. By Nicholas Busaba and David Li

## Overview

The scripts in `Assets/Scripts/TestingScripts` and in `./Scripts` allow for testing within the Unity editor. `VREmulator.cs` in in `Assets/Scripts/TestingScripts` allows for camera movement and `Emulator_Add.sh`/`Emulator_Remove.sh` in in `./Scripts` turn on/off interactions. <br> <br>

## Setup

Step 1 <br>
Add `VREmulator.cs` to the main camera object. Can adjust sensitivity in inspector.

Step 2<br>
Run `Emulator_Add.sh` in `./Scripts`. It will change `CardboardReticlePointer.cs` in the Cardboard Plugin to allow interactions in the editor.
Run `Emulator_Remove.sh` to undo the changes.