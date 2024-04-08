using System.Collections;
using UnityEngine;

public class RotationGame : MiniGame
{
    [SerializeField] float touchSensitivity = 0.1f; // Touch sensitivity for rotation
    [SerializeField] private float smoothReturnSpeed = 1f;

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
            isSolved = true;
        }
            
    }

    void RandomizeRotation()
    {
        transform.rotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)); //randomize rotation on start
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
