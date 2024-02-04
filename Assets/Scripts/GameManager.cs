using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Screen.orientation = ScreenOrientation.Portrait;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
