using UnityEngine;

public class Rail: DefaultRail
{
    public override void RotateRail()
    {
        RailManager previousRail = RailQueue.Instance.GetRailAtIndex(railManager.QueueId - 1);
        if (previousRail == null) return;
        Vector2Int[] neighborOffsets = NeighborOffsets.GetOffsets(hexComponent.offsetCoordinate.y, false);
        for (int i = 0; i < neighborOffsets.Length; i++)
        {
            Vector2Int offset = neighborOffsets[i];

            if (previousRail.hexComponent.offsetCoordinate == hexComponent.offsetCoordinate + offset)
            {
                switch(i)
                {
                    case 0:
                        transform.Rotate(0, -60, 0);
                        rotation = -60;
                        break;
                    case 2:
                        transform.Rotate(0, 60, 0);
                        rotation = 60;
                        break;

                }
            }
        }
    }
}