using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInventoryDisplay : MonoBehaviour, IPointerDownHandler
{
    
    public Item item;
    public Transform selected;
    public RectTransform itemShopPrefab;

    public Image artWorkImage;
    public TextMeshProUGUI amountText;

    private void Start()
    {
        artWorkImage.sprite = item.artWork;
    }
    private void Update()
    {
        if(item.itemType == ItemType.Carriage || transform.GetComponentInParent<CraftingCarUI>())
        {
            amountText.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            Dictionary<Item, int> items = DataManager.Instance.data.playerData.itemsInventory.ToDictionary();
            amountText.text = items[item].ToString();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (item.itemType == ItemType.Carriage)
        {
            PanelManager.Instance.ShowItemShopDisplay(item);
        }
        else if (transform.GetComponentInParent<CraftingCarUI>())
        {
            PanelManager.Instance.ShowItemShopDisplay(item, true);
        }
        else
        {
            BuildingManager.Instance.SelectBuilding(this);
        }
    }
}
