using System.Collections.Generic;
using UnityEngine;

public class RailQueue : SingletonWithoutDontDestroy<RailQueue>
{
    private List<RailManager> railQueue = new List<RailManager>();
    public RailManager lastRail;
    public int EnqueueRail(RailManager railManager)
    {
        railQueue.Add(railManager);
        lastRail = railManager;
        return railQueue.Count - 1;
    }
    public RailManager GetRailAtIndex(int index)
    {
        if (index >= 0 && index < railQueue.Count)
        {
            return railQueue[index];
        }
        else
        {
            return null;
        }
    }
    public int GetIndexOfRail(RailManager railManager)
    {
        int index = railQueue.IndexOf(railManager);
        return index;
    }
}