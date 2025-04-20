# Scene Lighting. By David Li

## Overview
Lighting is up to the scenario designer's discretion and this only applies to sample scenarios developed by the RCOS RCC team. As of last update this only involves Scenario1.unity.

Lighting utilizes exclusively baked lighting alongside lightprobes for non-static/moving objects. Realtime and mixed lighting could be implemented, but this sample scenarios avoided this for simplicity and performance.
Lightmap information are stored in `Assets/Scenes/Lighting`. For example, scenario 1's lightmap file is stored in `Assets/Scenes/Lighting`, and the data and textures for that lightmap is stored in `Assets/Scenes/Lighting/Scenario1`. Light probe group objects are found in the scene around non-static objects and complicated meshes.

Note that when designing a scenario's scene with this lighting system that only static objects preset at generation will be affected by the lightmap. If you add/move a static object to the scene, you must
regenerate the lightmap to accommodate the change. If you add/move a non-static object, you must have a light probe field around the object to disperse light onto it.



