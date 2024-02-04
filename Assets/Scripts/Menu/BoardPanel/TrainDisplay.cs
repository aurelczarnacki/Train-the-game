using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainDisplay : MonoBehaviour
{
    public TextMeshProUGUI speed;
    public TextMeshProUGUI distanceTraveled;
    public TextMeshProUGUI enginners;
    public TextMeshProUGUI level;
    public TextMeshProUGUI woodCost;
    public TextMeshProUGUI stoneCost;

    private void Update()
    {
        ShowTrainInfo();
    }
    public void ShowTrainInfo()
    {
        Train train = Train.Instance;
        distanceTraveled.text = (train.carMovement.currentRail.hexComponent.offsetCoordinate.x + 1).ToString() + " hex";
        if (train.carMovement.nextRail == null)
        {
            speed.text = "0 hex/h";
        }
        else
        {
            speed.text = "10 hex/h";
        }
        string enginnersNumber = TaskQueue.Instance.currentEngineers.ToString();
        string maxenginners = TaskQueue.Instance.maxEngineers.ToString();
        enginners.text = enginnersNumber + " | " + maxenginners;
        level.text = "Poci¹g " + BoardManager.Instance.currentLevel.ToString() + " lvl";
        woodCost.text = (BoardManager.Instance.woodCost * BoardManager.Instance.currentLevel).ToString();
        stoneCost.text = (BoardManager.Instance.stoneCost * BoardManager.Instance.currentLevel).ToString();
}
}