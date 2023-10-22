using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Present at all times
 */
public class LevelSceneManager : MonoBehaviour
{
    // public float deathFadeDelay = 1f;
    public static LevelSceneManager Instance { get; set; }
    private ScreenFader _screenFader;
    public string currentScene;
    public int currentSceneIndex = 1;

    public int levelsUnlocked; //Use in UI to enable buttons for level select

    private void Awake()
    {
        //Ensure only 1 exists
        var objs = FindObjectsOfType<LevelSceneManager>();
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        _screenFader = FindObjectOfType<ScreenFader>();
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        currentScene = SceneManager.GetActiveScene().name;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        
        FadeIn();
    }

    void FadeIn()
    {
        _screenFader.FadeIn();
    }
    
    public void PlayerDeath()
    {
        _screenFader.PlayerDeath();
        // StartCoroutine(PlayerDeathDelayed());
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //Load level indices from Build Settings
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void ReloadCurrentLevel()
    {
        LoadLevel(currentSceneIndex);
    }

    //On level complete, unlock the next level
    public void PassLevel(int passedLevelIndex)
    {
        if (passedLevelIndex >= PlayerPrefs.GetInt("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", passedLevelIndex+1);
        }
        Debug.Log("Level "+PlayerPrefs.GetInt("levelsUnlocked")+" has been unlocked");
        
        _screenFader.LevelComplete(); //Trigger level complete effect
    }

    public void PassLevel()
    {
        PassLevel(currentSceneIndex);
    }

    // void PlayerDeathDelayed()
    // {
    //     // yield return new WaitForSeconds(deathFadeDelay);
    //     _screenFader.PlayerDeath();
    // }
}
