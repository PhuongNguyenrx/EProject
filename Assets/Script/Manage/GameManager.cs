using System.Collections.Generic;
using TS.PageSlider;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentStageIndex { get; private set; }
    [SerializeField] PageScroller scroller;
    [SerializeField] List<Transform> stageGames;
    
    [SerializeField] List<string> stageNames;
    [SerializeField] TextMeshProUGUI stageTitle;

    [SerializeField] AudioSource TTS;

    public bool scrollEnabled = true;
    public UnityEvent OnPageChange;

    private void Awake()
    {
        if (instance == null || instance.isActiveAndEnabled == false)
        {
            instance = this; // Set instance to this object
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate instances of this object
        }
    }

    private void Start()
    {
        scroller.OnPageChangeEnded.AddListener(UpdateStage);
    }

    public void UpdateStage(int prevStageIndex, int newStageIndex)
    {
        currentStageIndex = newStageIndex;
        stageGames[prevStageIndex].gameObject.SetActive(false);
        stageGames[newStageIndex].gameObject.SetActive(true);
        stageTitle.text = stageNames[newStageIndex];
        TTS.Pause();
        OnPageChange.Invoke();
    }

    public void ToggleScroll(bool toggle)
    {
        scrollEnabled = toggle;
    }
}
