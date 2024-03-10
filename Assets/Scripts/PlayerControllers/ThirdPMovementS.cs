using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPMovementS : ThirdPController
{
    readonly float speed = 5, borderRight = 32;

    protected override void Update()
    {
        if (Input.GetKey(right) && !Input.GetKey(left) && transform.position.x < borderRight)
        {
            if (true)
            {
                player.transform.position += Vector3.right * speed * Time.deltaTime;

                if (sr.flipX)
                {
                    sr.flipX = false;
                }

                if (!animator.enabled)
                {
                    animator.enabled = true;
                }
            }
        }
        else if (Input.GetKey(left) && !Input.GetKey(right))
        {
            player.transform.position -= Vector3.right * speed * Time.deltaTime;

            if (!sr.flipX)
            {
                sr.flipX = true;
            }

            if (!animator.enabled)
            {
                animator.enabled = true;
            }
        }
        else if (animator.enabled)
        {
            animator.enabled = false;
            sr.sprite = idle;
        }

        base.Update();
    }
}
