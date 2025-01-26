using UnityEngine;
using System.Collections;

public class Stun : MonoBehaviour
{
    [SerializeField] private float stunDuration = 5f; // Duration of the stun
    private bool isStunned = false; // To check if the player is stunned
    private Rigidbody2D playerRigidbody;
    private PlayerMovement playerMovement;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isStunned)
        {
            // Get the player's Rigidbody and PlayerMovement components
            playerRigidbody = collision.GetComponent<Rigidbody2D>();
            playerMovement = collision.GetComponent<PlayerMovement>();

            if (playerRigidbody != null && playerMovement != null)
            {
                StartCoroutine(StunPlayer());
            }
        }
    }

    private IEnumerator StunPlayer()
    {
        isStunned = true;

        Debug.Log("Player is stunned");

        // Disable player's movement and stop them
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        playerMovement.enabled = false;

        // Wait for the stun duration
        yield return new WaitForSeconds(stunDuration);

        // Re-enable player's movement after the stun period
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerMovement.enabled = true;
        isStunned = false;
    }
}