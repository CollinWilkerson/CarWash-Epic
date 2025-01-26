using System.Collections;
using UnityEngine;

public class Vacuum : MonoBehaviour
{
    [SerializeField] private float reverseDuration = 2f;
    [SerializeField] private float pushBackForce = 10f;
    [SerializeField] private AudioSource vacuumSound;
    private bool isVacuumActive = false;

    private void Start()
    {
        StartCoroutine(ControlVacuumSound());
    }

    private IEnumerator ControlVacuumSound()
    {
        while (true)
        {
            // Turn on the vacuum and play the sound for 10s
            isVacuumActive = true;
            vacuumSound.Play();

            yield return new WaitForSeconds(10f);

            // Turn off the vacuum and stop the sound for 20s
            isVacuumActive = false;
            vacuumSound.Stop();

            yield return new WaitForSeconds(20f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isVacuumActive && collision.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                Vector2 pushBackDirection = (collision.transform.position - transform.position).normalized;
                playerMovement.PushBack(pushBackDirection, pushBackForce);
                StartCoroutine(ReversePlayerMovement(playerMovement));
            }
        }
    }

    private IEnumerator ReversePlayerMovement(PlayerMovement playerMovement)
    {
        float originalDirection = playerMovement.GetMovementDirection();

        playerMovement.SetMovementDirection(-originalDirection);

        // Flip the sprite
        playerMovement.FlipSprite();

        yield return new WaitForSeconds(reverseDuration);

        playerMovement.SetMovementDirection(originalDirection);

        // Flip the sprite back
        playerMovement.FlipSprite();
    }
}