using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public Rigidbody2D[] rb;
    public Animator anim;
    private void Awake()
    {
        foreach (Rigidbody2D r in rb)
        {
            r.freezeRotation = true;
        }
    }
    public void Activate()
    {
        foreach(Rigidbody2D r in rb)
        {
            r.freezeRotation = false;
            anim.enabled = false;
        }
    }
}
