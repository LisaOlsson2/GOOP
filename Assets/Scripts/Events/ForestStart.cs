using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestStart : StartEvent
{
    readonly Vector3[] pos = { new Vector3(0, 0, -10) }, rot = { Vector3.zero };
    readonly float[,] s = { { 10, 100 } };

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(MoveCam(toEnable.transform, pos, rot, s));
    }

    protected override void StepDone()
    {
        AllDone();
    }
}
