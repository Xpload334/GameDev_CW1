using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryLevel : MonoBehaviour
{
    private bool _wasPressed;

    [SerializeField] private Button retryLevelButton;
    // Start is called before the first frame update
    void Start()
    {
        if (retryLevelButton == null) retryLevelButton = GetComponent<Button>();
        retryLevelButton.onClick.AddListener(ForceRetryLevel);
    }

    void ForceRetryLevel()
    {
        
        try
        {
            if (_wasPressed) return;
            var screenFader = FindObjectOfType<ScreenFader>();
            if(screenFader.isLoading) return;
            
            _wasPressed = true;
            screenFader.PlayerDeath();

        }
        catch
        {
            // ignored
            Debug.LogError("Failed to retry");
            _wasPressed = false;
        }
    }
}
