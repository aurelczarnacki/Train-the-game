using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildDisplay : MonoBehaviour
{
    public Transform itemsPanel;
    public ItemInventoryDisplay itemInventoryDisplay;

    public RectTransform buildContainer;

    public List<Item> allItems;

    private ItemType currentCategory;
    private Dictionary<Item, int> items;
    private BuildingManager buildingManager;

    private void Start()
    {
        items = DataManager.Instance.data.playerData.itemsInventory.ToDictionary();
        buildingManager = BuildingManager.Instance;
        ShowRoad();
    }
    public IEnumerator MoveBuildContainer(Vector2Int targetPosition)
    {
        float elapsedTime = 0f;
        Vector2 initialPosition = buildContainer.anchoredPosition;

        while (elapsedTime < 0.5f)
        {
            float t = elapsedTime / 0.5f;
            buildContainer.anchoredPosition = Vector2.Lerp(initialPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        buildContainer.anchoredPosition = targetPosition;
    }

    private void DestroyAllItems()
    {
        foreach (Transform child in itemsPanel)
        {
            Destroy(child.gameObject);
        }
    }

    private void DisplayBuildingButtonsForCategory(List<Item> items)
    {
        DestroyAllItems();
        foreach (Item item in items)
        {
            Transform buildButtonObj = Instantiate(itemInventoryDisplay.transform, itemsPanel);

            ItemInventoryDisplay buildingDisplay = buildButtonObj.GetComponent<ItemInventoryDisplay>();
            buildingDisplay.item = item;
        }
    }

    public void ShowCarriage()
    {
        currentCategory = ItemType.Carriage;
        List<Item> carriages = allItems
            .Where(item => item.itemType == ItemType.Carriage && !items.ContainsKey(item))
            .ToList();
        DisplayBuildingButtonsForCategory(carriages);
        buildingManager.selectedItem = null;
    }

    public void ShowRoad()
    {
        currentCategory = ItemType.Road;
        List<Item> roads = allItems
            .Where(item => item.itemType == ItemType.Road && items.ContainsKey(item) && items[item] > 0)
            .ToList();
        DisplayBuildingButtonsForCategory(roads);
    }

    private void Update()
    {
        Dictionary<Item, int> currentItems = DataManager.Instance.data.playerData.itemsInventory.ToDictionary();
        if (!items.SequenceEqual(currentItems))
        {
            items = currentItems;

            if (currentCategory == ItemType.Carriage)
            {
                ShowCarriage();
            }
            else
            {
                ShowRoad();
            }
        }
    }
}