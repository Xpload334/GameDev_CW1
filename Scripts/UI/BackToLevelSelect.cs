using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToLevelSelect : MonoBehaviour
{
    private bool _wasPressed;

    [SerializeField] private Button returnToLevelSelectButton;
    // Start is called before the first frame update
    void Start()
    {
        if (returnToLevelSelectButton == null) returnToLevelSelectButton = GetComponent<Button>();
        returnToLevelSelectButton.onClick.AddListener(LevelSelect);
    }

    void LevelSelect()
    {
        
        try
        {
            if (_wasPressed) return;
            if(FindObjectOfType<ScreenFader>().isLoading) return;
            
            _wasPressed = true;
            FindObjectOfType<LevelSceneManager>().StartLevel(0);

        }
        catch
        {
            // ignored
            Debug.LogError("Failed to return to level select");
            _wasPressed = false;
        }
    }
    
}
