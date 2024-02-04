using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TaskQueue : SingletonWithoutDontDestroy<TaskQueue>
{
    
    public int maxEngineers = 1;
    public int currentEngineers = 0;

    public GameObject engineerPrefab;
    private Queue<Hex> taskQueue = new Queue<Hex>();

    public void AddTask(Hex hexEntity)
    {
        if (!taskQueue.Contains(hexEntity))
        {
            taskQueue.Enqueue(hexEntity);
        }
    }

    public void Update()
    {
        if (taskQueue.Count > 0 && maxEngineers > currentEngineers)
        {
            PerformNextTask();
        }
    }

    private void PerformNextTask()
    {
        Hex hexEntity = taskQueue.Dequeue();
        Engineer[] engineers = FindObjectsOfType<Engineer>();

        foreach (Engineer eng in engineers)
        {
            if (eng.back)
            {
                List<Hex> path = HexPathfinder.Instance.FindShortestPath(eng.GetComponentInParent<Hex>(), hexEntity);

                if (path != null)
                {
                    currentEngineers++;
                    eng.SetNewTask(hexEntity, path);
                    return;
                }
                else
                {
                    hexEntity.GetComponentInChildren<IEntity>().isDeleted = false;
                }
            }
        }

        List<Hex> newPath = HexPathfinder.Instance.FindShortestPath(GetComponentInParent<Hex>(), hexEntity);

        if (newPath != null)
        {
            currentEngineers++;
            Transform newEngineer = Instantiate(engineerPrefab.transform, transform.position, Quaternion.identity);
            newEngineer.SetParent(transform);

            Engineer engineer = newEngineer.GetComponent<Engineer>();
            engineer.SetNewTask(hexEntity, newPath);
        }
        else
        {
            hexEntity.GetComponentInChildren<IEntity>().isDeleted = false;
        }
    }
}