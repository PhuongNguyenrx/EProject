using System.Collections;
using UnityEngine;

public class SpotGame : MiniGame
{
    [SerializeField] GameManager manager;
    LineAnimator lineAnimator;
    [SerializeField] float fadeInterval = 0.1f;
    [SerializeField] float fadeDelay = 1;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        lineAnimator = GetComponent<LineAnimator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        manager.ToggleScroll(false);
    }
    private void Update()
    {
        if (isSolved)
            return;
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            var touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

            // Check if the ray hits any 2D colliders
            if (hit.collider != null)
                Skip();
        }
    }
    IEnumerator FadeImage()
    {
        yield return new WaitForSeconds(fadeDelay);
        // Get the color value of the main material
        Color color = spriteRenderer.materials[0].color;

        while (color.a > 0)
        {
            // Reduce the color's alpha value
            color.a -= 0.1f;

            // Apply the modified color to the object's mesh renderer
            spriteRenderer.materials[0].color = color;

            // Wait for the frame to update
            yield return new WaitForSeconds(fadeInterval);
        }
       
        // If material completely transparent or completely opaque, end coroutine
        yield return new WaitUntil(() => isSolved ? spriteRenderer.materials[0].color.a >= 1f : spriteRenderer.materials[0].color.a <= 0);
        manager.ToggleScroll(true);
        isSolved = true;
    }
    public override void ResetGame()
    {
        manager.ToggleScroll(false);
        Color color = spriteRenderer.materials[0].color;
        color.a = 1;
        spriteRenderer.materials[0].color = color;
        lineAnimator.Disable();
        isSolved = false;
    }

    public override void Skip()
    {
        lineAnimator.DrawLine();
        StartCoroutine(FadeImage());
    }
}
