using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDebris : MonoBehaviour
{
    public GameObject shatterPrefab;
    [SerializeField]
    Rigidbody2D rb;
    public float upForce = 50f;
    public float rotForceMax = 200f;
    public float rotForceMin = -200f;
    public Destructible parent;
    [Header("audio")]
    [SerializeField] AudioClip breakNoise;

    private AudioSource audioSource;
    private bool isBroken = false;
    private void Start()
    {
        rb.AddForce(Vector2.up * upForce);
        rb.AddTorque(Random.Range(rotForceMin, rotForceMax), ForceMode2D.Force);

        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Destructible") && !collision.gameObject.CompareTag("Semisolid") && !isBroken)
        {
            Shatter();
        }
    }
    public void Shatter()
    {
        //audio
        audioSource.PlayOneShot(breakNoise);
        GameObject shatterObject = Instantiate(shatterPrefab, transform.position, transform.rotation);
        shatterObject.transform.localScale = transform.localScale;
        ExplodeDebris shatterScript = shatterObject.GetComponent<ExplodeDebris>();
        if(parent != null)
        {
            shatterScript.parent = parent;
        }
        //give the game time to play the sound
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.clear;
        isBroken = true;
        Destroy(gameObject, 1.65f);
    }
}
