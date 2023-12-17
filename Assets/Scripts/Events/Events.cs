using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Events : MonoBehaviour
{
    static UIController ui;

    [SerializeField]
    protected CamController toEnable;

    bool useEnabled;

    protected int step;

    private void Awake()
    {
        useEnabled = toEnable == null;
    }

    protected virtual void OnEnable()
    {
        if (useEnabled)
        {
            toEnable = GetUI().enabledController;
        }
    }

    protected void AbleControllers(bool able)
    {
        GetUI().gameObject.SetActive(able);
        toEnable.enabled = able;
    }

    protected virtual void StepDone()
    {
        step++;
    }

    protected IEnumerator MoveThing(Transform thing, Vector3[] pos, Vector3[] rot, float[,] speeds)
    {
        for (int i = 0; i < pos.Length; i++)
        {
            Vector3 v = pos[i] - thing.position;
            Vector3 r = rot[i] - thing.eulerAngles;
            float[] f2 = { r.x, r.y, r.z };
            r = CheckChangeRotation(f2);
            while (v.magnitude > 0.1 || r.magnitude > 2)
            {
                if (r.magnitude > 2)
                {
                    thing.rotation = Quaternion.Euler(thing.eulerAngles + r.normalized * Time.deltaTime * speeds[i, 1]);
                    r = rot[i] - thing.eulerAngles;
                    float[] f = { r.x, r.y, r.z };
                    r = CheckChangeRotation(f);
                }

                if (v.magnitude > 0.1)
                {
                    thing.position += v.normalized * Time.deltaTime * speeds[i, 0];
                    v = pos[i] - thing.position;
                }

                yield return null;
            }
            thing.position = pos[i];
            thing.rotation = Quaternion.Euler(rot[i]);

            StepDone();
        }
    }
    Vector3 CheckChangeRotation(float[] f)
    {
        for (int i = 0; i < 3; i++)
        {
            if (Mathf.Abs(f[i]) > 180)
            {
                f[i] = (f[i] / Mathf.Abs(f[i])) * (Mathf.Abs(f[i]) - 360);
            }
        }

        return new Vector3(f[0], f[1], f[2]);
    }

    protected void EventStartedElseWhere()
    {
        GetUI().StartEvent(this);
    }

    UIController GetUI()
    {
        if (ui == null)
        {
            ui = FindObjectOfType<UIController>();
        }
        return ui;
    }
}
