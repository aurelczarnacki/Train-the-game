using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public List<GameObject> panelList;
    public Transform savesContainer;
    public GameObject saveDisplayPrefab;

    private GameObject currentlyActive;


    private void Start()
    {
        LoadSaves();
    }

    public void LoadMenuPanel(int panelId)
    {
        CloseActive();
        currentlyActive = panelList[panelId];
        currentlyActive.gameObject.SetActive(true);
    }

    public void CloseActive()
    {
        if (currentlyActive != null) currentlyActive.gameObject.SetActive(false);
    }

    public void LoadSaves()
    {
        string[] saveFiles = Directory.GetFiles(Application.dataPath + "/Saves/", "*.json");

        foreach (string saveFile in saveFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(saveFile);
            GameObject saveDisplay = Instantiate(saveDisplayPrefab, savesContainer);
            SaveDisplay saveDisplayScript = saveDisplay.GetComponent<SaveDisplay>();

            if (saveDisplayScript != null)
                saveDisplayScript.nick = fileName;
        }
    }
    public void LoadGame(string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            SaveManager.Instance.nickName = text;
            GameManager.Instance.LoadGame();
        }
    }
}
