using UnityEngine;

public class RailManager : MonoBehaviour
{
    public Transform railPrefab;
    public Transform sharpRailPrefab;
    public Transform turnedRailPrefab;

    public Hex hexComponent;
    public DefaultRail railComponent;

    public int QueueId;
    
    private Transform rail;

    void Awake()
    {
        QueueId = RailQueue.Instance.EnqueueRail(this);
        hexComponent = transform.GetComponentInParent<Hex>();
        PlaceRail(railPrefab);
    }
    private void PlaceRail(Transform newRail)
    {
        rail = Instantiate(newRail, transform);

        railComponent = rail.GetComponent<DefaultRail>();
        railComponent.railManager = this;
        railComponent.hexComponent = hexComponent;
        railComponent.RotateRail();
    }
    public void ChangeToTurnedRail()
    {
        Destroy(rail.gameObject);
        PlaceRail(turnedRailPrefab);
    }
    public void ChangeToSharpRail()
    {
        Destroy(rail.gameObject);
        PlaceRail(sharpRailPrefab);
    }
    public RailManager FindPreviousRail()
    {
        RailManager previousRail = RailQueue.Instance.GetRailAtIndex(QueueId - 1);
        if (previousRail != null)
        {
            return previousRail;
        }
        return null;
    }
    public RailManager FindNextRail()
    {
        RailManager nextRail = RailQueue.Instance.GetRailAtIndex(QueueId + 1);
        if (nextRail != null)
        {
            return nextRail;
        }
        return null;
    }

}
