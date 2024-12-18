using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectUI : MonoBehaviour
{
    [System.Serializable]
    public class LevelButton
    {
        public Button button;
        public TextMeshProUGUI levelText;
        public Image lockIcon;
    }

    public LevelButton[] levelButtons;

    void Start()
    {
        InitializeLevelButtons();
    }

    void InitializeLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelId = i + 1;
            LevelButton levelBtn = levelButtons[i];

            // Set level number text
            if (levelBtn.levelText != null)
            {
                levelBtn.levelText.text = levelId.ToString();
            }

            // Setup button click
            levelBtn.button.onClick.AddListener(() => OnLevelButtonClick(levelId));

            // Update UI based on level locked status
            UpdateLevelButtonUI(levelBtn, levelId);
        }
    }

    void UpdateLevelButtonUI(LevelButton levelBtn, int levelId)
    {
        bool isUnlocked = MapLevelManager.Instance.IsLevelUnlocked(levelId);
        
        // Update lock icon
        if (levelBtn.lockIcon != null)
        {
            levelBtn.lockIcon.gameObject.SetActive(!isUnlocked);
        }

        // Update button interactability
        levelBtn.button.interactable = isUnlocked;

        // Update button color
        Color buttonColor = isUnlocked ? Color.white : Color.gray;
        levelBtn.button.GetComponent<Image>().color = buttonColor;
    }

    void OnLevelButtonClick(int levelId)
    {
        if (MapLevelManager.Instance.IsLevelUnlocked(levelId))
        {
            MapLevelManager.Instance.LoadLevel(levelId);
        }
    }
}