using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexData
{
    public Vector2Int offsetCoordinate;
    public HexType type;
    public ObjectType objectType;
}
[Serializable]
public enum ObjectType
{
    None,
    Tree,
    Rock,
    Grass
}
[Serializable]
public enum HexType
{
    Water,
    WoodBridge,
    Grass,
    Stone
}
[Serializable]
public class GridData
{
    public List<HexData> hexes;
    public int gridWidth;
    public int gridHeight;
}
[Serializable]
public class TrainData
{
    public Vector2Int trainLocation;
    public int trainLevel;
}
[Serializable]
public class PlayerData
{
    public int wood;
    public int stone;
    public SerializableDictionary<Item, int> itemsInventory;
}

[Serializable]
public class Data
{
    public TrainData trainData;
    public PlayerData playerData;
    public List<Vector2Int> railLocations;
    public GridData gridData;
}


[System.Serializable]
public class SerializableDictionary<TKey, TValue>
{
    [SerializeField]
    private List<TKey> keys;
    [SerializeField]
    private List<TValue> values;

    public SerializableDictionary(Dictionary<TKey, TValue> dictionary)
    {
        keys = new List<TKey>(dictionary.Keys);
        values = new List<TValue>(dictionary.Values);
    }

    public Dictionary<TKey, TValue> ToDictionary()
    {
        var dictionary = new Dictionary<TKey, TValue>();
        int count = Mathf.Min(keys.Count, values.Count);
        for (int i = 0; i < count; i++)
        {
            dictionary.Add(keys[i], values[i]);
        }
        return dictionary;
    }

}
public enum ItemType
{
    Carriage,
    Road,
}