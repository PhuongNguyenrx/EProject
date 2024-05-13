using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIScreenController : MonoBehaviour
{
    [SerializeField] Transform mainScreen;
    [SerializeField] Transform audioTourScreen;
    [SerializeField] AudiotourToggle toggle;

    private void OnEnable()
    {
        if (audioTourScreen.gameObject.active)
            toggle.Toggle();
    }
}
