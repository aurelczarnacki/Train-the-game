using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Train : SingletonWithoutDontDestroy<Train>
{   
    public int maxCarQuantity = 1;
    public CarMovement carMovement;

    private List<CarMovement> carsMovement = new List<CarMovement>();
    private RailManager currentRail;
    private void Start()
    {
        carMovement = transform.AddComponent<CarMovement>();
        AddCarMovement(carMovement);

        InitializeTrainCars();
    }
    private void Update()
    {
        if(currentRail != carMovement.currentRail)
        {
            currentRail = carMovement.currentRail;
            DataManager.Instance.data.trainData.trainLocation = currentRail.hexComponent.offsetCoordinate;
        }
    }
    public void OnMouseDown()
    {
        PanelManager.Instance.ShowTrainDisplay();
    }
    private void InitializeTrainCars()
    {
        Dictionary<Item, int> items = DataManager.Instance.data.playerData.itemsInventory.ToDictionary();

        foreach (Item item in items.Keys)
        {
            if (item.itemType == ItemType.Carriage && carsMovement.Count - 1 < maxCarQuantity)
            {
                CreateCarObject(item);
            }
        }
    }
    public void ResetTrain()
    {
        for (int i = carsMovement.Count - 1; i >= 0; i--)
        {
            CarMovement car = carsMovement[i];
            car.SetCarPosition(RailQueue.Instance.GetRailAtIndex(carsMovement.Count - 1 - i));
        }
    }
    public void CreateCarObject(Item item)
    {
        RailManager currentRail = transform.parent.GetComponentInChildren<RailManager>();
        RailManager firstFreeRail = RailQueue.Instance.GetRailAtIndex(currentRail.QueueId - carsMovement.Count - 1);

        Transform newCarTranform = Instantiate(item.prefab3D, firstFreeRail.transform.parent);

        AddCarMovement(newCarTranform.GetComponent<Car>());
    }
    public void AddCarMovement(CarMovement carMovement)
    {
        carsMovement.Add(carMovement);
        carMovement.carNumber = carsMovement.IndexOf(carMovement);
    }
}