using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stone : Entity
{
    public override void EntityDestroy()
    {
        Storage.Instance.AddStone(1);
        Destroy(this.gameObject);
    }
}
