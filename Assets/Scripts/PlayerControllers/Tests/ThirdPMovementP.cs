using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPMovementP : ThirdPController
{
    readonly float speed = 500, maxVelocity = 5, slowDownSpeed = 1.5f;
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

        if (Input.GetKey(forward) && rb.velocity.y < maxVelocity)
        {
            direction += Vector3.up;
        }
        if (Input.GetKey(back) && rb.velocity.y > -maxVelocity)
        {
            direction -= Vector3.up;
        }
        if (Input.GetKey(right) && rb.velocity.x < maxVelocity)
        {
            direction += Vector3.right;
        }
        if (Input.GetKey(left) && rb.velocity.x > -maxVelocity)
        {
            direction -= Vector3.right;
        }

        if (direction.x == 0 && rb.velocity.x != 0)
        {
            rb.velocity -= Mathf.Abs(rb.velocity.x) / rb.velocity.x * Vector2.right * Time.deltaTime * slowDownSpeed;
        }
        if (direction.y == 0 && rb.velocity.y != 0)
        {
            rb.velocity -= Mathf.Abs(rb.velocity.y) / rb.velocity.y * Vector2.up * Time.deltaTime * slowDownSpeed;
        }

        rb.AddForce(direction * speed * Time.deltaTime);

        base.Update();
    }
}
