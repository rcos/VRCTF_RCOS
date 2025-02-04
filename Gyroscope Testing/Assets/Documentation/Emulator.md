# Emulator. By Nicholas Busaba

## Overview

The scripts in `Assets/TestingScripts` allow for testing within the Unity editor. `VREmulator.cs` allows for camera movement and `CardboardReticlePointerEmulator.cs` allows for interactions. <br><br>

## Setup

Add `VREmulator.cs` to the main camera object. Can adjust sensitivity in inspector.

Add `CardboardReticlePointerEmulator.cs` to `Packages/Google Cardboard XR Plugin/Resources/Runtime`.

Replace `CardboardReticlePointer.cs` with `CardboardReticlePointerEmulator.cs` in the CardboardReticlePointer Prefab. Set Reticle Interaction Layer Mask with Interactive.