using TS.PageSlider;
using UnityEngine;
using UnityEngine.UI;

public class NavButton : MonoBehaviour
{
    Toggle toggle;
    [SerializeField] private PageScroller scroller;
    [SerializeField] private int pageNumber;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(ScrollToPage);
        GameManager.instance.OnPageChange.AddListener(UpdateToggleState);
    }
    void Update()
    {
        toggle.interactable = GameManager.instance.scrollEnabled;
    }
    void UpdateToggleState()
    {
            toggle.isOn = (pageNumber == GameManager.instance.currentStageIndex);
    }    
    void ScrollToPage(bool isOn)
    {
        if (isOn)
            scroller.ScrollToPage(pageNumber);
    }
}
