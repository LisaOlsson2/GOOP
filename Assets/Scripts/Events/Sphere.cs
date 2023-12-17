using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : Events
{
    readonly Vector3[] pos = { Vector3.forward * 16, Vector3.forward * 13, Vector3.forward * 16, new Vector3(0, 1.5f, 18.5f), new Vector3(0, 1, 20)}, rot = { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero};
    readonly float[,] speeds = {{4, 50},{ 3, 50}, {6, 50,}, {5, 50}, {5, 50}};

    protected override void OnEnable()
    {
        base.OnEnable();

        AbleControllers(false);
        StartCoroutine(MoveThing(toEnable.transform, pos, rot, speeds));
    }
}
