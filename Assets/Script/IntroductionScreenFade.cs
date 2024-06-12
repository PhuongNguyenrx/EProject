using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroductionScreenFade : MonoBehaviour
{
    [SerializeField]Image screen; // Reference to the parent Image component
    [SerializeField] float fadeDuration = 1f; // Duration of the fade animation
    [SerializeField] TextMeshProUGUI childText; // Reference to the child Text component

    private float timer = 0f; // Timer for the fade animation

    void Update()
    {
        // Increase the timer
        timer += Time.deltaTime;

        // Calculate the alpha value based on the timer and fade duration
        float alpha = Mathf.Clamp01(1f - (timer / fadeDuration));

        // Apply the alpha value to the parent RawImage component
        Color imageColor = screen.color;
        imageColor.a = alpha;
        screen.color = imageColor;

        // Apply the alpha value to the child Text component
        Color textColor = childText.color;
        textColor.a = alpha;
        childText.color = textColor;

        // Disable the script once the fade is complete
        if (timer >= fadeDuration)
        {
            gameObject.SetActive(false);
        }
    }
}
