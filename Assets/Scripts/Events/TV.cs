using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : Events
{
    readonly Vector3[] p = { new(-20.3f, -3.4f, -13.3f) }, r = { new(0, 270, 0) };
    readonly float[,] s = { { 30, 100 } };

    void Update()
    {
        if (step == 1)
        {

        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(MoveCam(p, r, s));
    }
}
