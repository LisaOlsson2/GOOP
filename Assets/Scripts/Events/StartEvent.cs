using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartEvent : Events
{
    [SerializeField]
    Image image;

    readonly float speed = 0.3f;

    protected override void OnEnable()
    {
        base.OnEnable();

        AbleControllers(false);
        StartCoroutine(Disappear());
        StartCoroutine(MoveThing(toEnable.transform, new Vector3[] { Vector3.forward * toEnable.transform.position.z}, new Vector3[] { Vector3.zero }, new float[,] { { 0.5f, 20 } }));
    }

    protected override void StepDone()
    {
        AbleControllers(true);
        image.gameObject.SetActive(false);
        Destroy(this);
    }

    IEnumerator Disappear()
    {
        while(image.color.a > 0)
        {
            image.color -= Color.black * Time.deltaTime * speed;
            yield return null;
        }

    }
}
