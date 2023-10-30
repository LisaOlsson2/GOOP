using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : Events
{
    readonly KeyCode[] exitKeys = { KeyCode.Backspace, KeyCode.Escape }, gameKeys = { KeyCode.W, KeyCode.S, KeyCode.D, KeyCode.A };

    readonly Vector3[] p = { new(-20.3f, -3.4f, -13.3f) }, r = { new(0, 270, 0) }, beforePos = new Vector3[1], beforeRot = new Vector3[1];
    readonly float[,] s = { { 30, 100 } };

    SpriteRenderer ren;

    [SerializeField]
    Sprite[] sprites;


    void Update()
    {
        if (step == 1)
        {
            foreach (KeyCode k in exitKeys)
            {
                if (Input.GetKeyDown(k))
                {
                    StartCoroutine(MoveCam(toEnable.transform, beforePos, beforeRot, s));
                }
            }

            for (int i = 0; i < gameKeys.Length; i++)
            {
                if (Input.GetKeyDown(gameKeys[i]))
                {
                    ren.sprite = sprites[i];
                }
            }

        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (ren == null)
        {
            ren = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        beforePos[0] = toEnable.transform.position;
        beforeRot[0] = toEnable.transform.localEulerAngles;

        StartCoroutine(MoveCam(toEnable.transform, p, r, s));
    }

    protected override void StepDone()
    {
        base.StepDone();

        if (step == 2)
        {
            ren.sprite = sprites[0];
            AllDone();
        }
    }
}
