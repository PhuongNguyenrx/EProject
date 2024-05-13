using System.Collections;
using UnityEngine;

public class SpotGame : MiniGame
{
    LineAnimator lineAnimator;
    [SerializeField] float timeInterval = 0.1f;

    private void Start()
    {
       lineAnimator = GetComponent<LineAnimator>();
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
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        // Get the color value of the main material
        Color color = spriteRenderer.materials[0].color;

        if (!isSolved)
            // While the color's alpha value is above 0
            while (color.a > 0)
            {
                // Reduce the color's alpha value
                color.a -= 0.1f;

                // Apply the modified color to the object's mesh renderer
                spriteRenderer.materials[0].color = color;

                // Wait for the frame to update
                yield return new WaitForSeconds(timeInterval);
            }
        else
            while (color.a < 1)
            {
                // Reduce the color's alpha value
                color.a += 0.1f;

                // Apply the modified color to the object's mesh renderer
                spriteRenderer.materials[0].color = color;

                // Wait for the frame to update
                yield return new WaitForSeconds(timeInterval);
            }
        // If material completely transparent or completely opaque, end coroutine
        yield return new WaitUntil(() => isSolved ? spriteRenderer.materials[0].color.a >= 1f : spriteRenderer.materials[0].color.a <= 0);
        yield return isSolved = true;
    }
    public override void ResetGame()
    {
        StartCoroutine(FadeImage());
        isSolved = false;
        lineAnimator.Disable();
    }

    public override void Skip()
    {
        lineAnimator.DrawLine();
        StartCoroutine(FadeImage());
    }
}
