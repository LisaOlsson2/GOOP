using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ThirdPController : CamController
{
    [SerializeField]
    protected Transform player;
    readonly float followSpeed = 1;

    protected override void OnEnable()
    {
        base.OnEnable();
        ui.Hide(true);
    }

    protected virtual void Update()
    {

        transform.position += new Vector3(player.position.x - transform.position.x, player.position.y - transform.position.y, 0) * followSpeed * Time.deltaTime;
    }
}
