using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPController : CamController
{
    [SerializeField]
    Transform player;
    readonly float speed = 5;

    protected override void OnEnable()
    {
        base.OnEnable();
        ui.Hide(true);
    }

    private void Update()
    {
        if (Input.GetKey(forward))
        {
            player.transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(back))
        {
            player.transform.position += Vector3.down * speed * Time.deltaTime;
        }
        if (Input.GetKey(right))
        {
            player.transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(left))
        {
            player.transform.position += Vector3.left * speed * Time.deltaTime;
        }

    }
}
