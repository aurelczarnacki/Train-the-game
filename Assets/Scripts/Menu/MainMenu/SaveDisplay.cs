using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveDisplay : MonoBehaviour
{
    public TextMeshProUGUI saveNameText;
    public TMP_InputField inputField;
    public string nick;

    void Start()
    {
        if(saveNameText != null) saveNameText.text = "Nazwa: " + nick;
    }
    public void onClick()
    {
        if(inputField != null) nick = inputField.text;
        SaveManager.Instance.CreateOrLoad(nick);
    }
}
