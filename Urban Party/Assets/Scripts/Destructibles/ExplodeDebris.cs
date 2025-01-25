using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeDebris : MonoBehaviour
{
    public Rigidbody2D[] debris;
    public AudioSource audioSource;
    public float force = 500f;
    public float rotForceMax = 500f;
    public float rotForceMin = 20f;
    public float upForce = 100f;
    public Destructible parent;

    private void Start()
    {
        if(parent != null)
        {
            parent.DoChaos();
        }
        foreach (var particulate in debris)
        {
            float x = particulate.position.x - transform.position.x;
            float y = particulate.position.y - transform.position.y;

            Vector2 explosionPosition = new Vector2(x, y);

            particulate.AddForce((explosionPosition * force) + (Vector2.up * upForce), ForceMode2D.Force);
            particulate.AddTorque(Random.Range(rotForceMin, rotForceMax) * explosionPosition.normalized.x, ForceMode2D.Force);
        }
    }
}
