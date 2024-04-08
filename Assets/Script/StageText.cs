using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageText : MonoBehaviour
{
    [SerializeField] MiniGame stageMiniGame;
    [SerializeField] GameObject textHolder;
    [SerializeField] GameObject gameUIHolder;
    private void Update()
    {
        if (stageMiniGame == null)
            return;
        textHolder.SetActive(stageMiniGame.isSolved);
        gameUIHolder.SetActive(!stageMiniGame.isSolved);
    }
}
