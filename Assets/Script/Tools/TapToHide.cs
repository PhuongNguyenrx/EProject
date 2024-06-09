using UnityEngine;
using UnityEngine.EventSystems;

public class TapToHide : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] float fadeDuration = 1f;

    public void OnPointerClick(PointerEventData eventData)
    {
        this.gameObject.SetActive(false);
    }
}