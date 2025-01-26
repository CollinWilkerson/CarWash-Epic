using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkedaddleNoise : MonoBehaviour
{
    public AudioSource skedadle;
    private PlayerMovement player;
    private Animator animator;
    private float curSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        curSpeed = animator.GetFloat("CurSpeed");
        bool isRunning = animator.GetBool("IsInput");
        bool isGrounded = animator.GetBool("IsGrounded");

        if (isRunning && isGrounded)
        {
            if (!skedadle.isPlaying)
            {
                skedadle.Play();
            }
            skedadle.pitch = Mathf.Clamp(curSpeed / 18.8f, 0, 1);
        }
        else
        {
            skedadle.Stop();
        }
        
    }
}
