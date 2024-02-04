using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Item")]
public class Item : ScriptableObject
{
    public new string name;
    public string description;

    public int woodCost;
    public int stoneCost;

    public Sprite artWork;
    public Transform prefab3D;

    public ItemType itemType;
}