using UnityEngine;

public class ScrollableObject : MonoBehaviour
{
    private Vector2 touchStartPos;
    private bool isScrolling = false;
    private float screenHeight;
    private float triggerHeight;
    float touchDelta;
    Vector3 startingPos;

    [SerializeField] private float scrollSpeedMultiplier = 0.0001f; // Multiplier to control scroll speed
    [SerializeField] private float minTouchDeltaForReturn = 10f;

    void Start()
    {
        screenHeight = Screen.height;
        triggerHeight = screenHeight * 0.2f; // 20% of the screen height

    }

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Check if touch starts at the top 20% or bottom 20% of the screen
                    if (touch.position.y < triggerHeight || touch.position.y > screenHeight - triggerHeight)
                    {
                        touchStartPos = touch.position;
                        isScrolling = true;
                        startingPos = transform.position;
                    }
                    break;

                case TouchPhase.Moved:
                    // If scrolling is active
                    if (isScrolling)
                    {
                        // Calculate touch delta
                        touchDelta = touch.position.y - touchStartPos.y;
                        // Apply scrolling along the Y-axis with adjusted speed
                            transform.position += new Vector3(0f, touchDelta * scrollSpeedMultiplier, 0f);
                    }
                    break;

                case TouchPhase.Ended:
                    // Reset scrolling flag
                    isScrolling = false;
                    if (Mathf.Abs(touchDelta) < minTouchDeltaForReturn)
                    {
                        transform.position = startingPos;
                    }
                    touchDelta = 0;
                    break;
            }
        }
    }
}
