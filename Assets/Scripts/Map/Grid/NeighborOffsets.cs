using System.Collections.Generic;
using UnityEngine;

public static class NeighborOffsets
{
    public static Vector2Int[] GetOffsets(int y, bool isForward)
    {
        int minus = isForward ? minus = 1 : minus = -1;
        bool isEven = y % 2 == 0;

        return new Vector2Int[]
        {
                    new Vector2Int(isEven ? 0 : 0 + minus*1, -1),
                    new Vector2Int(minus*1, 0),
                    new Vector2Int(isEven ? 0 : 0 + minus*1, 1)
        };
    }
}