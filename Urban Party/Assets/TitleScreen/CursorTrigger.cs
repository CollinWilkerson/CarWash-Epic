using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTrigger : MonoBehaviour
{
    public Rigidbody2D rb;
    void Update()
    {
        this.gameObject.transform.position = Input.mousePosition;
        if(rb != null)
        {
            if(Input.GetMouseButtonDown(1))
            {
                rb.AddForce(new Vector2(rb.transform.position.x-this.transform.position.x, rb.transform.position.y-this.transform.position.y).normalized*2 , ForceMode2D.Impulse);
                Debug.Log("Force applied");
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided");
        if(collision.gameObject.CompareTag("Balloon"))
        {
            collision.gameObject.GetComponent<BalloonBehavior>().EnableBalloon();
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(rb.transform.position.x - this.transform.position.x, rb.transform.position.y - this.transform.position.y).normalized*2, ForceMode2D.Impulse);
        }
    }

}
