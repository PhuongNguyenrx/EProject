using UnityEngine;

public class Scrolling : MonoBehaviour
{
    [SerializeField] Transform stageHolder;
    [SerializeField] float yoffset;

    private Vector2 touchStartPos;
    private bool isScrolling = false;

    [Tooltip ("The percentage of the bottom/top of screen that allows scrolling motion")]
    [SerializeField] float scrollTolerance;
    [Tooltip("The minimum percentage of screen height allowed for mintouchdelta return")]
    [SerializeField] float scrollThreshold;
    private float screenHeight;
    private float triggerHeight;
    float touchDelta;
    private float minTouchDeltaForReturn = 10f;
    Vector3 startingPos;

    [SerializeField] private float scrollSpeedMultiplier = 0.0001f; // Multiplier to control scroll speed
    
    // Set up screen heights, mintouchdelta, etc
    void Start()
    {
        screenHeight = Screen.height;
        triggerHeight = screenHeight * scrollTolerance; // % of the screen height allow scrolling
        minTouchDeltaForReturn = screenHeight * scrollThreshold;
    }
    //Handle touch input
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
                        startingPos = stageHolder.transform.position;
                    }
                    break;

                case TouchPhase.Moved:
                    // If scrolling is active
                    if (isScrolling)
                    {
                        // Calculate touch delta
                        touchDelta = touch.position.y - touchStartPos.y;
                        if(GameManager.instance.currentStageIndex < stageHolder.childCount - 1 && touchDelta > 0 || GameManager.instance.currentStageIndex > 0 && touchDelta < 0)
                        stageHolder.transform.position += new Vector3(0f, touchDelta * scrollSpeedMultiplier, 0f);
                    }
                    break;

                case TouchPhase.Ended:
                    // Reset scrolling flag
                    isScrolling = false;
                    if (Mathf.Abs(touchDelta) < minTouchDeltaForReturn)
                    {
                        stageHolder.transform.position = startingPos;
                    }
                    else
                    {
                        if (GameManager.instance.currentStageIndex < stageHolder.childCount - 1 && touchDelta > 0)
                            GameManager.instance.UpdateStageIndex (GameManager.instance.currentStageIndex + 1);
                        else if (GameManager.instance.currentStageIndex > 0 && touchDelta < 0)
                            GameManager.instance.UpdateStageIndex(GameManager.instance.currentStageIndex - 1);
                    }
                    touchDelta = 0;
                    break;
            }
        }
        UpdateScrollPosition();
    }

    void UpdateScrollPosition()
    {
        stageHolder.position = new Vector3(stageHolder.position.x, Mathf.Abs(yoffset * GameManager.instance.currentStageIndex), stageHolder.position.z);
    }
}
