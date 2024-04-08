using UnityEngine;
using UnityEngine.UI;

public class Hyperlink : MonoBehaviour
{
    Button button;
    [SerializeField] string url;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenLink);
    }

    void OpenLink()
    {
        Application.OpenURL(url);
    }
}
