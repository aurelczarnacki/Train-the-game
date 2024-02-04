using UnityEngine;

public class NoRail : MonoBehaviour
{
    private Train train;
    private GameObject childGameObject;
    void Start()
    {
        train = Train.Instance;
        
        foreach (Transform child in transform)
        {
            childGameObject = child.gameObject;
        }
        childGameObject.SetActive(false);
    }

    void Update()
    {
        bool shouldActivate = train.carMovement.nextRail == null;
        childGameObject.SetActive(shouldActivate);
    }
}
