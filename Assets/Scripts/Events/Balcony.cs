using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balcony : Events
{
    readonly Vector3[] p = { new(4, 0, 5.5f), new(4, 0, 10) }, r = { Vector3.zero, new(0, 345, 0) }, p2 = new Vector3[1], r2 = new Vector3[1];
    readonly float[,] s = { { 15, 100 }, { 15, 100 } }, s2 = new float[1, 2];

    readonly string[] lines = { "", "...", "Won't you join me for a bit?", "...", "blabalbabalb", "You aren’t talking, but you’re saying a lot", "blabalbabalb", "" };

    static Text text;

    bool endWhenDone;

    protected override void OnEnable()
    {
        p2[0] = p[0];
        r2[0] = r[0];
        s2[0, 0] = s[0, 0];
        s2[0, 1] = s[0, 1];

        base.OnEnable();

        StartCoroutine(MoveCam(toEnable.transform, p, r, s));

        if (text == null)
        {
            text = GameObject.Find("TextBox").GetComponent<Text>();
        }
    }

    private void Update()
    {
        if ((step > 0 && step < 3) || (step > 3 && step < lines.Length))
        {
            foreach (KeyCode k in interactKeys)
            {
                if (Input.GetKeyDown(k))
                {
                    text.text = lines[step];
                    StepDone();
                }
            }
        }
        else if (step == 3)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                text.text = "";
                int[] nexts = { 2, 3 };
                StartCoroutine(SwitchChildren(1, 0.5f, nexts));
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                text.text = "";
                StartCoroutine(MoveCam(toEnable.transform, p2, r2, s2));
                endWhenDone = true;
            }
        }
    }

    protected override void StepDone()
    {
        base.StepDone();

        if (endWhenDone)
        {
            endWhenDone = false;
            AllDone();
        }
        else
        {
            if (step == 4)
            {
                text.text = lines[step - 1];
            }
            else if (step == lines.Length)
            {
                int[] nexts = { 2, 1 };
                StartCoroutine(SwitchChildren(3, 0.5f, nexts));
            }
            else if (step == lines.Length + 1)
            {
                StartCoroutine(MoveCam(toEnable.transform, p2, r2, s2));
                endWhenDone = true;
            }
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
