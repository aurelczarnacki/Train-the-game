using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterHex : Hex
{
    public Transform woodBridge;
    private bool isWoodBridge = false;
    public override void OnMouseDown()
    {
        if (BuildingManager.Instance.isBuilding && BuildingManager.Instance.selectedItem != null)
        {
            Dictionary<Item, int> itemsDictionary = data.playerData.itemsInventory.ToDictionary();
            if (BuildingManager.Instance.selectedItem.name == "Most" && itemsDictionary.ContainsKey(BuildingManager.Instance.selectedItem) && itemsDictionary[BuildingManager.Instance.selectedItem] > 0)
            {
                if (!isWoodBridge)
                {
                    PlaceBridge();
                }
            }
            if (BuildingManager.Instance.selectedItem.name == "Tor" && itemsDictionary.ContainsKey(BuildingManager.Instance.selectedItem) && itemsDictionary[BuildingManager.Instance.selectedItem] > 0 && isWoodBridge)
            {
                if (isWoodBridge)
                {
                    TryToCreateRail();
                }

            }
        }
    }

    public void PlaceBridge(bool isFree = false)
    {
        if (!isFree)
        {
            HexData hexDataToUpdate = data.gridData.hexes.Find(hexData => hexData.offsetCoordinate == offsetCoordinate);
            if (hexDataToUpdate != null)
            {
                hexDataToUpdate.type = HexType.WoodBridge;
            }
            Dictionary<Item, int> itemsDictionary = data.playerData.itemsInventory.ToDictionary();
            itemsDictionary[BuildingManager.Instance.selectedItem]--;
            DataManager.Instance.data.playerData.itemsInventory = new SerializableDictionary<Item, int>(itemsDictionary);
        }
        isWoodBridge = true;
        Transform newObject = Instantiate(woodBridge, transform);
        newObject.position = transform.position;
    }
}
