using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float sidewaysForce;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float fallGravity;
    [SerializeField]
    private float airGravity;
    [SerializeField]
    private float groundGravity;
    [SerializeField]
    private Transform playerSprite;
    [SerializeField]
    private float sprintMultiplier;
    private float sprintHardCap = 18;

    [SerializeField]
    private Transform GroundCheckPos;
    [SerializeField]
    private Transform ClimbCheckPos;

    private SpriteRenderer sr;
    [SerializeField]
    private Sprite run;
    [SerializeField]
    private Sprite jump;

    private bool isFalling;
    private bool atMaxSpeed;
    private bool isGrounded;
    private bool isFrozen; // NEW
    private bool isAffectedByVacuum; // NEW
    private float currentDirection = 1f; // NEW
    //private float yVelocity;
    private Animator anim;
    public bool isInput;
    private bool wallJumped;

    public Rigidbody2D rb;

    private void Start()
    {
        Debug.Log("FindRigidBody");
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (isFrozen) return; // Skip if frozen - NEW

        if (isGrounded)
            rb.AddForce(Vector3.down * groundGravity);
        else if (isFalling)
            rb.AddForce(Vector3.down * fallGravity);
        else
            rb.AddForce(Vector3.down * airGravity);

        // Apply horizontal force when affected by the vacuum
        if (isAffectedByVacuum)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            rb.AddForce(Vector2.right * currentDirection * sidewaysForce);
        }
    }
    void Update()
    {
        if (isFrozen) return; // Skip if frozen - NEW
        UpdateAnim();
        bool notFalling = rb.velocity.y < 0;

        if (notFalling && CurrentlyGrounded())
        {
            isFalling = false;
            isGrounded = true;
            rb.velocity.Set(rb.velocity.x, 0);
        }
        else
        {
            isFalling = (rb.velocity.y >= 0) ? false : true;
            isGrounded = (CurrentlyGrounded()) ? true : false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            maxSpeed *= sprintMultiplier;
            Debug.Log(maxSpeed);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            maxSpeed /= sprintMultiplier;
            Debug.Log(maxSpeed);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Attack!");
            anim.SetBool("IsAttack", true);
            this.transform.GetChild(4).GetComponent<BoxCollider2D>().enabled = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            this.transform.GetChild(4).GetComponent<BoxCollider2D>().enabled = false;
            anim.SetBool("IsAttack", false);
        }

        if (Input.anyKeyDown)
        {
            Debug.Log("Meow");
            //Add meow sound effect here
        }

        atMaxSpeed = (Mathf.Abs(rb.velocity.x) > maxSpeed) ? true : false;
    }

    public void UpdateAnim()
    {
        anim.SetBool("IsInput", isInput);
        anim.SetFloat("CurSpeed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("IsGrounded", isGrounded);
    }

    public void SetVacuumEffect(bool state) // NEW
    {
        isAffectedByVacuum = state;
    }

    public float GetMovementDirection() //NEW
    {
        return currentDirection;
    }

    public void SetMovementDirection(float direction) //NEW
    {
        currentDirection = direction;
    }

    public void Move(Direction direction)
    {
        if (isFrozen) return; // Skip if frozen - NEW

        bool movingRight = rb.velocity.x < 0;
        bool movingLeft = rb.velocity.x > 0;

        if (atMaxSpeed)
        {
            if (movingRight)
                if (direction == Direction.right)
                {
                    //maxSpeed *= 2;
                    Debug.Log("Gotta go right!");
                    rb.AddForce(sidewaysForce * Vector2.right, 0);
                }
            if (movingLeft)
                if (direction == Direction.left)
                {
                    rb.AddForce(sidewaysForce * Vector2.left, 0);
                }
            return;
        }

        if (direction == Direction.left)
        {
            rb.AddForce(sidewaysForce * Vector2.left, 0);

            transform.localScale = new Vector3(-0.16f, 0.16f, 0.16f);
            //playerSprite.localScale = new Vector3(-1, 1, 1);
            SetMovementDirection(-1f);
        }
        else if (direction == Direction.right)
        {
            rb.AddForce(sidewaysForce * Vector2.right, 0);

            transform.localScale = new Vector3(0.16f, 0.16f, 0.16f);
            //playerSprite.localScale = new Vector3(1, 1, 1);
            SetMovementDirection(1f);
        }

        if (CanClimb(GetMovementDirection()))
        {
            rb.AddForce(sidewaysForce * Vector2.up, 0);
        }
        CapSpeed();
    }

    public void TryJump()
    {
        if (isFrozen) return; // Skip if frozen - NEW

        if (CurrentlyGrounded())
        {
            DoJump();
        }
        else if (CanClimb(GetMovementDirection()) && !wallJumped)
        {
            DoJump();

            if (GetMovementDirection() == 1f)
            {
                rb.AddForce(sidewaysForce * Vector2.left, 0);
            }
            else if (GetMovementDirection() == -1f)
            {
                rb.AddForce(sidewaysForce * Vector2.right, 0);
            }

            wallJumped = true;
        }
        else
        {
            isGrounded = false;

            if (rb.velocity.x < 0)
                isFalling = true;
        }

    }

    bool CurrentlyGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(GroundCheckPos.position.x, GroundCheckPos.position.y), Vector2.down, 0.2f);

        if (hit)
        {
                bool onPlayer = hit.transform.gameObject.CompareTag("Player");
                sr.sprite = run;
                wallJumped = false;

                return true;
        }
        else
        {
            sr.sprite = jump;
        
            return false;
        }
    }

    void DoJump()
    {
        rb.velocity.Set(rb.velocity.x, 0);
        rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
        isFalling = false;
        isGrounded = false;
        anim.Play("GooberJump", 0);
    }

    public void Stun()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

    public void Release()
    {
        rb.isKinematic = false;
    }

    public void FreezePlayer(float freezeDuration) //NEW
    {
        if (!isFrozen)
        {
            StartCoroutine(FreezeCoroutine(freezeDuration));
        }
    }

    private IEnumerator FreezeCoroutine(float freezeDuration) //NEW
    {
        isFrozen = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        yield return new WaitForSeconds(freezeDuration);

        rb.isKinematic = false;
        isFrozen = false;
    }

    public void PushBack(Vector2 direction, float force) //NEW
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    public void FlipSprite() //NEW
    {
        transform.localScale = new Vector3(
            Mathf.Abs(transform.localScale.x) * currentDirection,
            transform.localScale.y,
            transform.localScale.z
        );
    }

    bool CanClimb(float direction)
    {
        if(isFrozen || isAffectedByVacuum)
        {
            return false;
        }
        
        RaycastHit2D hit;

        if (direction == -1f)
        {
            hit = Physics2D.Raycast(new Vector2(ClimbCheckPos.position.x, ClimbCheckPos.position.y), Vector2.left, 0.2f);
        }
        else
        {
            hit = Physics2D.Raycast(new Vector2(ClimbCheckPos.position.x, ClimbCheckPos.position.y), Vector2.right, 0.2f);
        }

        if (!hit) { return false; }

        if (hit.transform.gameObject.CompareTag("Climbable"))
        {
            Debug.Log("climb this");
            return true;
        }
        else
        {
            return false;
        }
    }

    void CapSpeed()
    {
        float curSpeed = Mathf.Abs(rb.velocity.x);
        if(curSpeed > sprintHardCap)
        {
            rb.velocity = new Vector2(rb.velocity.normalized.x * sprintHardCap, rb.velocity.y);
        }
        float curSpeedY = rb.velocity.y;
        if (curSpeedY > sprintHardCap)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.normalized.y * sprintHardCap);
        }
    }
}