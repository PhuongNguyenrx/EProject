using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesselGame: MiniGame
{
    [SerializeField] GameManager manager;
    [SerializeField] float fadeInterval = 0.1f;
    [SerializeField] float fadeDelay=1;

    [SerializeField] GameObject spritePrefab;

    private bool isDragging = false;
    private bool isTap = false;
    private Vector3 offset;
    private Camera mainCamera;
    private float tapDuration = 0.2f; // Max duration to consider as a tap
    private float dragThreshold = 10f; // Min movement to consider as a drag
    private float touchStartTime;
    private Vector2 touchStartPos;

    int markingIndex=0;
    BoxCollider2D objectCollider;
    [SerializeField] BoxCollider2D[] markings;

    Vector3 intialPos;
    SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer image;
    
    List<GameObject> instantiatedSprites = new List<GameObject>();


    void Start()
    {
        mainCamera = Camera.main;
        objectCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        intialPos = gameObject.transform.position;
        manager.ToggleScroll(false);
    }

    void Update()
    {
        //HandleTouchInput
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartTime = Time.time;
                    touchStartPos = touch.position;
                    isDragging = false;
                    isTap = true;
                    break;

                case TouchPhase.Moved:
                    if (isTap && (Time.time - touchStartTime > tapDuration || Vector2.Distance(touchStartPos, touch.position) > dragThreshold))
                    {
                        isTap = false;
                        isDragging = true;
                        offset = transform.position - touchPosition;
                    }

                    if (isDragging)
                    {
                        Vector3 newPosition = touchPosition + offset;
                        transform.position = ClampPositionToScreenBounds(newPosition);
                    }
                    break;

                case TouchPhase.Ended:
                    if (isTap)
                    {
                        Collider2D collider = Physics2D.OverlapPoint(touchPosition);
                        if (collider != null && collider.gameObject == gameObject)
                        {
                            transform.Rotate(0, 0, -90); // Rotate 90 degrees clockwise
                        }
                    }
                    isDragging = false;
                    break;
            }
        }

        if (IsObjectFullyInsideMarking())
        {
            //Debug.Log("Object is fully inside marking!");
            // Hide current marking and show the next one
            var newSprite = Instantiate(spritePrefab, markings[markingIndex].transform.position, markings[markingIndex].transform.rotation,gameObject.transform.parent.transform);
            instantiatedSprites.Add(newSprite);
            markings[0].gameObject.SetActive(false);
            ShowNextMarking();
        }
    }
    private Vector3 ClampPositionToScreenBounds(Vector3 position)
    {
        Vector3 minScreenBounds = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 maxScreenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.nearClipPlane));

        Collider2D objectCollider = GetComponent<Collider2D>();
        float objectWidth = objectCollider.bounds.extents.x;
        float objectHeight = objectCollider.bounds.extents.y;

        float clampedX = Mathf.Clamp(position.x, minScreenBounds.x + objectWidth, maxScreenBounds.x - objectWidth);
        float clampedY = Mathf.Clamp(position.y, minScreenBounds.y + objectHeight, maxScreenBounds.y - objectHeight);

        return new Vector3(clampedX, clampedY, position.z);
    }

    private bool IsObjectFullyInsideMarking()
    {
        Bounds objectBounds = objectCollider.bounds;
        Bounds markingBounds = markings[markingIndex].bounds;
        return markingBounds.Contains(objectBounds.min) && markingBounds.Contains(objectBounds.max) && markings[markingIndex].gameObject.transform.rotation.eulerAngles.z == gameObject.transform.rotation.eulerAngles.z;
    }

    private void ShowNextMarking()
    {
        if (markingIndex >= markings.Length - 1)
        {
            Skip();
            return;
        }
        markings[markingIndex].gameObject.SetActive(false);
        markingIndex++;
        markings[markingIndex].gameObject.SetActive(true);
    }

    IEnumerator FadeImage()
    {
        yield return new WaitForSeconds(fadeDelay);
        // Get the color value of the main material
        Color color = image.materials[0].color;

        while (color.a > 0)
        {
            // Reduce the color's alpha value
            color.a -= 0.1f;

            // Apply the modified color to the object's mesh renderer
            image.materials[0].color = color;

            // Wait for the frame to update
            yield return new WaitForSeconds(fadeInterval);
        }

        // If material completely transparent or completely opaque, end coroutine
        yield return new WaitUntil(() => isSolved ? image.materials[0].color.a >= 1f : image.materials[0].color.a <= 0);
        manager.ToggleScroll(true);
        isSolved = true;
    }

    public override void ResetGame()
    {
        manager.ToggleScroll(false);
        Color color = image.materials[0].color;
        color.a = 1;
        image.materials[0].color = color;
        image.gameObject.SetActive(false);

        spriteRenderer.enabled = true;
        gameObject.transform.position = intialPos;

        markingIndex = 0;
        markings[markingIndex].gameObject.SetActive(true);
        isSolved = false;
    }

    public override void Skip()
    {
        foreach (var marking in markings)
            marking.gameObject.SetActive(false);
        image.gameObject.SetActive(true);
        spriteRenderer.enabled = false;

        foreach (var sprite in instantiatedSprites)
            Destroy(sprite);
        instantiatedSprites.Clear();

        StartCoroutine(FadeImage());
    }

}
