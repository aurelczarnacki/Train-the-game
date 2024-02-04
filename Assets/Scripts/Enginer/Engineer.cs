using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Engineer : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;
    public Hex task;
    public bool back;
    public float speed = 5f;
    public float stoppingDistance = 0.1f;
    public float animationDuration = 5f;

    private List<Hex> path = new List<Hex>();
    private List<Hex> backPath = new List<Hex>();
    private int currentPathIndex = 0;

    private bool isMoving = true;

    private void Update()
    {
        if (isMoving)
        {
            if (currentPathIndex >= path.Count)
            {
                path = HexPathfinder.Instance.FindShortestPath(transform.GetComponentInParent<Hex>(), Train.Instance.carMovement.currentRail.hexComponent);
                if(path == null)
                {
                    path = backPath;
                }

                currentPathIndex = 0;

                StartCoroutine(RotateTowardsHexCoroutine(path[currentPathIndex]));
            }

            if (currentPathIndex < path.Count)
            {
                Hex targetHex = path[currentPathIndex];
                Vector3 targetPosition = targetHex.transform.position;

                float distance = Vector3.Distance(transform.position, targetPosition);

                if (distance > stoppingDistance)
                {
                    Vector3 direction = (targetPosition - transform.position).normalized;
                    transform.Translate(direction * speed * Time.deltaTime, Space.World);
                }
                else
                {
                    currentPathIndex++;

                    if (currentPathIndex >= path.Count)
                    {
                        isMoving = false;
                        transform.SetParent(targetHex.transform);

                        if (back)
                        {
                            Destroy(gameObject);
                        }
                        else if (path.Count == currentPathIndex)
                        {
                            StartCoroutine(RotateTowardsHexCoroutine(task));
                            StartCoroutine(PlayAnimationAndDestroy());
                        }
                        else
                        {
                            StartCoroutine(RotateTowardsHexCoroutine(path[currentPathIndex]));
                        }
                        
                    }
                    else
                    {
                        // Obróæ postaæ w kierunku nastêpnego hexa przed rozpoczêciem ruchu
                        StartCoroutine(RotateTowardsHexCoroutine(path[currentPathIndex]));
                    }
                }
            }
        }
    }
    public void SetNewTask(Hex newTask, List<Hex> newPath)
    {
        back = false;
        task = newTask;
        path = newPath;

        List<Hex> pathCoppy = new List<Hex>(newPath);
        pathCoppy.Reverse();
        backPath.InsertRange(0, pathCoppy);
        currentPathIndex = 0;
    }

    private IEnumerator PlayAnimationAndDestroy()
    {
        Entity entity = task.GetComponentInChildren<Entity>();
        if (entity is Stone)
        {
            animator.Play("Walk|Right");
        }
        else
        {
            animator.Play("Walk|Left");
        }
        
        
        yield return new WaitForSeconds(animationDuration);
        //animator.StopPlayback();
        animator.Play("Walk|Walk");

        entity?.EntityDestroy();
        DataManager.Instance.RemoveEntity(task.offsetCoordinate);

        back = true;
        isMoving = true;
        TaskQueue.Instance.currentEngineers--;
    }
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        animator = GetComponent<Animator>();
    }
    private IEnumerator RotateTowardsHexCoroutine(Hex targetHex)
    {
        if (targetHex != null)
        {
            Vector3 targetDirection = (targetHex.transform.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            float elapsedTime = 0f;
            float duration = 0.5f;

            while (elapsedTime < duration)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.rotation = targetRotation;
        }
    }
}
