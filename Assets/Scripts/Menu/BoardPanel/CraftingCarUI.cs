using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingCarUI : MonoBehaviour
{
    public Transform itemsPanel;
    public RectTransform itemInventoryDisplay;

    public List<Item> allItems;

    public void Start()
    {
        DisplayBuildingButtonsForCategory();
    }
    private void DisplayBuildingButtonsForCategory()
    {
        foreach (Item item in allItems)
        {
            if (item.itemType == ItemType.Road)
            {
                Transform itemButtonObj = Instantiate(itemInventoryDisplay, itemsPanel);
                ItemInventoryDisplay itemDisplay = itemButtonObj.GetComponent<ItemInventoryDisplay>();
                itemDisplay.item = item;
            }
        }
    }

}
