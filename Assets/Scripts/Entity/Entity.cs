using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IEntity
{
    public bool isDeleted { get; set; }

    public void OnMouseDown()
    {
        if (!BuildingManager.Instance.isBuilding && !isDeleted)
        {
            TaskQueue.Instance.AddTask(GetComponentInParent<Hex>());
            isDeleted = true;
        }
    }
    public virtual void EntityDestroy()
    {
        throw new System.NotImplementedException();
    }
}
