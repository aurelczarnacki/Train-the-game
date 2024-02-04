using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridCreator : SingletonWithoutDontDestroy<GridCreator>
{
    public Transform railManagerPrefab;

    private float hexWidth = 1.73f;
    private float hexHeight = 2.0f;
    private Data data;
    private List<HexData> createdHexes = new List<HexData>();

    private void Start()
    {
        data = DataManager.Instance.data;
        CreateGrid();
    }

    public void GenerateNextLevelGrid()
    {
        GridGenerator.Instance.GenerateNextChunk();
        CreateGrid();
    }

    private Vector3 CalcWorldPos(Vector2 gridPos)
    {
        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = hexWidth / 2;

        float x = 0 - gridPos.x * hexWidth + offset;
        float z = 0 - gridPos.y * hexHeight * 0.75f;

        return new Vector3(x, 0, z);
    }
    public void CreateGrid()
    {
        foreach (HexData hexData in data.gridData.hexes)
        {
            if (!createdHexes.Any(hex => hex.Equals(hexData)))
            {
                Transform hexPrefab = BoardManager.Instance.GetHexPrefab(hexData.type);
                Transform hex = MakeGrid(hexData.offsetCoordinate, hexPrefab);
                if (hexData.type == HexType.WoodBridge)
                {
                    WaterHex waterHex = hex.GetComponent<WaterHex>();
                    waterHex.PlaceBridge(true);
                }

                if (hexData.objectType != ObjectType.None)
                {
                    Transform child = BoardManager.Instance.GetEntityPrefab(hexData.objectType);
                    Instantiate(child, hex);
                }
            }
            createdHexes.Add(hexData);

        }
        foreach (Vector2Int location in data.railLocations)
        {
            Hex hex = FindObjectsOfType<Hex>().FirstOrDefault(hex => hex.offsetCoordinate == location);
            if (hex != null)
            {
                hex.TryToCreateRail(true);
            }
        }
        Hex trainHex = FindObjectsOfType<Hex>().FirstOrDefault(hex => hex.offsetCoordinate == data.trainData.trainLocation);
        Transform trainPrefab = BoardManager.Instance.GetTrainPrefab();
        Instantiate(trainPrefab, trainHex.transform);
    }

    private Transform MakeGrid(Vector2Int offsetCoordinate, Transform material)
    {
        Transform hex = Instantiate(material) as Transform;

        hex.position = CalcWorldPos(offsetCoordinate);
        hex.parent = transform;
        hex.name = "Hexagon" + offsetCoordinate.x + "|" + offsetCoordinate.y;

        Hex hexComponent = hex.GetComponent<Hex>();
        hexComponent.offsetCoordinate = offsetCoordinate;
        return hex;
    }
}
