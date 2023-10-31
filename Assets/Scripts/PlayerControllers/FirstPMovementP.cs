using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPMovementP : FirstPController
{
    Rigidbody rb;

    readonly float speed = 2000;

    Vector3 direction;


    protected override void OnEnable()
    {
        base.OnEnable();

        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }


    protected override void Update()
    {
        base.Update();

        direction = Vector3.zero;

        if (Input.GetKey(forward))
        {
            direction += forward2;
        }
        if (Input.GetKey(back))
        {
            direction -= forward2;
        }
        if (Input.GetKey(right))
        {
            direction += transform.right;
        }
        if (Input.GetKey(left))
        {
            direction -= transform.right;
        }

        rb.AddForce(direction * speed * Time.deltaTime);
    }
}
