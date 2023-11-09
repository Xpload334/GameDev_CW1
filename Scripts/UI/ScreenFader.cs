using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    public bool isLoading;
    [Header("Events")]
    public UnityEvent afterPlayerDeath; //Call when the player dies
    public UnityEvent afterLevelComplete; //Call when you want to switch to the next level
    [Header("Properties")]
    public Image fadeImage;
    public float fadeOutDelay = 0.5f;
    public float fadeInDelay = 0.5f;
    public float fadeSpeed = 1f;

    private void Start()
    {
        // Initialize the alpha value of the image to 1 (fully opaque).
        fadeImage.color = new Color(0, 0, 0, 1);
    }
    
    public void PlayerDeath()
    {
        if (!isLoading)
        {
            isLoading = true;
            StartCoroutine(FadeToBlack(afterPlayerDeath));
        }
    }

    public void LevelComplete()
    {
        if (!isLoading)
        {
            isLoading = true;
            StartCoroutine(FadeToBlack(afterLevelComplete));
        }
    }
    
    public void StartFadeToBlack(Action afterFadeEvent)
    {
        if (!isLoading)
        {
            isLoading = true;
            StartCoroutine(FadeToBlack(afterFadeEvent));
        }
    }

    public void FadeFromBlack()
    {
        if (!isLoading)
        {
            isLoading = true;
            StartCoroutine(FadeFromBlack(null));
        }
    }

    /**
     * Fade to black, then trigger the event
     */
    private IEnumerator FadeToBlack(UnityEvent eventToTrigger)
    {
        // _isLoading = true;
        fadeImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(fadeOutDelay);
        float alpha = 0;

        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // Invoke finishing event
        eventToTrigger?.Invoke();
        isLoading = false;
    }
    /**
     * Fade to black, then trigger the event
     */
    private IEnumerator FadeToBlack(Action actionToTrigger)
    {
        // _isLoading = true;
        fadeImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(fadeOutDelay);
        float alpha = 0;

        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // Invoke finishing event
        actionToTrigger?.Invoke();
        isLoading = false;
    }

    /**
     * Fade from black, then trigger the event
     */
    private IEnumerator FadeFromBlack(UnityEvent eventToTrigger)
    {
        // _isLoading = true;
        fadeImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(fadeInDelay);
        float alpha = 0;

        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, 1 - alpha);
            yield return null;
        }

        // Invoke finishing event
        eventToTrigger?.Invoke();
        isLoading = false;
        fadeImage.gameObject.SetActive(false);
    }
}