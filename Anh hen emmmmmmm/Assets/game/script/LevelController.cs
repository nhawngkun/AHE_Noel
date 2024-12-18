using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private void Start()
    {
        Debug.Log($"Level {MapLevelManager.Instance.GetCurrentLevel()} started");
    }

    public void OnLevelComplete()
    {
       
        MapLevelManager.Instance.UnlockNextLevel();

       
        SceneManager.LoadScene("LevelSelect");
    }

    public void OnRetryLevel()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuitToMenu()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}