using UnityEngine;

//Image linked to panel for zoom/pan
public class ImagePanel : MonoBehaviour
{
    private RectTransform panelRectTransform; // Reference to the panel's RectTransform
    [SerializeField] private float panSpeed = 1f; // Speed of panning
    [SerializeField] private float zoomSpeed = 0.5f; // Speed of zooming
    [SerializeField] private float minZoomScale = 1; // Minimum scale allowed for zooming
    [SerializeField] private float maxZoomScale = 8; // Maximum scale allowed for zooming

    private Vector2 touchStart; // Last touch position for panning
    private bool isPanActive = false; // Flag to indicate if panning is active
    private Vector2 touchDelta;

    private Vector3 initialScale;

    private void OnEnable()
    {
        panelRectTransform = GetComponent<RectTransform>();
        panelRectTransform.anchoredPosition = Vector2.zero;
        initialScale = panelRectTransform.localScale;
    }

    private void Update()
    {
        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (Input.touchCount == 2)
        {
            initialScale = panelRectTransform.localScale;
            HandlePinchZoom();
            return;
        }
        else if (Input.touchCount == 1)
        {
            if (touch.phase == TouchPhase.Began)
            {
                touchStart = touch.position;
                isPanActive = true;
            }
            else if (touch.phase == TouchPhase.Moved && isPanActive)
            {
                touchDelta = touch.position - touchStart;
                panelRectTransform.anchoredPosition += touchDelta * panSpeed;
                touchStart = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (touchDelta.magnitude <= 2) 
                    gameObject.SetActive(false);
                isPanActive = false;
                touchDelta = Vector2.zero;
                ClampPanPosition();
            }
        }
    }

    void HandlePinchZoom()
    {
            
        Touch touch0 = Input.GetTouch(0);
        Touch touch1 = Input.GetTouch(1);

        Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
        Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

        float prevMagnitude = (touch0PrevPos - touch1PrevPos).magnitude;
        float currentMagnitude = (touch0.position - touch1.position).magnitude;

        float difference = currentMagnitude - prevMagnitude;

        float scaleFactor = Mathf.Clamp(initialScale.x - difference * zoomSpeed, minZoomScale, maxZoomScale);
        panelRectTransform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
        //if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
        //{
        //    initialPinchDistance = (touch0.position - touch1.position).magnitude;
        //    initialScale = panelRectTransform.localScale;
        //}
        //else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
        //{
        //    float currentDistance = (touch0.position - touch1.position).magnitude;
        //    float deltaDistance = currentDistance - initialPinchDistance;
        //    float scaleFactor = Mathf.Clamp(initialScale.x - deltaDistance * zoomSpeed, minZoomScale, maxZoomScale);
        //    panelRectTransform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
        //}
    }

    private void ClampPanPosition()
    {
        Vector3 pos = panelRectTransform.localPosition;

        // Get the size of the panel image
        Vector2 panelSize = panelRectTransform.rect.size;

        // Get the size of the canvas
        RectTransform canvasRectTransform = panelRectTransform.parent.GetComponent<RectTransform>();
        Vector2 canvasSize = canvasRectTransform.rect.size;

        // Calculate the minimum and maximum positions based on the panel and canvas sizes
        float minX = canvasSize.x / 2 - panelSize.x / 2;
        float maxX = panelSize.x / 2 - canvasSize.x / 2;
        float minY = panelSize.y > canvasSize.y ? (canvasSize.y / 2 - panelSize.y / 2) : 0;
        float maxY = panelSize.y > canvasSize.y ? (panelSize.y / 2 - canvasSize.y / 2) : canvasSize.y / 2 - panelSize.y / 2;

        // Clamp the position within the calculated bounds
        pos.x = Mathf.Clamp(pos.x, -maxX, -minX);
        pos.y = Mathf.Clamp(pos.y, -maxY, -minY);

        // Update the position of the panel RectTransform
        panelRectTransform.localPosition = pos;
    }
}
