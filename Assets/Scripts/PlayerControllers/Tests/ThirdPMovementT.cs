using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPMovementT : ThirdPController
{
    readonly float speed = 5;

    protected override void Update()
    {

        if (Input.GetKey(forward))
        {
            player.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(back))
        {
            player.position += Vector3.down * speed * Time.deltaTime;
        }
        if (Input.GetKey(right))
        {
            player.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(left))
        {
            player.position += Vector3.left * speed * Time.deltaTime;
        }
        base.Update();
    }

}
