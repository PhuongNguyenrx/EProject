using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clip;

    public void PlayAudio()
    {
        if (audioSource == null || clip == null)
            return;
        audioSource.clip = clip;
        audioSource.Play();
    }
}
