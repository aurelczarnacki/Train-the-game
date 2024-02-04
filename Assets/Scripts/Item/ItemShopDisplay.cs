using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopDisplay : MonoBehaviour
{
    public TextMeshProUGUI itemPriceWoodText;
    public TextMeshProUGUI itemPriceStoneText;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    public TextMeshProUGUI itemQuantityText;
    public RectTransform quantity;
    public bool isQuantity;

    public Slider quantitySlider;

    public Item item;

    private void Update()
    {
        itemNameText.text = item.name;
        itemDescriptionText.text = item.description;

        quantity.gameObject.SetActive(isQuantity);
        quantitySlider.minValue = 1;
        quantitySlider.maxValue = maxQuantity();
        itemQuantityText.text = "Iloœæ: " + quantitySlider.value.ToString();

        itemPriceWoodText.text = (item.woodCost * Mathf.Max(quantitySlider.value, 1)).ToString();
        itemPriceStoneText.text = (item.stoneCost * Mathf.Max(quantitySlider.value, 1)).ToString();
    }
    public int maxQuantity()
    {
        Data data = DataManager.Instance.data;
        int maxWood = data.playerData.wood / item.woodCost;
        int maxStone = (item.stoneCost > 0) ? (data.playerData.stone / item.stoneCost) : data.playerData.stone;

        int maxPossibleQuantity = Mathf.Min(maxWood, maxStone);

        return Mathf.Max(maxPossibleQuantity, 0);

    }
    public void PurchaseItem()
    {
        int quantity = (int)quantitySlider.value;
        if(quantity > 0)
        {
            Storage.Instance.BuyItem(item, quantity);
        }
        gameObject.SetActive(false);

    }
}
