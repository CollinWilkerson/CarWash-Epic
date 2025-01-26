using UnityEngine;
using System.Collections;

public class LaserInteraction : MonoBehaviour
{
    [SerializeField] private float freezeDuration = 5f;
    private bool isFrozen = false;
    private Rigidbody2D playerRigidbody;
    private PlayerMovement playerMovement;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isFrozen)
        {
            // Freeze the player
            playerRigidbody = collision.GetComponent<Rigidbody2D>();
            playerMovement = collision.GetComponent<PlayerMovement>();

            if (playerRigidbody != null && playerMovement != null)
            {
                StartCoroutine(FreezePlayer());
            }
        }
    }

    private IEnumerator FreezePlayer()
    {
        isFrozen = true;

        Debug.Log("Player is frozen");

        // Disable movement
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        playerMovement.enabled = false;

        // Wait for the freeze duration
        yield return new WaitForSeconds(freezeDuration);

        // Re-enable movement
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerMovement.enabled = true;
        isFrozen = false;
    }
}