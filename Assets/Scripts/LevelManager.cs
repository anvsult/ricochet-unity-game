using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    private bool levelEnded = false;
    private int MainMenuScene = 0;
    public CanvasGroup levelCompleteUI;
    void Start()
    {
        levelCompleteUI.alpha = 0f;
        levelCompleteUI.interactable = false;
        levelCompleteUI.blocksRaycasts = false;

        StartCoroutine(CheckEnemies());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(MainMenuScene);
        }
    }

    IEnumerator CheckEnemies()
    {
        while (!levelEnded)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (enemies.Length == 0)
            {
                levelEnded = true;
                StartCoroutine(FadeInLevelComplete());
                UnlockNewLevel();
            }

            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator FadeInLevelComplete()
    {
        float t = 0f;
        float duration = 1f;

        while (t < duration)
        {
            t += Time.deltaTime;
            levelCompleteUI.alpha = Mathf.Lerp(0f, 1f, t / duration);
            yield return null;
        }

        levelCompleteUI.interactable = true;
        levelCompleteUI.blocksRaycasts = true;
    }
    // void UnlockNewLevel()
    // {
    //     if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex", 1))
    //     {
    //         PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
    //         PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
    //         PlayerPrefs.Save();
    //     }
    // }

    void UnlockNewLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int reachedIndex = PlayerPrefs.GetInt("ReachedIndex", 0);
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Debug.Log($"Current Index: {currentIndex}, Reached: {reachedIndex}, Unlocked: {unlockedLevel}");

        if (currentIndex >= reachedIndex)
        {
            PlayerPrefs.SetInt("ReachedIndex", currentIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel + 1);
            PlayerPrefs.Save();
            Debug.Log("New level unlocked!");
        }
    }
}