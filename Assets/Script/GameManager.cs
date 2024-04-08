using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentStageIndex { get; private set; }
    [SerializeField] GameObject[] stageUI;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Set instance to this object
            DontDestroyOnLoad(gameObject); // Don't destroy this object when changing scenes
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate instances of this object
        }
    }

    private void Start()
    {
        UpdateStageIndex(currentStageIndex);
    }
    public void UpdateStageIndex(int newStageIndex)
    {
        stageUI[currentStageIndex].SetActive(false);
        stageUI[newStageIndex].SetActive(true);
        currentStageIndex = newStageIndex;
    }
}
