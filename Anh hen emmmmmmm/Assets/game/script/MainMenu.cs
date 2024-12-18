using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void ResetProgress()
    {
        MapLevelManager.Instance.ResetAllProgress();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}