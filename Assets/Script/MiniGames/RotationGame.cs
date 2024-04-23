using System.Collections;
using UnityEngine;

public class RotationGame : MiniGame
{
    [SerializeField] float touchSensitivity = 0.1f; // Touch sensitivity for rotation
    [SerializeField] private float smoothReturnSpeed = 1f;
    [SerializeField] float customThreshold;

    private Quaternion desiredRotation;
    private bool isRotating = true; // Flag to indicate whether the model is rotating
    bool isFading = false;// Flag to indicate whether the model is fading
    


    private void Start()
    {
        desiredRotation= transform.rotation;
        RandomizeRotation();
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
            StartCoroutine(FadeModel());
        }
            
    }

    void RandomizeRotation()
    {
        float randomXRotation = Random.Range(-45f, 45f);
        transform.Rotate(randomXRotation, 0, 0);    
    }
    void HandleRotateInput()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            float rotationX = -touchDeltaPosition.y * touchSensitivity;
            //float rotationY = touchDeltaPosition.x * touchSensitivity;
            transform.Rotate(rotationX,0,0);
            //transform.Rotate(Vector3.up, rotationY, Space.World);
        }
    }
    bool CompareRotation()
    {
        //// Check if the rotation is approximately -90 degrees around the x-axis
        //return Quaternion.Angle(desiredRotation, transform.rotation)<1? true : false;
        // Calculate the angle between the current rotation and the desired rotation
        float angleDifference = Quaternion.Angle(desiredRotation, transform.rotation);

        // Define a custom threshold for approximately equal comparison
        float customThreshold = 1.0f; // Adjust as needed

        // Check if the angle difference is within a tolerance threshold
        // Also, consider rotations around the Z-axis as correct solutions
        bool isApproximatelyEqual = Mathf.Abs(angleDifference - 360f) < customThreshold;
        // Return true if the angle difference is within the tolerance threshold or approximately equal to 360 degrees
        return angleDifference < 3f || isApproximatelyEqual;
    }
    IEnumerator FadeModel()
    {
        isFading = true;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        // Get the color value of the main material
        Color color = meshRenderer.materials[0].color;

        if (!isSolved)
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
        else
            while (color.a < 1)
            {
                // Reduce the color's alpha value
                color.a += 0.1f;

                // Apply the modified color to the object's mesh renderer
                meshRenderer.materials[0].color = color;

                // Wait for the frame to update
                yield return new WaitForSeconds(0.1f);
            }
        // If material completely transparent or completely opaque, end coroutine
        yield return new WaitUntil(() => isSolved? meshRenderer.materials[0].color.a >= 1f : meshRenderer.materials[0].color.a <= 0);
        yield return isSolved = true;
    }
    void AutoRotate()
    {
        float lerpFactor = smoothReturnSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, lerpFactor);
    }

    public override void Skip()
    {
        skip = true;
    }
    public override void ResetGame()
    {
        skip = false;
        RandomizeRotation();
        StartCoroutine(FadeModel());
        isFading = false;
        isRotating = true;
        isSolved = false;
    }
}
