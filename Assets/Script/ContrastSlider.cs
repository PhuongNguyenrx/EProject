using UnityEngine;
using UnityEngine.UI;

public class ContrastSlider : MonoBehaviour
{
    Slider intensitySlider;
    public Light directionalLight;

    // Set the minimum and maximum values for the slider
    [SerializeField] private float minIntensity = 1.5f;
    [SerializeField] private float maxIntensity = 5f;

    void Start()
    {
        intensitySlider = GetComponent<Slider>();
        if (intensitySlider == null || directionalLight == null)
        {
            Debug.LogError("Assign the slider and directional light in the inspector!");
            return;
        }

        // Add a listener to the slider to update the light intensity
        intensitySlider.onValueChanged.AddListener(UpdateLightIntensity);
    }

    void UpdateLightIntensity(float intensity)
    {
        directionalLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, intensitySlider.value);
    }
}

