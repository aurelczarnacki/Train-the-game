using UnityEngine;

public class Wood : Entity
{
    public override void EntityDestroy()
    {
        Storage.Instance.AddWood(1);
        Destroy(this.gameObject);
    }
}
