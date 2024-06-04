using TS.PageSlider;
using UnityEngine;
using UnityEngine.UI;

public class NavButtons : MonoBehaviour
{
    Button button;
    [SerializeField] private PageScroller scroller;
    [SerializeField] private int pageNumber;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ScrollToPage);
    }
    void Update()
    {
        button.interactable = GameManager.instance.scrollEnabled;
    }
    
    void ScrollToPage()
    {
        scroller.ScrollToPage(pageNumber);
    }
}
