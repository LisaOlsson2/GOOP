using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : Events
{
    protected override void Update()
    {
        if (step == 0 && toEnable.transform.position.z > transform.position.z - 10)
        {

            AbleControllers(false);

            Vector3 v = transform.position - toEnable.transform.position;

            step = 1;

            StartCoroutine(RotateThing(toEnable.transform, Vector3.up * (Mathf.Atan(v.x/v.z) * Mathf.Rad2Deg), 20));
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
            StartCoroutine(Wait(2));
        }
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        AbleControllers(true);
        Destroy(gameObject);

    }
}
