using UnityEngine;

public class RotateImage : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f; // Speed of rotation in degrees per second

    void Update()
    {
        // Rotate the image around its Z axis
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
