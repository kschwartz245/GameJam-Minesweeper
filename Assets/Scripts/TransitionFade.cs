using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionFade : MonoBehaviour
{
    public Image image;
    public float duration = 1f;

    private void Start()
    {
        StartCoroutine(FadeOut());
    }

    public void FadeAndLoad(string sceneName)
    {
        StartCoroutine(Fader(sceneName));
    }

    private IEnumerator Fader(string sceneName)
    {
        yield return StartCoroutine(Fade(1f, duration));

        SceneManager.LoadScene(sceneName);

        yield return StartCoroutine(Fade(0f, duration));
    }

    private IEnumerator Fade(float targetAlpha, float fadeDuration)
    {
        float startAlpha = image.color.a;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(
                startAlpha,
                targetAlpha,
                t / fadeDuration
            );

            Color c = image.color;
            c.a = alpha;
            image.color = c;

            yield return null;
        }

        Color finalColor = image.color;
        finalColor.a = targetAlpha;
        image.color = finalColor;
    }

    private IEnumerator FadeOut()
    {
        yield return StartCoroutine(Fade(0f, duration));
    }
}