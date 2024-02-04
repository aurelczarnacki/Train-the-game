using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpRail : DefaultRail
{
    public override void RotateRail()
    {
        RailManager nextRail = railManager.FindNextRail();

        Vector2Int[] neighborOffsets = NeighborOffsets.GetOffsets(nextRail.hexComponent.offsetCoordinate.y, true);
        Vector2Int offset = neighborOffsets[2];

        if (nextRail.hexComponent.offsetCoordinate == hexComponent.offsetCoordinate + offset)
        {
            transform.Rotate(0, 60, 0);
            rotation = -60;
        }
        else
        {
            transform.Rotate(0, -120, 0);
            rotation = 60;
        }
    }
}
