using UnityEngine;
using UnityEngine.UI;

public class AudioBar : MonoBehaviour
{
    Slider audioSlider;
    private AudioSource audio;
    float progress;

    // Start is called before the first frame update
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        audioSlider = GetComponent<Slider>();
        audioSlider.maxValue = audio.clip.length;
    }

    private void Update()
    {
        audioSlider.value = audio.time;
    }
    public void OnSliderValueChanged()
    {
       audio.time = audioSlider.value;
    }
    public void PlayAudio()
    {
        if (audio.isPlaying)
            return;
        audio.Play();
    }

    public void PauseAudio()
    {
        if(!audio.isPlaying) return;
        audio.Pause();
    }

    public void StopAudio()
    {
        audio.Stop();
    }

}