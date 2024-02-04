using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridGenerator : SingletonWithoutDontDestroy<GridGenerator>
{
    public float treeChance = 0.3f;
    public float grassChance = 0.05f;

    public int chunkWidth = 20;
    public int chunkHeight = 11;

    private Data data;

    private void Start()
    {
        data = DataManager.Instance.data;
        if(data.gridData.hexes.Count == 0)
        {
            CreateGridData();
            data.gridData.hexes.Sort(new HexDataComparer());
        }
    }
    public void GenerateNextChunk()
    {
        data.gridData.gridWidth += chunkWidth;
        bool[,] occupiedPositions = new bool[data.gridData.gridWidth, chunkHeight];

        for (int x = data.gridData.gridWidth - chunkWidth; x < data.gridData.gridWidth; x++)
        {
            for (int y = 0; y < chunkHeight; y++)
            {
                HexData hexData = new HexData
                {
                    offsetCoordinate = new Vector2Int(x, y),
                };
                hexData.type = GetRandomHexType(Random.Range(1, 100));

                if (!occupiedPositions[x, y])
                {
                    CreateCluster(hexData, occupiedPositions);
                }

            }
        }
        data.gridData.hexes.Sort(new HexDataComparer());
    }
    private void CreateGridData()
    {
        bool[,] occupiedPositions = new bool[chunkWidth, chunkHeight];

        for (int x = 0; x < chunkWidth; x++)
            {
            for (int y = 0; y < chunkHeight; y++)
            {
                HexData hexData = new HexData
                {
                    offsetCoordinate = new Vector2Int(x, y),
                };
                if (x < 5)
                {
                    if (Random.Range(0f, 1f) < grassChance )
                    {
                        hexData.objectType = ObjectType.Grass;
                    }
                    hexData.type = HexType.Grass;
                    data.gridData.hexes.Add(hexData);
                }
                else
                {
                    hexData.type = GetRandomHexType(Random.Range(1, 100));

                    if (!occupiedPositions[x, y])
                    {
                        CreateCluster(hexData, occupiedPositions);
                    }
                }

            }
        }
    }
    private HexType GetRandomHexType(int random)
    {
        if (random <= 15)
        {
            return HexType.Water;
        }
        if (random <= 45)
        {
            return HexType.Stone;
        }
        return HexType.Grass;
    }

    private void CreateCluster(HexData startHex, bool[,] occupiedPositions)
    {
        int clusterSize = Random.Range(1, 5);
        for (int yOffset = 0; yOffset < clusterSize; yOffset++)
        {
            for (int xOffset = 0; xOffset < clusterSize; xOffset++)
            {
                int x = startHex.offsetCoordinate.x + xOffset;
                int y = startHex.offsetCoordinate.y + yOffset;

                if (x < data.gridData.gridWidth && y < data.gridData.gridHeight && !occupiedPositions[x, y])
                {
                    HexData hexData = new HexData
                    {
                        offsetCoordinate = new Vector2Int(x, y),
                    };
                    hexData.type = startHex.type;
                    if (startHex.type == HexType.Grass)
                    {
                        if (Random.Range(0f, 1f) < treeChance && x > 5)
                        {
                            hexData.objectType = ObjectType.Tree;
                        }
                        else if (Random.Range(0f, 1f) < grassChance)
                        {
                            hexData.objectType = ObjectType.Grass;
                        }
                    }
                    else if (startHex.type == HexType.Stone)
                    {
                        hexData.objectType = ObjectType.Rock;
                    }

                    data.gridData.hexes.Add(hexData);

                    occupiedPositions[x, y] = true;
                }
            }
        }
    }
    private class HexDataComparer : IComparer<HexData>
    {
        public int Compare(HexData hex1, HexData hex2)
        {
            if (hex1.offsetCoordinate.x == hex2.offsetCoordinate.x)
            {
                return hex1.offsetCoordinate.y.CompareTo(hex2.offsetCoordinate.y);
            }
            return hex1.offsetCoordinate.x.CompareTo(hex2.offsetCoordinate.x);
        }
    }
}