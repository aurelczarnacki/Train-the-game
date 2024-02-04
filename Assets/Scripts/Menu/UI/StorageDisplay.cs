using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StorageDisplay : MonoBehaviour
{
    public Text woodText;
    public Text stoneText;

    private void Update()
    {
        woodText.text = DataManager.Instance.data.playerData.wood.ToString();
        stoneText.text = DataManager.Instance.data.playerData.stone.ToString();
    }
}
