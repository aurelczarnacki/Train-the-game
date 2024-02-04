using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : SingletonWithoutDontDestroy<PanelManager>
{
    public GameObject craftingCarDisplay;
    public GameObject trainDisplay;
    public ItemShopDisplay itemShopDisplay;
    public GameObject pousePanel;

    public GameObject topPanel;
    public GameObject buildContainer;
    private GameObject currentlyActive;

    public void ShowCraftingCarDisplay()
    {
        CloseActive();
        craftingCarDisplay.SetActive(true);
        currentlyActive = craftingCarDisplay;
    }
    public void ShowTrainDisplay()
    {
        CloseActive();
        trainDisplay.SetActive(true);
        currentlyActive = trainDisplay;
    }
    public void ShowItemShopDisplay(Item item, bool isQuantity = false)
    {
        CloseActive();
        itemShopDisplay.gameObject.SetActive(true);
        itemShopDisplay.item = item;
        itemShopDisplay.isQuantity = isQuantity;
        currentlyActive = itemShopDisplay.gameObject;
    }
    public void ShowPousePanel(bool isActive)
    {
        CloseActive();
        topPanel.SetActive(!isActive);
        buildContainer.SetActive(!isActive);
        pousePanel.SetActive(isActive);
        currentlyActive = pousePanel;
    }
    public void CloseActive()
    {
        if (currentlyActive != null)
            currentlyActive.SetActive(false);
    }
}
