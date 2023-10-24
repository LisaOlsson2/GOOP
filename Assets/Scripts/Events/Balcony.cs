using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balcony : Events
{
    readonly Vector3[] p = { new(4, 0, 5.5f), new(4, 0, 10) }, r = { Vector3.zero, new(0, 345, 0) };
    readonly float[,] s = { { 10, 100 }, { 10, 100 } };

    readonly KeyCode[] interactKeys = { KeyCode.Mouse0, KeyCode.Space, KeyCode.Return };

    readonly string[] lines = { "...", "why don't you join me for a bit?", "helo" };

    static Text text;

    bool b;

    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(MoveCam(p, r, s));

        if (text == null)
        {
            text = GameObject.Find("TextBox").GetComponent<Text>();
        }
    }

    private void Update()
    {
        if (step > 0 && step < 3)
        {
            foreach (KeyCode k in interactKeys)
            {
                if (Input.GetKeyDown(k))
                {
                    text.text = lines[step - 1];
                    StepDone();
                }
            }
        }
        else if (step == 3)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                text.text = "";
                int[] nexts = { 1, 2 };
                StartCoroutine(SwitchChildren(0, 0.5f, nexts));
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Vector3[] p2 = { p[0] };
                Vector3[] r2 = { r[0] };
                float[,] s2 = { { s[0, 0], s[0, 1] } };

                StartCoroutine(MoveCam(p2, r2, s2));
                b = true;
            }
        }
        else if (step == 4)
        {
            foreach (KeyCode k in interactKeys)
            {
                if (Input.GetKeyDown(k))
                {
                    text.text = lines[step - 2];
                    StepDone();
                }
            }
        }
        else if (step == 5)
        {
            foreach (KeyCode k in interactKeys)
            {
                if (Input.GetKeyDown(k))
                {
                    int[] nexts = { 1, 0 };
                    StartCoroutine(SwitchChildren(2, 0.5f, nexts));
                    text.text = "";
                }
            }
        }
    }

    protected override void StepDone()
    {
        base.StepDone();

        if (step == 6)
        {
            Vector3[] p2 = { p[0] };
            Vector3[] r2 = { r[0] };
            float[,] s2 = { { s[0, 0], s[0, 1] } };
            StartCoroutine(MoveCam(p2, r2, s2));
            b = true;
        }

        if (b)
        {
            b = false;
            AllDone();
        }
    }

    IEnumerator SwitchChildren(int current, float time, int[] nexts)
    {
        for (int i = 0; i < nexts.Length; i++)
        {
            transform.GetChild(0).GetChild(current).gameObject.SetActive(false);
            current = nexts[i];
            transform.GetChild(0).GetChild(current).gameObject.SetActive(true);
            yield return new WaitForSeconds(time);
        }

        StepDone();
    }
}
