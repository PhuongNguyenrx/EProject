using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent (typeof(AudioSource))]
public class ToggleAudio : MonoBehaviour
{
    bool isPlaying = false;
    AudioSource audioSource;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Toggle);
        audioSource = GetComponent<AudioSource>();
    }

    void Toggle()
    {
        if (isPlaying)
            audioSource.Pause();
        else audioSource.Play();
        isPlaying = !isPlaying;
    }
}
