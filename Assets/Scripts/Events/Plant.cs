using System.Collections;
using UnityEngine;

public class Plant : Events
{
    Vector3 beforePos, beforeRot;

    [SerializeField]
    GameObject c, w;

    protected override void OnEnable()
    {
        base.OnEnable();

        beforePos = toEnable.transform.position;
        beforeRot = toEnable.transform.localEulerAngles;

        toEnable.transform.right = transform.position - toEnable.transform.position;
        toEnable.transform.localRotation = Quaternion.Euler(Vector3.zero);
        toEnable.transform.position += (transform.position - toEnable.transform.position);
        toEnable.transform.position -= toEnable.transform.forward * 25;
        toEnable.transform.position += toEnable.transform.up * 3;
        StartCoroutine(Wait());
    }

    private void Update()
    {
        if (step == 1)
        {
            foreach (KeyCode k in interactKeys)
            {
                if (Input.GetKeyDown(k))
                {
                    toEnable.transform.position = beforePos;
                    toEnable.transform.localRotation = Quaternion.Euler(beforeRot);
                    c.SetActive(false);
                    AllDone();
                }
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
}