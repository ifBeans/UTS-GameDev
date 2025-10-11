using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance;
    public CanvasGroup fadeCanvas;
    public float fadeDuration = 1f;
    private bool _isFading = false;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        fadeCanvas.alpha = 1; // start black
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        yield return Fade(1, 0);
    }

    public IEnumerator FadeOut()
    {
        yield return Fade(0, 1);
    }

    private IEnumerator Fade(float from, float to)
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(from, to, t / fadeDuration);
            yield return null;
        }
        fadeCanvas.alpha = to;
    }

    public void FadeToScene(string sceneName)
    {
        if (_isFading) return;

        StartCoroutine(FadeAndLoad(sceneName));
    }

    private IEnumerator FadeAndLoad(string sceneName)
    {
        _isFading = true;

        yield return FadeOut();
        SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(0.1f);
        yield return FadeIn();

        _isFading = false;
    }
}
