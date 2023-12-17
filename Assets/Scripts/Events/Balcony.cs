using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balcony : Events
{
    readonly string[] lines = { "...", "Won't you join me for a bit?", "blabalbabalb", "You aren’t talking, but you’re saying a lot", "blabalbabalb"};
    readonly float delay = 0.05f;

    readonly float[,] doorSpeed = { { 10, 100 } }, camSpeed = { { 5, 50}, { 3, 30} };
    readonly Vector3[] doorPos = new Vector3[1], doorRot = { Vector3.up * 90 }, camPos = new Vector3[2], camRot = { Vector3.up * 90, Vector3.up * 90};

    Text text;
    Animator animator;
    BoxCollider bc;

    private void Start()
    {
        text = GameObject.Find("TextBox").GetComponent<Text>();
        animator = GetComponent<Animator>();
        bc = GetComponent<BoxCollider>();
    }

    protected override void OnEnable()
    {
        if (step == 0)
        {
            base.OnEnable();

            transform.SetParent(null);

            doorPos[0] = new Vector3(1.5f, 0, toEnable.transform.position.z);
            StartCoroutine(MoveThing(transform, doorPos, doorRot, doorSpeed));
        }
        else if (step == 2)
        {
            AbleControllers(false);

            camPos[0] = new Vector3(1, toEnable.transform.position.y, transform.position.z);
            camPos[1] = new Vector3(1.5f, toEnable.transform.position.y, transform.position.z);

            StartCoroutine(MoveThing(toEnable.transform, camPos, camRot, camSpeed));
        }
        else
        {
            print("BAD");
        }
    }

    private void Update()
    {
        if (step == 5)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                text.text = "";
                StartCoroutine(ChildrenOfChild(new int[][] { new int[] { 1}, new int[] {1, 2} }));
                step = 7;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                text.text = "";
                End();
            }
        }
        else if (step == 9)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                text.text = "";
                StartCoroutine(ChildrenOfChild(new int[][] { new int[]{2,1}, new int[] { 1 }}));
                step = 10;
            }
        }
    }

    protected override void StepDone()
    {
        base.StepDone();

        if (step == 1)
        {
            animator.enabled = true;
        }
        else if (step == 2)
        {
            bc.size = new Vector3(3, 6, bc.size.z);
            enabled = false;
        }
        else if (step == 3)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (step == 4)
        {
            StartCoroutine(ShowText(0, 1));
        }
        else if (step == 7)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            step = 2;
            AbleControllers(true);
            enabled = false;
        }
        else if (step == 8)
        {
            StartCoroutine(ShowText(2, lines.Length - 1));
        }
        else if (step == 11)
        {
            End();
        }
    }

    IEnumerator ShowText(int start, int end)
    {
        for (int i = start; i <= end; i++)
        {
            char[] brokenLine = lines[i].ToCharArray();

            foreach (char c in brokenLine)
            {
                text.text += c;
                yield return new WaitForSeconds(delay);
            }

            if (i < end)
            {
                yield return new WaitUntil(Clicked);
                text.text = "";
            }
        }
        StepDone();
    }

    IEnumerator ChildrenOfChild(int[][] indexes)
    {
        foreach (int[] ints in indexes)
        {
            foreach (int i in ints)
            {
                GameObject g = transform.GetChild(0).GetChild(i).gameObject;
                g.SetActive(!g.activeSelf);
            }
            yield return new WaitForSeconds(0.4f);
        }
        StepDone();
    }

    void End()
    {
        step = 6;
        StartCoroutine(MoveThing(toEnable.transform, new Vector3[] { camPos[0] }, new Vector3[] { camRot[0] }, new float[,] { { camSpeed[1, 0], camSpeed[1, 1] } }));
    }


    bool Clicked()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    public void AnimationFinished(string animation)
    {
        if (animation == "Door")
        {
            StepDone();
        }
    }

    /*
    IEnumerator ScaleThing(Transform thing, Vector3 scale, float speed)
    {
        Vector3 v = scale - thing.localScale;
        while (v.magnitude > 0.5)
        {
            thing.localScale += v.normalized * Time.deltaTime * speed;
            v = scale - thing.localScale;

            yield return null;
        }

        thing.localScale = scale;
        StepDone();
    }
    */
}
