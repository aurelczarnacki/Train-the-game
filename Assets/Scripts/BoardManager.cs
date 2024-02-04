using System.Collections.Generic;
using UnityEngine;

public class BoardManager : SingletonWithoutDontDestroy<BoardManager>
{
    public int woodCost { get; private set; } = 5;
    public int stoneCost { get; private set; } = 10;

    public List<Transform> waterHexPrefab;
    public List<Transform> stoneHexPrefab;
    public List<Transform> grassHexPrefab;

    public List<Transform> trainPrefab;

    public List<Transform> treePrefab;
    public List<Transform> grassPrefab;
    public List<Transform> rockPrefab;

    public int currentLevel { get; private set; } = 1;

    public Transform GetHexPrefab(HexType type)
    {
        switch(type){
            case (HexType.Grass):
                return grassHexPrefab[currentLevel];
            case (HexType.Stone):
                return stoneHexPrefab[currentLevel];
            case (HexType.Water):
                return waterHexPrefab[currentLevel];
            case (HexType.WoodBridge):
                return waterHexPrefab[currentLevel];
            default:
                Debug.Log($"Wrong hex type: {type}");
                return null;
        }
    }
    
    public Transform GetEntityPrefab(ObjectType type)
    {
        switch (type)
        {
            case (ObjectType.Grass):
                return grassPrefab[currentLevel];
            case (ObjectType.Rock):
                return rockPrefab[currentLevel];
            case (ObjectType.Tree):
                return treePrefab[currentLevel];
            default:
                Debug.Log($"Wrong entity type: {type}");
                return null;
        }
    }
    public Transform GetTrainPrefab()
    {
        return trainPrefab[currentLevel];
    }
    
    public void RankUp()
    {
        PlayerData playerData = DataManager.Instance.data.playerData;
        if (woodCost * currentLevel <= playerData.wood && stoneCost * currentLevel <= playerData.stone)
        {
            playerData.wood -= woodCost * currentLevel;
            playerData.stone -= stoneCost * currentLevel;
            currentLevel++;
        }

    }
}