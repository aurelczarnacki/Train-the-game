using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Hex : MonoBehaviour
{
    public Transform railManager;
    public Transform billboardPrefab;
    public Vector2Int offsetCoordinate;

    protected Material originalMaterial;
    protected Material highlightMaterial;
    protected Renderer hexRenderer;
    protected Data data;

    public void Start()
    {
        hexRenderer = GetComponent<Renderer>();
        data = DataManager.Instance.data;
        originalMaterial = hexRenderer.material;

        highlightMaterial = new Material(originalMaterial)
        {
            color = Color.yellow
        };
        if ((offsetCoordinate.x + 1) % 10 == 0 && offsetCoordinate.y == 10)
        {
            Transform billboard = Instantiate(billboardPrefab, transform);
            billboard.localPosition = new Vector3(0f, 1.5f, -2f);
            billboard.GetComponentInChildren<TextMeshProUGUI>().text = (offsetCoordinate.x + 1).ToString() + " m\n|";
        }
        else if (offsetCoordinate.x == 0 && offsetCoordinate.y == 10)
        {
            Transform billboard = Instantiate(billboardPrefab, transform);
            billboard.localPosition = new Vector3(1.5f, 1.5f, -2f);
            billboard.GetComponentInChildren<TextMeshProUGUI>().text = (offsetCoordinate.x).ToString() + " m\n|";
        }
    }
    public virtual void OnMouseDown()
    {
        if (BuildingManager.Instance.isBuilding && transform.GetComponentInChildren<IEntity>() == null && BuildingManager.Instance.selectedItem != null)
        {
            Dictionary<Item, int> itemsDictionary = data.playerData.itemsInventory.ToDictionary();
            if (BuildingManager.Instance.selectedItem.name == "Tor" && itemsDictionary.ContainsKey(BuildingManager.Instance.selectedItem) && itemsDictionary[BuildingManager.Instance.selectedItem] > 0)
            {
                TryToCreateRail();
            }
        }
    }

    public void OnMouseEnter()
    {
        if (BuildingManager.Instance.isBuilding)
        {
            HighlightHex(true);
        }
    }

    public void OnMouseExit()
    {
        HighlightHex(false);
    }

    public void PlaceRail()
    {
        Transform newObject = Instantiate(railManager, transform);
        newObject.position = transform.position;
    }
    public void HighlightHex(bool highlight)
    {
        if (highlight)
        {
            hexRenderer.material = highlightMaterial;
        }
        else
        {
            hexRenderer.material = originalMaterial;
        }
    }
    public void TryToCreateRail(bool isFree = false)
    {
        RailManager lastRailManager = FindObjectsOfType<RailManager>().FirstOrDefault(r => r.FindNextRail() == null);

        if (lastRailManager != null)
        {
            Vector2Int[] neighborOffsets = NeighborOffsets.GetOffsets(offsetCoordinate.y, true);

            for (int i = 0; i < neighborOffsets.Length; i++)
            {
                Vector2Int offset = neighborOffsets[i];

                if (offsetCoordinate == lastRailManager.hexComponent.offsetCoordinate + offset)
                {
                    if (!isFree)
                    {
                        Dictionary<Item, int> itemsDictionary = data.playerData.itemsInventory.ToDictionary();
                        itemsDictionary[BuildingManager.Instance.selectedItem]--;
                        DataManager.Instance.data.playerData.itemsInventory = new SerializableDictionary<Item, int>(itemsDictionary);

                    }
                    PlaceRail();
                    int test = RailQueue.Instance.GetIndexOfRail(lastRailManager);
                    if ( test != 0)
                    {
                        lastRailManager.railComponent.ChangeRail(this);
                    }
                }
            }
        }
        else
        {
            this.PlaceRail();
        }
    }
}