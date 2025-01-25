using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBehavior : MonoBehaviour
{

    public Rigidbody rb;

    private bool fly = false;
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fly == false)
        {
            
        }
        else
        {
            //rb.AddForce(new Vector3(0, 0.1f, 0), ForceMode.Impulse);
        }
    }

    public void EnableBalloon()
    {
        fly = true;
        Debug.Log("Activated");
    }
}
