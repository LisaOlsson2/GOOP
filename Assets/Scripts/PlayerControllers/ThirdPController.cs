using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPController : CamController
{
    Transform player;

    protected override void OnEnable()
    {
        base.OnEnable();
        Cursor.lockState = CursorLockMode.None;
        ui.Hide(true);
    }

    private void Update()
    {
        
    }
}
