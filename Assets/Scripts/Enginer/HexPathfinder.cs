using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexPathfinder : SingletonWithoutDontDestroy<HexPathfinder>
{
    public List<Hex> FindShortestPath(Hex startHex, Hex endHex)

    {
        Dictionary<Hex, int> distances = new Dictionary<Hex, int>();
        Dictionary<Hex, Hex> previousHexes = new Dictionary<Hex, Hex>();
        PriorityQueue<Hex> priorityQueue = new PriorityQueue<Hex>();

        Hex[] hexs = FindObjectsOfType<Hex>();
        foreach (Hex hex in hexs)
        {
            distances[hex] = int.MaxValue;
            previousHexes[hex] = null;
        }

        distances[startHex] = 0;
        priorityQueue.Enqueue(startHex, 0);

        while (!priorityQueue.IsEmpty)
        {
            Hex currentHex = priorityQueue.Dequeue();

            if (currentHex == endHex)
                break;

            foreach (Hex neighborHex in GetNeighbors(currentHex))
            {
                if ((neighborHex is WaterHex || neighborHex.GetComponentInChildren<Wood>() || neighborHex.GetComponentInChildren<Stone>()) && neighborHex != endHex)
                {
                    continue;
                }
                int tentativeDistance = distances[currentHex] + 1;

                if (tentativeDistance < distances[neighborHex])
                {
                    distances[neighborHex] = tentativeDistance;
                    previousHexes[neighborHex] = currentHex;
                    int priority = distances[neighborHex] + HeuristicCostEstimate(neighborHex, endHex);
                    priorityQueue.Enqueue(neighborHex, priority);
                }
            }
        }

        List<Hex> path = new List<Hex>();
        Hex current = endHex;

        if (previousHexes[current] == null)
        {
            Debug.Log("Nie mo¿na znaleŸæ œcie¿ki. Trasa jest przerwana.");
            return null;
        }

        while (current != null)
        {
            path.Insert(0, current);
            current = previousHexes[current];
        }

        // Usuñ ostatni hex, aby œcie¿ka koñczy³a siê na s¹siednim hexie
        if (path.Count > 1)
        {
            path.RemoveAt(path.Count - 1);
        }

        return path;
    }

    public List<Hex> GetNeighbors(Hex currentHex)
    {
        List<Hex> result = new List<Hex>();
        bool isEven = currentHex.offsetCoordinate.y % 2 == 0;
        Vector2Int[] neighborsVec;
        if (isEven)
        {
            neighborsVec = new Vector2Int[]
            {
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(1, -1),
        new Vector2Int(1, 0),
        new Vector2Int(1, 1)
            };
        }
        else
        {
            neighborsVec = new Vector2Int[]
            {
                new Vector2Int(-1, -1),
                new Vector2Int(-1, 0),
                new Vector2Int(-1, 1),
                new Vector2Int(0, -1),
                new Vector2Int(-1, 0),
                new Vector2Int(0, 1)
            };
        }
        foreach (Vector2Int neighbor in neighborsVec)
        {
            int neighborX = currentHex.offsetCoordinate.x + neighbor.x;
            int neighborY = currentHex.offsetCoordinate.y + neighbor.y;

            Hex neighborHex = FindObjectsOfType<Hex>().FirstOrDefault(h => h.offsetCoordinate.x == neighborX && h.offsetCoordinate.y == neighborY);

            if (neighborHex != null)
            {
                result.Add(neighborHex);
            }
        }
        return result;
    }
    private int HeuristicCostEstimate(Hex startHex, Hex endHex)
    {
        // You can use a heuristic like Manhattan distance or Euclidean distance
        return Mathf.Abs(startHex.offsetCoordinate.x - endHex.offsetCoordinate.x) + Mathf.Abs(startHex.offsetCoordinate.y - endHex.offsetCoordinate.y);
    }
}

public class PriorityQueue<T>
{
    private SortedDictionary<int, Queue<T>> dict = new SortedDictionary<int, Queue<T>>();

    public bool IsEmpty => dict.Count == 0;

    public void Enqueue(T item, int priority)
    {
        if (!dict.ContainsKey(priority))
            dict[priority] = new Queue<T>();
        dict[priority].Enqueue(item);
    }

    public T Dequeue()
    {
        var item = dict.First();
        var value = item.Value.Dequeue();
        if (item.Value.Count == 0)
            dict.Remove(item.Key);
        return value;
    }
}