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
        SetAlpha(1f);
        StartCoroutine(Fade(0f));
    }

    public void FadeAndLoad(string sceneName)
    {
        StartCoroutine(Fader(sceneName));
    }

    private IEnumerator Fader(string sceneName)
    {
        yield return StartCoroutine(Fade(1f));

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = image.color.a;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;

            Color c = image.color;
            c.a = Mathf.Lerp(startAlpha, targetAlpha,
                Mathf.Clamp01(t / duration));
            image.color = c;

            yield return null;
        }

        SetAlpha(targetAlpha);
    }
    private void SetAlpha(float alpha)
    {
        Color c = image.color;
        c.a = alpha;
        image.color = c;
    }
}