using UnityEngine;

public class TesselGame : MonoBehaviour
{
    private Vector3 touchStartPos;
    private bool isRotating = false;

    void Update()
    {
        // Check for tap input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check if touch phase began
            if (touch.phase == TouchPhase.Began)
            {
                // Raycast to detect if the touch hits the object
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.transform == transform)
                {
                    // Rotate the object 90 degrees clockwise
                    transform.Rotate(0, 0, -90);
                    isRotating = true;
                }
            }

            // Check if touch phase ended
            if (touch.phase == TouchPhase.Ended && isRotating)
            {
                isRotating = false;
            }
        }

        // Check for drag input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && !isRotating)
        {
            // Get the touch delta position
            Vector2 touchDelta = Input.GetTouch(0).deltaPosition;

            // Move the object by the touch delta
            transform.position += new Vector3(touchDelta.x, touchDelta.y, 0) * Time.deltaTime;
        }
    }
}
