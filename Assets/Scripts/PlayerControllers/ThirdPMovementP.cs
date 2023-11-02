using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPMovementP : ThirdPController
{
    readonly float speed = 500;
    Vector3 direction;
    Rigidbody2D rb;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (rb == null)
        {
            rb = player.GetComponent<Rigidbody2D>();
        }
    }

    protected override void Update()
    {
        direction = Vector3.zero;

        if (Input.GetKey(forward))
        {
            direction += Vector3.up;
        }
        if (Input.GetKey(back))
        {
            direction -= Vector3.up;
        }
        if (Input.GetKey(right))
        {
            direction += Vector3.right;
        }
        if (Input.GetKey(left))
        {
            direction -= Vector3.right;
        }

        rb.AddForce(direction * speed * Time.deltaTime);

        base.Update();
    }
}
