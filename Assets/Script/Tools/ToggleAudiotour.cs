using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleAudiotour : MonoBehaviour
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

    void Toggle()
    {
        audioTour = !audioTour;
        prototypeScreen.gameObject.SetActive(!audioTour);
        prototypeScreenUI.gameObject.SetActive(!audioTour);
        audiotourScreen.gameObject.SetActive(audioTour);
        buttonDescription.text = audioTour ? "Back" : "Audiotour";
    }
}
