using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedUIFader : UIFader
{
    public bool shouldFadeOut;
    public float waitBeforeFadeIn = 0.2f;
    public float waitBeforeFadeOut = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartDelayedFadeIn();
    }

    void StartDelayedFadeIn()
    {
        StartCoroutine(DelayedFadeIn());
    }
    void StartDelayedFadeOut()
    {
        StartCoroutine(DelayedFadeOut());
    }
    

    IEnumerator DelayedFadeIn()
    {
        uiElement.alpha = 0;
        yield return new WaitForSeconds(waitBeforeFadeIn);
        FadeIn();
        
        if(shouldFadeOut) StartDelayedFadeOut();
    }

    IEnumerator DelayedFadeOut()
    {
        yield return new WaitForSeconds(waitBeforeFadeOut);
        FadeOut();
    }
}
