using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public RailManager currentRail;
    public RailManager nextRail;

    public float speed = 1;
    public float stopDistance = 0.1f;
    public float railY = 0.1f;
    public int rotationSpeed = 50;

    public int carNumber;

    private void Start()
    {
        currentRail = transform.parent.GetComponentInChildren<RailManager>();
    }

    private void Update()
    {
        if( nextRail == null )
        {
            GetNextRail();
            return;
        }

        UpdateRotation();

        if (UpdatePosition() <= stopDistance)
        {
            currentRail = nextRail;
            GetNextRail();
        }
    }

    private void UpdateRotation()
    {
        Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, currentRail.railComponent.rotation, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    private float UpdatePosition()
    {
        Vector3 targetPosition = currentRail.transform.position;
        targetPosition.y = railY;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        return Vector3.Distance(transform.position, targetPosition);
    }
    private bool GetNextRail()
    {
        int targetQueueId = currentRail.QueueId + carNumber;

        if (targetQueueId < RailQueue.Instance.lastRail.QueueId)
        {
            nextRail = currentRail.FindNextRail();
            transform.SetParent(nextRail.transform);
            return true;
        }

        return false;
    }
    public void SetCarPosition(RailManager currentRailManager)
    {
        currentRail = currentRailManager;
        transform.SetParent(currentRail.transform.parent);
        transform.position = currentRail.transform.parent.position;
        nextRail = currentRail.FindNextRail();
    }
}