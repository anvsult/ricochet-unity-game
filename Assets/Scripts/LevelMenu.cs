using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    private void OnEnable()
    {
        UpdateLevelButtons();
    }

    private void Start()
    {
        UpdateLevelButtons();
    }
    private void UpdateLevelButtons()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        }   
        for (int i = 0; i < unlockedLevel; i++){
            buttons[i].interactable = true;
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        }
    }
    public void OpenLevel(int levelId) {
        string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
    }
}
