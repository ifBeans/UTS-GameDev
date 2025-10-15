using System.Collections;
using TMPro;
using UnityEngine;

public class WarningMessageScript : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 1f;
    [SerializeField] private float _displayDuration = 2f;
    [SerializeField] private TMP_Text _warningTextBox;

    private Coroutine currentRoutine;

    public void ShowWarning(string message)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(FadeWarning(message));
    }

    private IEnumerator FadeWarning(string message)
    {
        _warningTextBox.text = message;

        // Fade in
        float time = 0;
        Color c = _warningTextBox.color;
        while (time < _fadeDuration)
        {
            time += Time.deltaTime;
            c.a = Mathf.Lerp(0, 1, time / _fadeDuration);
            _warningTextBox.color = c;
            yield return null;
        }

        yield return new WaitForSeconds(_displayDuration);

        time = 0;
        while (time < _fadeDuration)
        {
            time += Time.deltaTime;
            c.a = Mathf.Lerp(1, 0, time / _fadeDuration);
            _warningTextBox.color = c;
            yield return null;
        }
    }
}
