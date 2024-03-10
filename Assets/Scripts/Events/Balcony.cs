using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balcony : Talker
{
    // skriv monolog

    readonly string[] lines = { "...", "Oh hello there young one       \nWhat's your name?", "...", "Not much of a talker, huh?", "Well you're welcome to join me, listen while i reflect"};
    readonly string[] otherLines = {"blablabbaalbalb" , "ablblbabla", "balbblbb"};
    readonly float distance = 6;
    readonly int end = 20;

    readonly float[,] doorSpeed = { { 10, 100 } }, camSpeed = { { 5, 50}, { 3, 30} };
    readonly Vector3[] doorPos = new Vector3[1], doorRot = { Vector3.up * 90 }, camPos = new Vector3[2], camRot = { Vector3.up * 90, Vector3.up * 90};

    Animator animator;
    BoxCollider bc;

    [SerializeField]
    Sprite door;

    [SerializeField]
    GameObject buttonsParent;

    readonly List<SpriteRenderer> turners = new();

    private void Start()
    {
        animator = GetComponent<Animator>();
        bc = GetComponent<BoxCollider>();
    }

    protected override void OnEnable()
    {
        if (step == 0)
        {
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                if (transform.parent.GetChild(i) != transform && transform.parent.GetChild(i).position.x > 0)
                {
                    turners.Add(transform.parent.GetChild(i).GetComponent<SpriteRenderer>());
                }
            }

            transform.SetParent(null);

            doorPos[0] = new Vector3(distance, 0, toEnable.transform.position.z);
            StartCoroutine(MoveThing(transform, doorPos, doorRot, doorSpeed));
        }
        else if (step == 2)
        {
            base.OnEnable();

            foreach (SpriteRenderer s in turners)
            {
                s.sortingOrder = -3;
            }

            camPos[0] = new Vector3(distance - 0.5f, toEnable.transform.position.y, transform.position.z);
            camPos[1] = new Vector3(distance, toEnable.transform.position.y, transform.position.z);

            StartCoroutine(MoveThing(toEnable.transform, camPos, camRot, camSpeed));
        }
        else
        {
            print("BAD");
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
            animator.enabled = false;

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = door;
            sr.color = Color.white;
            bc.size = new Vector3(10, 10, bc.size.z);
            enabled = false;
        }
        else if (step == 3)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (step == 4)
        {
            StartCoroutine(ShowText(lines));
        }
        else if (step == 5)
        {
            buttonsParent.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else if (step == 6)
        {
            text.text = "";
            StartCoroutine(ShowText(otherLines));
        }
        else if (step == 7)
        {
            End();
        }
        else if (step == end + 1)
        {
            foreach (SpriteRenderer s in turners)
            {
                s.sortingOrder = 0;
            }

            transform.GetChild(0).gameObject.SetActive(false);
            step = 2;
            AbleControllers(true);
            enabled = false;
        }
    }

    public void Join()
    {
        StepDone();
    }


    /*
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
    */

    public override void End()
    {
        base.End();

        Cursor.lockState = CursorLockMode.Locked;
        step = end;
        StartCoroutine(MoveThing(toEnable.transform, new Vector3[] { camPos[0] }, new Vector3[] { camRot[0] }, new float[,] { { camSpeed[1, 0], camSpeed[1, 1] } }));
    }



    public void AnimationFinished(string animation)
    {
        if (animation == "Door")
        {
            StepDone();
        }
    }
}
