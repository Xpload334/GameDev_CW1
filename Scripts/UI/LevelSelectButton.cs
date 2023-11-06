using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    private LevelSceneManager _levelSceneManager;
    private Button _button;
    [SerializeField] private TMP_Text tmpText;
    public int levelNum = 1;
    // Start is called before the first frame update
    void Start()
    {
        _levelSceneManager = FindObjectOfType<LevelSceneManager>();
        _button = GetComponent<Button>();
        _levelSceneManager.OnLevelUnlockedChange += HandleLevelsUnlockedChange;
        int levelsUnlocked = _levelSceneManager.LevelsUnlocked;
        
        //Set text
        tmpText.text = levelNum.ToString();
        _button.interactable = levelsUnlocked >= levelNum; //Enable interaction if levels beaten
    }
    
    public void StartLevel()
    {
        if(_levelSceneManager != null) _levelSceneManager.StartLevel(levelNum);
    }

    private void HandleLevelsUnlockedChange(int newVal)
    {
        int levelsUnlocked = _levelSceneManager.LevelsUnlocked;
        _button.interactable = newVal >= levelNum; //Enable interaction if levels beaten
        _button.interactable = levelsUnlocked >= levelNum; //Enable interaction if levels beaten
    }
}
