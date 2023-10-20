using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1.0f;

    private void Start()
    {
        // Initialize the alpha value of the image to 1 (fully opaque).
        fadeImage.color = new Color(0, 0, 0, 1);
    }
    
    private void HandlePlayerDeath()
    {
        StartCoroutine(FadeToBlackAndReload());
    }

    private IEnumerator FadeToBlackAndReload()
    {
        float alpha = 0;

        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // Reload the current scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}