using UnityEngine;
using UnityEngine.UI;

public class ImageZoom : MonoBehaviour
{
    private bool isZoomedIn = false;
    private Vector3 originalScale;
    private Vector2 originalPosition;

    void Start()
    {
        // Store the original scale and position of the image
        originalScale = transform.localScale;
        originalPosition = GetComponent<RectTransform>().anchoredPosition;

        // Add a click listener to the image
        GetComponent<Button>().onClick.AddListener(Zoom);
    }

    private void Update()
    {
        
    }
    void Zoom()
    {
        // Toggle zoom state
        isZoomedIn = !isZoomedIn;

        // Zoom in or out based on the current zoom state
        if (isZoomedIn)
        {
            // Calculate the scale factor to fill the screen vertically
            float targetHeight = Screen.height;
            float imageHeight = GetComponent<RectTransform>().rect.height;
            float scaleFactor = targetHeight / imageHeight;

            // Apply the scale factor to the image's scale
            transform.localScale = originalScale * scaleFactor;

            // Center the image on the screen
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        else
        {
            // Zoom out
            transform.localScale = originalScale;
            // Reset the image position
            GetComponent<RectTransform>().anchoredPosition = originalPosition;
        }
    }
}
