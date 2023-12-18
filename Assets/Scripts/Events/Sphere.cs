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

        for (int i = 0; i < toEnable.transform.childCount; i++)
        {
            Destroy(toEnable.transform.GetChild(i).gameObject);
        }

        AbleControllers(false);
        StartCoroutine(MoveThing(toEnable.transform, pos, rot, speeds));
    }

    private void Update()
    {
        if (step == pos.Length)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                transform.position = Vector3.up * -50;
                transform.GetChild(0).gameObject.SetActive(true);
                toEnable.transform.position = transform.position + Vector3.forward * -20;
                toEnable.transform.rotation = Quaternion.Euler(Vector3.zero);
                StepDone();
            }
        }
        if (step == pos.Length +1 && Input.GetKeyUp(KeyCode.Mouse0))
        {
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);
            StepDone();
        }
        if (step == pos.Length +2)
        {
            if (transform.GetChild(1).transform.position.y < transform.position.y -15)
            {
                StepDone();
            }
        }
    }

    protected override void StepDone()
    {
        base.StepDone();
        if (step == pos.Length +3)
        {
            StartCoroutine(MoveThing(toEnable.transform, new Vector3[] { toEnable.transform.position + Vector3.up * -20 }, new Vector3[] { toEnable.transform.eulerAngles }, new float[,] { { 5, 50 } }));
        }
        if (step == pos.Length +4)
        {
            ((ThirdPController)toEnable).player = transform.GetChild(1);

            transform.GetChild(2).SetParent(null);
            transform.GetChild(1).SetParent(null);

            AbleControllers(true);
            Destroy(gameObject);
        }
    }

}
