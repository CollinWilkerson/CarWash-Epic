using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float maxspeed = 10;
    [SerializeField] float acceleration = 1;
    [SerializeField] int maxMash = 10;
    [SerializeField] Transform handLoc;
    [SerializeField] float grabSpeed;

    private PlayerMovement player;
    private Rigidbody2D rb;
    private bool grabbed;
    private bool preRelease;
    private int mash;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
        if(grabbed)
        {
            player.gameObject.transform.position = V3Lerp(handLoc.position, player.gameObject.transform.position, grabSpeed);

            if(Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                mash++;
            }
            if(mash > maxMash)
            {
                Release();
            }
        }
    }

    //move toward the player's transform horizontally
    private void ChasePlayer()
    {
        if(player.transform.position.x > transform.position.x && rb.velocity.x < maxspeed)
        {
            rb.AddForce(new Vector2(acceleration, 0));
            transform.localScale = new Vector2(-1, 1);
        }
        else if(rb.velocity.x > -maxspeed)
        {
            rb.AddForce(new Vector2(-acceleration, 0));
        }
        if(player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("DEATH. DECEPTION. PROSAIC DOOM.");
            Grab();
        }
    }

    private void Grab()
    {
        Debug.Log("Grab Trigger");
        if (grabbed || preRelease)
        {
            return;
        }
        grabbed = true;
        mash = 0;
        player.Stun();
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
    }
    private void Release()
    {
        Debug.Log("Release Trigger");
        mash = 0;
        player.Release();
        rb.isKinematic = false; 
        if (player.transform.position.x > transform.position.x && rb.velocity.x < maxspeed)
        {
            rb.AddForce(new Vector2(-1000, 0));
        }
        else if (rb.velocity.x > -maxspeed)
        {
            rb.AddForce(new Vector2(1000, 0));
        }
        preRelease = true;
        StartCoroutine(GrabBuffer());
    }

    private IEnumerator GrabBuffer()
    {
        yield return new WaitForSeconds(0.1f);
        grabbed = false;
        preRelease = false;
    }

    private Vector3 V3Lerp(Vector3 currentPos, Vector3 targetPos, float t)
    {
        return new Vector3(Mathf.Lerp(currentPos.x, targetPos.x, t),
                    Mathf.Lerp(currentPos.y, targetPos.y, t),
                    Mathf.Lerp(currentPos.z, targetPos.z, t));
    }
}
