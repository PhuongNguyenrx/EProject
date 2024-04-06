using System.Collections;
using UnityEngine;

public class RotationGame : MonoBehaviour
{
    [SerializeField] float touchSensitivity = 0.1f; // Touch sensitivity for rotation
    [SerializeField] private float smoothReturnSpeed = 1f;

    private Quaternion desiredRotation;
    private bool isRotating = true; // Flag to indicate whether the model is rotating
    bool isFading = false;// Flag to indicate whether the model is fading
    bool skip = false;// Flag to indicate if the game is skipped


    private void Start()
    {
        desiredRotation= transform.rotation; 
        transform.rotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)); //randomize rotation on start
    }
    void Update() //Handle logic
    {
        if (isRotating)
            HandleRotateInput(); //Users touch input rotation (2 axis)
        if (skip) 
            AutoRotate();
        if (!isFading && CompareRotation())
        {
            isRotating = false;
            StartCoroutine(FadeOutModel());
        }
            
    }
    void HandleRotateInput()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            float rotationX = -touchDeltaPosition.y * touchSensitivity;
            float rotationY = touchDeltaPosition.x * touchSensitivity;
            transform.Rotate(Vector3.right, rotationX, Space.World);
            transform.Rotate(Vector3.up, rotationY, Space.World);
        }
    }
    bool CompareRotation()
    {
        // Check if the rotation is approximately -90 degrees around the x-axis
        return Quaternion.Angle(desiredRotation, transform.rotation)<1? true : false;
    }
    IEnumerator FadeOutModel()
    {
        isFading = true;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        // Get the color value of the main material
        Color color = meshRenderer.materials[0].color;

        // While the color's alpha value is above 0
        while (color.a > 0)
        {
            // Reduce the color's alpha value
            color.a -= 0.1f;

            // Apply the modified color to the object's mesh renderer
            meshRenderer.materials[0].color = color;

            // Wait for the frame to update
            yield return new WaitForSeconds(0.1f);
        }

        // If the material's color's alpha value is less than or equal to 0, end the coroutine
        yield return new WaitUntil(() => meshRenderer.materials[0].color.a <= 0f);
    }
    public void Skip()
    {
        skip = true;
    }
    void AutoRotate()
    {
        float lerpFactor = smoothReturnSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, lerpFactor);
    }
}
