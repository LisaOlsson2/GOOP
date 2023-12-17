using System.Collections;
using UnityEngine;

public class Plant : Events
{
    [SerializeField]
    Material death;

    Vector3 beforePos, beforeRot;

    [SerializeField]
    GameObject c, w;

    readonly float distance = 25, xRot = 45, firstDistance = 2, height = -3.5f;

    readonly float[,] speed = { { 10, 100 } };

    private void Awake()
    {
        Saver saver = FindObjectOfType<Saver>();

        if (saver != null)
        {
            if (saver.GetItems()[0])
            {
                GetComponent<Renderer>().material = death;
            }
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        AbleControllers(false);

        toEnable.transform.forward = transform.position - toEnable.transform.position;

        Vector3[] pos = { new Vector3(transform.position.x - toEnable.transform.forward.x * firstDistance, height, transform.position.z - toEnable.transform.forward.z * firstDistance) };
        Vector3[] rot = { new Vector3(xRot, toEnable.transform.localEulerAngles.y, 0)};

        StartCoroutine(MoveThing(toEnable.transform, pos, rot, speed));
    }

    protected override void StepDone()
    {
        base.StepDone();

        if (step == 1)
        {
            beforePos = toEnable.transform.position;
            beforeRot = toEnable.transform.localEulerAngles;
            StartCoroutine(Wait2(2));
        }

        if (step == 2)
        {
            toEnable.transform.localRotation = Quaternion.Euler(transform.up * (toEnable.transform.localEulerAngles.y + 90));
            toEnable.transform.position += transform.position - toEnable.transform.position - toEnable.transform.forward * distance + toEnable.transform.up * 3;

            c.transform.position = toEnable.transform.position + toEnable.transform.forward * distance;
            w.transform.position = toEnable.transform.position + toEnable.transform.forward * distance;
            c.transform.localRotation = Quaternion.Euler(toEnable.transform.localEulerAngles);
            w.transform.localRotation = Quaternion.Euler(toEnable.transform.localEulerAngles);

            StartCoroutine(Wait());
        }

        if (step == 4)
        {
            StartCoroutine(Wait2(2));
        }

        if (step == 5)
        {
            Vector3[] pos = { new Vector3(toEnable.transform.position.x, 0, toEnable.transform.position.z) };
            Vector3[] rot = { Vector3.up * toEnable.transform.localEulerAngles.y };

            StartCoroutine(MoveThing(toEnable.transform, pos, rot, speed));
        }

        if (step == 6)
        {
            AbleControllers(true);
            step = 0;
            enabled = false;
        }
    }

    private void Update()
    {
        if (step == 3)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                toEnable.transform.position = beforePos;
                toEnable.transform.localRotation = Quaternion.Euler(beforeRot);
                c.SetActive(false);
                StepDone();
            }
        }
    }

    IEnumerator Wait()
    {
        c.SetActive(true);
        yield return new WaitForSeconds(2);
        w.SetActive(true);
        yield return new WaitForSeconds(3);
        w.SetActive(false);
        StepDone();
    }

    IEnumerator Wait2(float time)
    {
        yield return new WaitForSeconds(time);
        StepDone();
    }
}
