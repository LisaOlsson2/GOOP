using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : Events
{
    private void Update()
    {
        if (step == 0 && toEnable.transform.position.z > transform.position.z - 10)
        {
            AbleControllers(false);
            StartCoroutine(MoveThing(toEnable.transform, new Vector3[] { toEnable.transform.position }, new Vector3[] { Vector3.zero }, new float[,] { { 1, 20 } }));
            step = 1;
        }
    }

    protected override void StepDone()
    {
        base.StepDone();

        if (step == 2)
        {
            GetComponent<Animator>().enabled = true;
        }
    }

    public void AnimationFinished(string animation)
    {
        if (animation == "Deer")
        {
            AbleControllers(true);
            Destroy(gameObject);
        }
    }
}
