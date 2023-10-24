using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPController : CamController
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Cursor.lockState = CursorLockMode.None;
        ui.Hide(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            GetOther().enabled = true;
            this.enabled = false;
        }
    }

    protected override CamController MakeOtherRef()
    {
        return GetComponent<FirstPController>();
    }

}
