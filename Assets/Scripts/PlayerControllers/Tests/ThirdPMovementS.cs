using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPMovementS : ThirdPController
{
    readonly float speed = 5;

    protected override void Update()
    {
        if (Input.GetKey(right))
        {
            player.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(left))
        {
            player.position -= Vector3.right * speed * Time.deltaTime;
        }

        base.Update();
    }
}
