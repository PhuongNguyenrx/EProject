using UnityEngine;

public class ModelRotation : MonoBehaviour
{
    private bool isRotating = false;
    private Vector3 startDragPos;
    private Quaternion originalRotation;
    [SerializeField] private float rotSpeed=0.5f;

    private float idleTimer = 0f;
    [SerializeField] private float idleTime = 5f; 
    [SerializeField] private float smoothReturnSpeed = 1f; 

    void Start()
    {
        originalRotation = transform.rotation;
    }

    void Update()
    {
        HandleInput();
        IdleRotation();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            isRotating = true;
            startDragPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0)) // Left mouse button released
        {
            isRotating = false;
            idleTimer = 0f;
        }

        if (isRotating && Input.GetMouseButton(0)) // Left mouse button held down
        {
            idleTimer = 0f; // Reset the timer when there's user input

            Vector3 currentDragPos = Input.mousePosition;
            float deltaY = currentDragPos.y - startDragPos.y;

            // Apply rotation only around the X-axis
            transform.Rotate(Vector3.right, deltaY * rotSpeed, Space.Self);

            startDragPos = currentDragPos;
        }
    }

    void IdleRotation()
    {
        if (!isRotating)
        {
            idleTimer += Time.deltaTime;

            // Check if the idle time threshold is reached
            if (idleTimer >= idleTime)
            {
                // Smoothly interpolate back to the original rotation
                float lerpFactor = smoothReturnSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, lerpFactor);
            }
        }
    }
}
