using UnityEngine;

public class PauseMenu_ButtonParent
{
    public virtual void Pressed() { Debug.Log("PauseMenu_ButtonParent Pressed() called"); }
}

public class PauseMenu_Resume : PauseMenu_ButtonParent
{
    public override void Pressed() { StaticPausingFunctions.UnpauseGame(); }
}

public class PauseMenu_Settings : PauseMenu_ButtonParent
{
    public override void Pressed() { StaticPausingFunctions.toggleSettings(); }
}

public class PauseMenu_Inventory : PauseMenu_ButtonParent
{
    public override void Pressed() { StaticPausingFunctions.showInventory(); }
}
