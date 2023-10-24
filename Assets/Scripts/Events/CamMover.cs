using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMover : Events
{
    [SerializeField]
    Vector3[] pos, rot;

    [SerializeField]
    float[] ps, rs;

    protected override void OnEnable()
    {
        base.OnEnable();

        float[,] speeds = new float[ps.Length, 2];
        for (int i = 0; i < ps.Length; i++)
        {
            speeds[i, 0] = ps[i];
            speeds[i, 1] = rs[i];
        }

        StartCoroutine(MoveCam(pos, rot, speeds));
    }

    protected override void StepDone()
    {
        AllDone();
    }
}
