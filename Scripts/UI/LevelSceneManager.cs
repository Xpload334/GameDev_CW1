using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelSceneManager : MonoBehaviour
{
    // public float deathFadeDelay = 1f;
    // public static LevelSceneManager Instance { get; set; }
    private ScreenFader _screenFader;
    public string currentScene;
    public int currentSceneIndex = 1;
    
    //Use in UI to enable buttons for level select
    private int _levelsUnlocked;
    public int LevelsUnlocked
    {
        get => _levelsUnlocked;
        set
        {
            _levelsUnlocked = value;
            if (OnLevelUnlockedChange != null) OnLevelUnlockedChange(_levelsUnlocked);
        }
    }

    public delegate void OnLevelUnlockedDelegate(int newVal);
    public event OnLevelUnlockedDelegate OnLevelUnlockedChange;

    private void Awake()
    {
        //Ensure only 1 exists
        // var objs = FindObjectsOfType<LevelSceneManager>();
        // if (objs.Length > 1)
        // {
        //     Destroy(this.gameObject);
        // }
        // DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        _screenFader = GetComponent<ScreenFader>();
        LevelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        currentScene = SceneManager.GetActiveScene().name;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 0 || currentScene.Equals("LevelSelect"))
        {
            // try
            // {
            //     FindObjectOfType<MusicManager>().PlayLevelSelect();
            // }
            // catch
            // {
            //     // ignored
            // }

            if (LevelsUnlocked == 0)
            {
                Debug.Log("Level "+PlayerPrefs.GetInt("levelsUnlocked")+" has been unlocked");
                PlayerPrefs.SetInt("levelsUnlocked", 1);
            }
            
            // LevelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);
        }
        // else if (currentScene.Equals("EndScreen"))
        // {
        //     try
        //     {
        //         FindObjectOfType<MusicManager>().PlayEndScreen();
        //     }
        //     catch
        //     {
        //         // ignored
        //     }
        // }
        // else
        // {
        //     try
        //     {
        //         FindObjectOfType<MusicManager>().PlayLevelNormal();
        //     }
        //     catch
        //     {
        //         // ignored
        //     }
        // }
        
        FadeIn();
        // try
        // {
        //     FindObjectOfType<MusicManager>().SetMusicTrack();
        // }
        // catch
        // {
        //     Debug.Log("Failed to switch music tracks");
        // }
    }

    void FadeIn()
    {
        _screenFader.FadeFromBlack();
    }
    
    public void PlayerDeath()
    {
        _screenFader.PlayerDeath();
        // StartCoroutine(PlayerDeathDelayed());
    }

    //Load level indices from Build Settings
    private void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadNextLevel()
    {
        LoadLevel(currentSceneIndex+1);
    }

    public void ReloadCurrentLevel()
    {
        LoadLevel(currentSceneIndex);
    }

    //On level complete, unlock the next level (doesn't change scenes)
    public void PassLevel(int passedLevelIndex)
    {
        if (passedLevelIndex >= PlayerPrefs.GetInt("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", passedLevelIndex+1);
        }
        Debug.Log("Level "+PlayerPrefs.GetInt("levelsUnlocked")+" has been unlocked");
        LevelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);
        
        _screenFader.LevelComplete(); //Trigger level complete effect
    }

    //On level complete, unlock the next level
    // ReSharper disable Unity.PerformanceAnalysis
    public void PassLevel()
    {
        PassLevel(currentSceneIndex);
    }

    
    /*
     * Fade to black, then load the desired level index
     */
    public void StartLevel(int levelIndex)
    {
        _screenFader.StartFadeToBlack(() => LoadLevel(levelIndex));
    }

    // void PlayerDeathDelayed()
    // {
    //     // yield return new WaitForSeconds(deathFadeDelay);
    //     _screenFader.PlayerDeath();
    // }

    public void SetLevelsUnlocked(int newVal)
    {
        PlayerPrefs.SetInt("levelsUnlocked", newVal);
        LevelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);
        
        Debug.Log("Force set LevelsUnlocked to "+LevelsUnlocked);
    }

    public void SetPlayerDeaths(int newVal)
    {
        PlayerPrefs.SetInt("playerDeaths", newVal);
        Debug.Log("Force set PlayerDeaths to "+newVal);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        _screenFader.StartFadeToBlack(() =>
        {
            Application.Quit();
        });
    }
}
