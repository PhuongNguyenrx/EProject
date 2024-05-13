using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Toggle to Audiotourscreen
public class AudiotourToggle : MonoBehaviour
{
    [SerializeField] Transform prototypeScreen;
    [SerializeField] Transform prototypeScreenUI;
    [SerializeField] Transform audiotourScreen;
    [SerializeField] TextMeshProUGUI buttonDescription;
    bool audioTour = false;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Toggle);
    }

    private void OnEnable()
    {
        prototypeScreen.gameObject.SetActive(!audioTour);
    }

    public void Toggle()
    {
        audioTour = !audioTour;
        prototypeScreen.gameObject.SetActive(!audioTour);
        prototypeScreenUI.gameObject.SetActive(!audioTour);
        audiotourScreen.gameObject.SetActive(audioTour);
        buttonDescription.text = audioTour ? "Back" : "Audiotour";
    }
}
