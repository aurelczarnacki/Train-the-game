using Unity.VisualScripting;
using UnityEngine;

public class Car : CarMovement
{
    public void OnMouseDown()
    {
        PanelManager.Instance.ShowCraftingCarDisplay();
    }

}
