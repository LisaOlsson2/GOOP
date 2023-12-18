using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ThirdPController : CamController
{
    public Transform player;
    readonly float followSpeed = 1;

    [SerializeField]
    float borderX1, borderX2, borderY1, borderY2;

    protected override void OnEnable()
    {
        base.OnEnable();
        Cursor.lockState = CursorLockMode.Locked;
        ui.Hide(true);
    }

    protected virtual void Update()
    {
        if (borderX1 != borderX2)
        {
            if ((transform.position.x > player.position.x && transform.position.x > borderX1) || (transform.position.x < player.position.x && transform.position.x < borderX2))
            {
                transform.position += new Vector3(player.position.x - transform.position.x, 0, 0) * followSpeed * Time.deltaTime;
            }
        }

        if (borderY1 != borderY2)
        {
            if ((transform.position.y > player.position.y && transform.position.y > borderY1) || (transform.position.y < player.position.y && transform.position.y < borderY2))
            {
                transform.position += new Vector3(0, player.position.y - transform.position.y, 0) * followSpeed * Time.deltaTime;
            }
        }


        
    }
}
