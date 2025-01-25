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

    private void Start()
    {
        rb.AddForce(Vector2.up * upForce);
        rb.AddTorque(Random.Range(rotForceMin, rotForceMax), ForceMode2D.Force);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Destructible") && !collision.gameObject.CompareTag("Semisolid"))
        {
            Shatter();
        }
    }
    public void Shatter()
    {
        GameObject shatterObject = Instantiate(shatterPrefab, transform.position, transform.rotation);
        ExplodeDebris shatterScript = shatterObject.GetComponent<ExplodeDebris>();
        if(parent != null)
        {
            shatterScript.parent = parent;
        }
        Destroy(gameObject);
    }
}
