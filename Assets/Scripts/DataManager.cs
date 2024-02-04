using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : SingletonWithoutDontDestroy<DataManager>
{
    public int gridWidth = 20;
    public int gridHeight = 11;

    public Data data = new Data();

    private string filePath = Application.dataPath + $"/Saves/{SaveManager.Instance.nickName}.json";
    protected override void Awake()
    {
        base.Awake();

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<Data>(json);
        }
        else
        {
            CreateDataContext();
        }
    }

    protected override void OnApplicationQuit()
    {
        base.OnApplicationQuit();
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
    }
    public void CreateRail(Vector2Int offsetCoordinate)
    {
        foreach (Vector2Int location in data.railLocations)
        {
            if (location == offsetCoordinate)
                return;
        }
        data.railLocations.Add(offsetCoordinate);
    }
    public void RemoveEntity(Vector2Int offsetCoordinate)
    {
        foreach (HexData hexData in data.gridData.hexes)
        {
            if (hexData.offsetCoordinate == offsetCoordinate)
            {
                hexData.objectType = ObjectType.None;
                continue;
            }
        }
    }

    private void CreateDataContext()
    {
        data.gridData = new GridData();
        data.gridData.hexes = new List<HexData>();
        data.gridData.gridWidth = gridWidth;
        data.gridData.gridHeight = gridHeight;

        data.trainData = new TrainData();
        data.trainData.trainLevel = 1;
        data.trainData.trainLocation = new Vector2Int(0, 5);

        data.playerData = new PlayerData();
        data.playerData.itemsInventory = new SerializableDictionary<Item, int>(new Dictionary<Item, int>());

        data.playerData.stone = 0;
        data.playerData.wood = 0;

        List<Vector2Int> railLocations = new List<Vector2Int>
        {
        new Vector2Int(0, 5),
        new Vector2Int(1, 5),
        new Vector2Int(2, 5)
        };

        data.railLocations = railLocations;
    }
}
