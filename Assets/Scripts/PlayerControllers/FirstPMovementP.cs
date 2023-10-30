using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPMovementP : FirstPController
{
    Rigidbody rb;

    readonly float speed = 1000;

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

        if (Input.GetKeyDown(forward))
        {
            rb.AddForce(forward2 * speed);
        }
        if (Input.GetKey(forward))
        {
            rb.AddForce(forward2 * speed * Time.deltaTime);
        }
        if (Input.GetKeyUp(forward))
        {
            rb.velocity = Vector3.zero;
        }
    }
}
