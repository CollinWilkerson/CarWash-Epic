using UnityEngine;
using System.Collections;

public class LaserMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; // Laser speed
    [SerializeField] private float directionChangeInterval = 2f; // Change direction
    [SerializeField] private float rotationChangeInterval = 1f; // Trigger rotation change
    [SerializeField] private float rotationSpeed = 100f; // Speed of rotation
    [SerializeField] private float laserOffDuration = 3f; // Time the laser stays off
    [SerializeField] private float laserOnDuration = 10f; // Time the laser stays on
    [SerializeField] private float fadeDuration = 1f; // Duration of fade in/out

    private float direction = 1f;
    private float timeUntilNextDirectionChange;
    private bool isLaserActive = true;
    private SpriteRenderer laserRenderer;
    private Collider2D laserCollider;
    private Quaternion targetRotation;

    private void Start()
    {
        timeUntilNextDirectionChange = directionChangeInterval;

        laserRenderer = GetComponent<SpriteRenderer>();
        laserCollider = GetComponent<Collider2D>();

        StartCoroutine(ToggleLaserRoutine());
        StartCoroutine(SmoothRotationRoutine());
    }

    private void Update()
    {
        if (!isLaserActive) return; // Skip if the laser is off

        // Move the laser horizontally
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        timeUntilNextDirectionChange -= Time.deltaTime;

        if (timeUntilNextDirectionChange <= 0)
        {
            ChangeDirection();
            timeUntilNextDirectionChange = directionChangeInterval;
        }
    }

    private void ChangeDirection()
    {
        // Randomly switch direction (1 for right, -1 for left)
        direction = Random.value > 0.5f ? 1f : -1f;
    }

    private IEnumerator SmoothRotationRoutine()
    {
        while (true)
        {
            // Wait for the next rotation change
            yield return new WaitForSeconds(rotationChangeInterval);

            // Set a new random target rotation within the range of -13° to 13°
            float randomZRotation = Random.Range(-13f, 13f);
            targetRotation = Quaternion.Euler(0f, 0f, randomZRotation);

            // Smoothly rotate to the target rotation
            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }

    private IEnumerator ToggleLaserRoutine()
    {
        while (true)
        {
            yield return StartCoroutine(FadeLaser(0f, 1f));
            isLaserActive = true;
            laserCollider.enabled = true;

            yield return new WaitForSeconds(laserOnDuration);

            isLaserActive = false;
            laserCollider.enabled = false;
            yield return StartCoroutine(FadeLaser(1f, 0f));

            yield return new WaitForSeconds(laserOffDuration);
        }
    }

    private IEnumerator FadeLaser(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        Color laserColor = laserRenderer.color;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            laserRenderer.color = new Color(laserColor.r, laserColor.g, laserColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        laserRenderer.color = new Color(laserColor.r, laserColor.g, laserColor.b, endAlpha);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.FreezePlayer(5f);
            }
        }
    }
}