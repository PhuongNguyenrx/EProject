using TS.PageSlider;
using UnityEngine;
using UnityEngine.UI;

public class NavButton : MonoBehaviour
{
    Toggle toggle;
    [SerializeField] private PageScroller scroller;
    [SerializeField] private int pageNumber;

    [SerializeField] GameManager gameManager;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(ScrollToPage);
       gameManager.OnPageChange.AddListener(UpdateToggleState);
    }
    void Update()
    {
        toggle.interactable = gameManager.scrollEnabled;
    }
    void UpdateToggleState()
    {
            toggle.isOn = (pageNumber == gameManager.currentStageIndex);
    }    
    void ScrollToPage(bool isOn)
    {
        if (isOn)
            scroller.ScrollToPage(pageNumber);
    }
}
