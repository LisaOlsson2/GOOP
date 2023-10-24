using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    static GameObject ui;

    [SerializeField]
    CamController toEnable; // this can be empty if the last step changes the scene

    protected int step;

    protected virtual void OnEnable()
    {
        if (ui == null)
        {
            ui = FindObjectOfType<UIController>().gameObject;
        }
        ui.SetActive(false);
    }

    protected virtual void StepDone()
    {
        step++;
    }

    protected IEnumerator MoveCam(Vector3[] pos, Vector3[] rot, float[,] speeds)
    {
        Transform cam = toEnable.transform;

        for (int i = 0; i < pos.Length; i++)
        {
            Vector3 v = pos[i] - cam.position;
            Vector3 r = rot[i] - cam.localEulerAngles;
            while (v.magnitude > 0.1 || r.magnitude > 1)
            {
                float[] f = { r.x, r.y, r.z };
                r = CheckChangeRotation(f);

                cam.position += v.normalized * Time.deltaTime * speeds[i, 0];
                cam.localRotation = Quaternion.Euler(cam.localEulerAngles + r.normalized * Time.deltaTime * speeds[i, 1]);

                v = pos[i] - cam.position;
                r = rot[i] - cam.localEulerAngles;

                yield return null;
            }
        }
        StepDone();
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

    protected void AllDone()
    {
        step = 0;
        ui.SetActive(true);
        toEnable.enabled = true;
        this.enabled = false;
    }
}
