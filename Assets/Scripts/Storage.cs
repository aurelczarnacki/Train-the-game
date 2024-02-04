using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : SingletonWithoutDontDestroy<Storage>
{
    public bool BuyItem(Item item, int quantity = 1)
    {
        if (item.woodCost * quantity <= DataManager.Instance.data.playerData.wood && item.stoneCost * quantity <= DataManager.Instance.data.playerData.stone)
        {
            AddWood(-item.woodCost * quantity);
            AddStone(-item.stoneCost * quantity);

            Dictionary<Item, int> items = DataManager.Instance.data.playerData.itemsInventory.ToDictionary();

            if (!items.ContainsKey(item))
                items.Add(item, 0);
            
            if(item.itemType == ItemType.Carriage)
                Train.Instance.CreateCarObject(item);

            else items[item] += quantity;
            
            DataManager.Instance.data.playerData.itemsInventory = new SerializableDictionary<Item, int>(items);

            return true;
        }
        return false;
    }

    public void AddWood(int amount = 1)
    {
        DataManager.Instance.data.playerData.wood += amount;
    }

    public void AddStone(int amount = 1)
    {
        DataManager.Instance.data.playerData.stone += amount;
    }

}
