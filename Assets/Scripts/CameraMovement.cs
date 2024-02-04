using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    private Camera cam;


    [SerializeField]
    private float zoomStep, minCamSize, maxCamSize;

    [SerializeField]
    private float minX, maxX, minY, maxY;

    [SerializeField]
    private float idleTimeThreshold = 5f;

    private Vector3 dragOrigin;
    private Vector3 initialPosition;
    private float initialDistance;
    private GameObject targetObject;
    private bool isCenteringCamera = false;

    private float idleTimer = 0f;
    private float cameraMoveSpeed = 30f;



    private void Start()
    {
        Invoke("FindTargetObject", 0.2f);
    }
    void Update()
    {
        cam = GetComponent<Camera>();

        PanCamera();
        ZoomCamera();
        ScrollCamera();
        DragCameraWithTouch();

        if (isCenteringCamera)
        {
            CenterCameraOnTarget();
        }

        if (IsCameraIdle())
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleTimeThreshold)
            {
                idleTimer = 0f;
                isCenteringCamera = true;
            }
        }
        else
        {
            idleTimer = 0f;
        }
    }



    private bool IsCameraIdle()
    {
        return !(Input.GetMouseButton(0) || Input.touchCount > 0 || Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0);
    }

    private void FindTargetObject()
    {
        GameObject[] trainObjects = GameObject.FindGameObjectsWithTag("Train");
        if (trainObjects.Length > 0)
        {
            targetObject = trainObjects[0];
            isCenteringCamera = true;
        }
    }

    private void CenterCameraOnTarget()
    {
        Vector3 targetPosition = targetObject.transform.position;
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minY, maxY);

        Vector3 cameraToTargetVector = targetPosition - cam.transform.position;
        cameraToTargetVector.z += 2.0f;

        Vector3 newCameraPosition = cam.transform.position + cameraToTargetVector.normalized * Time.deltaTime * cameraMoveSpeed;

        newCameraPosition.x = Mathf.Clamp(newCameraPosition.x, minX, maxX);
        newCameraPosition.z = Mathf.Clamp(newCameraPosition.z, minY, maxY);
        newCameraPosition.y = cam.transform.position.y;

        if (IsCameraMoving())
        {
            isCenteringCamera = false;
        }
        float distanceToTarget = Vector3.Distance(cam.transform.position, targetPosition);
        if (distanceToTarget < 0.3f)
        {
                newCameraPosition = targetPosition;
                isCenteringCamera = false;
        }

        cam.transform.position = newCameraPosition;
    }

    private bool IsCameraMoving()
    {
        return Input.GetMouseButton(0) || Input.touchCount > 0 || Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0;
    }


    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            initialPosition = cam.transform.position;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 currentMouseWorldPos = cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, cam.transform.position.z));
            Vector3 initialMouseWorldPos = cam.ScreenToWorldPoint(new Vector3(dragOrigin.x, dragOrigin.y, cam.transform.position.z));
            Vector3 difference = initialMouseWorldPos - currentMouseWorldPos;
            difference.y = 0f;

            Vector3 newPosition = initialPosition + difference;
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.z = Mathf.Clamp(newPosition.z, minY, maxY);

            cam.transform.position = Vector3.Lerp(cam.transform.position, newPosition, Time.deltaTime * 10f);
        }
    }

    private void ZoomCamera()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            if (touchOne.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
            }
            else if (touchZero.phase == TouchPhase.Moved && touchOne.phase == TouchPhase.Moved)
            {
                float currentDistance = Vector2.Distance(touchZero.position, touchOne.position);
                float deltaDistance = initialDistance - currentDistance;

                if (Mathf.Abs(deltaDistance) > zoomStep)
                {
                    if (deltaDistance < 0)
                    {
                        ZoomIn();
                    }
                    else
                    {
                        ZoomOut();
                    }

                    initialDistance = currentDistance;
                }
            }
        }
    }

    private void ScrollCamera()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0)
        {
            ZoomOut();
        }
        else if (scroll < 0)
        {
            ZoomIn();
        }
    }

    private void DragCameraWithTouch()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 deltaPosition = touch.deltaPosition;
                Vector3 difference = new Vector3(deltaPosition.x, deltaPosition.y, 0) * Time.deltaTime;

                cam.transform.Translate(-difference);
            }
        }
    }

    public void ZoomIn()
    {
        float newFOV = cam.fieldOfView - zoomStep;
        cam.fieldOfView = Mathf.Clamp(newFOV, minCamSize, maxCamSize);
    }

    public void ZoomOut()
    {
        float newFOV = cam.fieldOfView + zoomStep;
        cam.fieldOfView = Mathf.Clamp(newFOV, minCamSize, maxCamSize);
    }
}