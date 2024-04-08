using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGame : MonoBehaviour
{
    public bool isSolved = false;
    protected bool skip = false;
    public abstract void Skip();
    public abstract void ResetGame();
}
