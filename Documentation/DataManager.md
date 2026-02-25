# DataManager

By Sathya R

## Overview
The script in `Assets/Scripts/Data/DataManager.cs` is a Singleton responsible for managing game saving and loading.

It coordinates all scripts that implement `IDataManager`, collects their data when saving, and distributes loaded data when loading. It ensures there is only one active instance in the scene.

Attach `DataManager.cs` to a single persistent GameObject (commonly named GameManager). This object should exist in every scene where saving/loading is required.

When the game starts, the `DataManager`:

- Creates a `FileDataHandler`
- Finds all objects implementing `IDataManager`
- Loads existing save data (or creates new data if none exists)

When the application quits, it automatically saves the game.

## Code Documentation

### public static DataManager Instance
Singleton instance of the `DataManager`.
Ensures only one instance exists at runtime.

### public void NewGame()
Creates a new `GameData` object.

Called when:

- No save file exists
- A fresh start is required

Resets all tracked game data to default values.

### public void LoadGame()
Loads saved game data from disk using `FileDataHandler`.

If no save file exists:

- Logs a message
- Calls `NewGame()`

After loading, it pushes the loaded `GameData` to all scripts implementing `IDataManager` by calling:

`LoadData(GameData data)`

on each registered object.

### public void SaveGame()
Collects updated data from all `IDataManager` scripts.

Each registered script updates the shared `GameData` object via:

`SaveData(ref GameData data)`

After collecting updated data, the `DataManager` passes it to `FileDataHandler` to serialize and write to disk.

### public void OnApplicationQuit()
Automatically calls `SaveGame()` when the application closes.

Ensures progress is not lost when exiting the game.

### private List FindAllDataManagerObjects()
Finds all active `MonoBehaviour` scripts in the scene that implement `IDataManager`.

Returns them as a list so the `DataManager` can:

- Push data to them (Load)
- Pull data from them (Save)

**Note:** Scripts must inherit from `MonoBehaviour` and implement `IDataManager` for this to work.