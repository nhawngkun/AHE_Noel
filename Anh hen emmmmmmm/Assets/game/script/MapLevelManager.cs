using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MapLevelManager : MonoBehaviour
{
    private static MapLevelManager instance;
    
    
        public static MapLevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("MapLevelManager");
                instance = go.AddComponent<MapLevelManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    private Dictionary<int, MapLevel> mapLevels = new Dictionary<int, MapLevel>();
    private int currentLevelId = 0;

   

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeMaps();
        LoadProgress(); // Load saved progress
    }
    
    private void InitializeMaps()
    {
        AddMapLevel(1, "Level_1");
        AddMapLevel(2, "Level_2");
        AddMapLevel(3, "Level_3");
        UnlockLevel(1); 
    }

    public void AddMapLevel(int levelId, string sceneName)
    {
        if (!mapLevels.ContainsKey(levelId))
        {
            mapLevels.Add(levelId, new MapLevel(levelId, sceneName));
        }
    }

    public void UnlockLevel(int levelId)
    {
        if (mapLevels.ContainsKey(levelId))
        {
            mapLevels[levelId].isUnlocked = true;
            SaveProgress();
        }
    }

    public void LoadLevel(int levelId)
    {
        if (mapLevels.ContainsKey(levelId) && mapLevels[levelId].isUnlocked)
        {
            currentLevelId = levelId;
            SceneManager.LoadScene(mapLevels[levelId].sceneName);
        }
    }

    public bool IsLevelUnlocked(int levelId)
    {
        return mapLevels.ContainsKey(levelId) && mapLevels[levelId].isUnlocked;
    }

    public int GetCurrentLevel()
    {
        return currentLevelId;
    }

    public void SaveProgress()
    {
        foreach (var map in mapLevels)
        {
            PlayerPrefs.SetInt($"Level_{map.Key}_Unlocked", map.Value.isUnlocked ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    public void LoadProgress()
    {
        foreach (var map in mapLevels)
        {
            mapLevels[map.Key].isUnlocked = PlayerPrefs.GetInt($"Level_{map.Key}_Unlocked", 0) == 1;
        
        
        
        }
    }

    public void UnlockNextLevel()
    {
        UnlockLevel(currentLevelId + 1);
    }

    public void ResetAllProgress()
    {
        foreach (var map in mapLevels)
        {
            map.Value.isUnlocked = false;
        }
        UnlockLevel(1);
        SaveProgress();
    }
}
 [System.Serializable]
    public class MapLevel
    {
        public int levelId;
        public string sceneName;
        public bool isUnlocked;
        
        public MapLevel(int id, string scene)
        {
            levelId = id;
            sceneName = scene;
            isUnlocked = false;
        }
    }