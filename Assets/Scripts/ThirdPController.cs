using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPController : CamController
{
    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePerspective("0,0,0,0,0,0,");
        }
    }

    protected override CamController GetOther()
    {
        return GetComponent<FirstPController>();
    }
}
