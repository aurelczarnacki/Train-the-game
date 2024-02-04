using TMPro;

public class SaveManager : Singleton<SaveManager>
{
    public string nickName;

    public void CreateOrLoad(string text = null)
    {
        if (!string.IsNullOrEmpty(text))
        {
            nickName = text;
            GameManager.Instance.LoadGame();
        }
    }
}
