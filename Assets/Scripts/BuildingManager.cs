using System.Linq;
using UnityEngine;

public class BuildingManager : SingletonWithoutDontDestroy<BuildingManager>
{
    public Item selectedItem;
    public Transform itemsPanel;
    public bool isBuilding { get; private set; }
    public BuildDisplay buildDisplay;
    
    public void EnterOrExitBuildMode()
    {
        isBuilding = !isBuilding;

        if (isBuilding)
        {
            StartCoroutine(buildDisplay.MoveBuildContainer(new Vector2Int(0, 100)));
        }
        else
        {
            StartCoroutine(buildDisplay.MoveBuildContainer(new Vector2Int(0, -100)));
            selectedItem = null;
        }
    }
    public void SelectBuilding(ItemInventoryDisplay itemInventoryDisplay)
    {
        ItemInventoryDisplay oldItemInventoryDisplay = itemsPanel.GetComponentsInChildren<ItemInventoryDisplay>()
            .FirstOrDefault(display => display.item == selectedItem);

        if (oldItemInventoryDisplay != null)
        {
            oldItemInventoryDisplay.selected.gameObject.SetActive(false);
        }

        itemInventoryDisplay.selected.gameObject.SetActive(true);
        selectedItem = itemInventoryDisplay.item;
    }
}
