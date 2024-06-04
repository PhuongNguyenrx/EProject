using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToggleAudio : MonoBehaviour
{
    bool isPlaying = false;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Toggle);
    }

    void Toggle()
    {
        if (audioSource.clip != audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Pause();
            else audioSource.Play();
        }
        //isPlaying = !isPlaying;
    }
}
