using System.Runtime;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent (typeof(Image))]
public class ImageZoom : MonoBehaviour
{
    Sprite image;
    [SerializeField] GameObject targetPanel;
    private bool isZoomedIn = false;

    void Start()
    { 
        image = GetComponent<Image>().sprite;

        // Add a click listener to the image
        GetComponent<Button>().onClick.AddListener(Zoom);
        
    }
    public void Zoom()
    {
        // Toggle zoom state
        targetPanel.GetComponent<Button>().onClick.AddListener(Zoom);
        isZoomedIn = !isZoomedIn;
        targetPanel.GetComponent<Image>().sprite = image;
        targetPanel.SetActive(isZoomedIn);
    }
}
