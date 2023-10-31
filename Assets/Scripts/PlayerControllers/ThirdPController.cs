using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPController : CamController
{
    [SerializeField]
    Transform player;
    readonly float speed = 5, followSpeed = 1;

    protected override void OnEnable()
    {
        base.OnEnable();
        ui.Hide(true);
    }

    private void Update()
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


        transform.position += new Vector3(player.position.x - transform.position.x, player.position.y - transform.position.y, 0) * followSpeed * Time.deltaTime;
    }
}
