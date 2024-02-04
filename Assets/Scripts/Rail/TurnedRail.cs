using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnedRail : DefaultRail
{
    public override void RotateRail()
    {
        RailManager PreviousRail = railManager.FindPreviousRail();

        RailManager NextRail = railManager.FindNextRail();

        if (PreviousRail == null)
        {
            return;
        }
        Vector2Int[] neighborOffsets = NeighborOffsets.GetOffsets(hexComponent.offsetCoordinate.y, false);

        for (int i = 0; i < neighborOffsets.Length; i++)
        {
            Vector2Int offset = neighborOffsets[i];
            if (PreviousRail.hexComponent.offsetCoordinate == hexComponent.offsetCoordinate + offset)
            {
                switch (i)
                {
                    case 1:
                        if (hexComponent.offsetCoordinate.y % 2 == 0)
                        {
                            Vector2Int ooffset = new Vector2Int(1, 1);
                            if (NextRail.hexComponent.offsetCoordinate == hexComponent.offsetCoordinate + ooffset)
                            {
                                rotation = -60;
                                break;
                            }
                        }
                        Vector2Int oooffset = new Vector2Int(0, 1);
                        if (NextRail.hexComponent.offsetCoordinate == hexComponent.offsetCoordinate + oooffset)
                        {
                            rotation = -60;
                            break;
                        }
                        transform.Rotate(0, -120, 0);
                        rotation = 60;
                        break;
                    case 2:
                        transform.Rotate(0, 60, 0);
                        break;
                    case 0:
                        transform.Rotate(0, 180, 0);
                        break;

                }
            }
        }
    }
}
