using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PousePanel : MonoBehaviour
{
    public Transform toggle;
    public TextMeshProUGUI stoneText;
    public TextMeshProUGUI woodText;

    void Update()
    {
        woodText.text = DataManager.Instance.data.playerData.wood.ToString();
        stoneText.text = DataManager.Instance.data.playerData.stone.ToString();
    }
    public void DeveloperToggle(bool isActive)
    {
        foreach (Transform child in toggle.parent)
        {
            if (child != toggle) child.gameObject.SetActive(isActive);
        }
    }
    public void ResetTrain()
    {
        Train.Instance.ResetTrain();
    }
    public void BackMainMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }
}
