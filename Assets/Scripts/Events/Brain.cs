using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : Talker
{

    readonly string[] line = { "Oh...." };

    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(ShowText(line));
        toEnable.transform.position = Vector3.right * 20;
    }

    protected override void StepDone()
    {
        base.StepDone();

        if (step == 1)
        {
            button.SetActive(true);
        }
    }

    public override void End()
    {
        base.End();
        toEnable.transform.position = Vector3.zero;
        AbleControllers(true);
        step = 0;
        enabled = false;
    }
}
