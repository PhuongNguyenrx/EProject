using System.Collections;
using UnityEngine;
using TMPro;

public class TextBlink : MonoBehaviour
{
    [SerializeField] float flashDuration = 0.5f; 
    private TextMeshProUGUI textMeshPro;
    private bool isFlashing;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        if (textMeshPro != null)
        {
            StartCoroutine(FlashText());
        }
    }
    IEnumerator FlashText()
    {
        isFlashing = true;
        while (isFlashing)
        {
            // Fade out
            yield return StartCoroutine(FadeTextToAlpha(0.0f, flashDuration));
            // Fade in
            yield return StartCoroutine(FadeTextToAlpha(1.0f, flashDuration));
        }
    }

    IEnumerator FadeTextToAlpha(float targetAlpha, float duration)
    {
        float startAlpha = textMeshPro.color.a;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            Color newColor = textMeshPro.color;
            newColor.a = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            textMeshPro.color = newColor;
            yield return null;
        }

        // Ensure the final alpha is set
        Color finalColor = textMeshPro.color;
        finalColor.a = targetAlpha;
        textMeshPro.color = finalColor;
    }

    void OnDisable()
    {
        isFlashing = false;
        StopAllCoroutines();
    }
}
