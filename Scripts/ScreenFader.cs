using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * Present at all times
 */
public class ScreenFader : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent afterPlayerDeath; //Call when the player dies
    public UnityEvent afterLevelComplete; //Call when you want to switch to the next level
    [Header("Properties")]
    public Image fadeImage;
    public float fadeSpeed = 1.0f;

    private void Start()
    {
        // Initialize the alpha value of the image to 1 (fully opaque).
        fadeImage.color = new Color(0, 0, 0, 1);
    }
    
    public void PlayerDeath()
    {
        StartCoroutine(FadeToBlack(afterPlayerDeath));
    }

    public void LevelComplete()
    {
        StartCoroutine(FadeToBlack(afterLevelComplete));
    }

    public void FadeIn()
    {
        StartCoroutine(FadeFromBlack(null));
    }

    /**
     * Fade to black, then trigger the event
     */
    private IEnumerator FadeToBlack(UnityEvent eventToTrigger)
    {
        float alpha = 0;

        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // Invoke finishing event
        eventToTrigger?.Invoke();
    }

    /**
     * Fade from black, then trigger the event
     */
    private IEnumerator FadeFromBlack(UnityEvent eventToTrigger)
    {
        float alpha = 0;

        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, 1 - alpha);
            yield return null;
        }

        // Invoke finishing event
        eventToTrigger?.Invoke();
    }
}