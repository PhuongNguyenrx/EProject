using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrowseScrollClamp : MonoBehaviour
{
    private ScrollRect scrollRect;
    private RectTransform contentRect;
    private RectTransform parentRect;
    private RectTransform[] itemRects; // Array to store the RectTransforms of child items
    private float totalItemSize; // Total height (or width) of all child items

    private void Start()
    {
        // Get references to ScrollRect, content RectTransform, and parent RectTransform
        scrollRect = GetComponent<ScrollRect>();
        contentRect = scrollRect.content;
        parentRect = transform.parent.GetComponent<RectTransform>();

        // Get the RectTransforms of child items
        itemRects = new RectTransform[contentRect.childCount];
        for (int i = 0; i < contentRect.childCount; i++)
        {
            itemRects[i] = contentRect.GetChild(i).GetComponent<RectTransform>();
            totalItemSize += itemRects[i].sizeDelta.y; // Assuming vertical scrolling, sum up item heights
        }
    }

    private void LateUpdate() // LateUpdate to ensure it runs after the ScrollRect has updated
    {
        // Calculate the maximum allowed position of the content based on the total item size and the parent size
        float maxY = totalItemSize - parentRect.sizeDelta.y;

        // Clamp the content's position to stay within the boundaries
        Vector2 clampedPosition = contentRect.anchoredPosition;
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, 0f, maxY);
        contentRect.anchoredPosition = clampedPosition;
    }
}