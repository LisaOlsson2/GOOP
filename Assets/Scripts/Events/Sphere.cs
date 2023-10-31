using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : Events
{
    readonly float distance = 25, camDistance = 55, fallDistance = 50, downDistance = 400;
    readonly Vector3[] pos = new Vector3[5], rot = new Vector3[5], directions = { Vector3.back, Vector3.left, Vector3.forward, Vector3.right };
    readonly float[,] speeds = { { 15, 100 }, { 7, 100 }, { 20, 100 }, { 20, 100 }, { 20, 100 } };

    int place;

    [SerializeField]
    Transform thingy;

    protected override void OnEnable()
    {
        base.OnEnable();

        Destroy(toEnable.GetComponent<Rigidbody>());

        for (int i = 0; i < directions.Length; i++)
        {
            if ((directions[i] * distance - toEnable.transform.position).magnitude < (directions[place] * distance - toEnable.transform.position).magnitude)
            {
                place = i;
            }
        }

        pos[0] = directions[place] * distance;
        pos[1] = pos[0] + directions[place] * 3;
        pos[2] = pos[0];
        pos[3] = pos[0] - directions[place] * 10 + Vector3.up * 5;
        pos[4] = directions[place] * 2.7f + Vector3.up * 3.8f;

        for (int i = 0; i < rot.Length; i++)
        {
            rot[i] = Vector3.up * 90 * place;
        }

        StartCoroutine(MoveCam(toEnable.transform, pos, rot, speeds));
    }

    private void Update()
    {
        if (step == 1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                thingy.gameObject.SetActive(true);
                thingy.position = directions[place] * 4;

                int place2 = place + 1;
                if (place2 == directions.Length)
                {
                    place2 = 0;
                }

                toEnable.transform.position = directions[place2] * camDistance;
                toEnable.transform.localRotation = Quaternion.Euler(toEnable.transform.localEulerAngles + Vector3.up * 90);
                thingy.localRotation = toEnable.transform.localRotation;
                StepDone();
            }
        }
        else if (step == 2)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Vector3[] p = { thingy.position + Vector3.down * fallDistance };
                Vector3[] r = { thingy.localEulerAngles };
                float[,] s = { { 20, 100 } };
                StepDone();
                StartCoroutine(MoveCam(thingy, p, r, s));
            }
        }
    }

    protected override void StepDone()
    {
        base.StepDone();

        if (step == 4)
        {
            thingy.gameObject.SetActive(false);
            Vector3[] p = { toEnable.transform.position + Vector3.down * downDistance };
            Vector3[] r = { toEnable.transform.localEulerAngles };
            float[,] s = { { 40, 100 } };
            StartCoroutine(MoveCam(toEnable.transform, p, r, s));
        }
        /*
        else if (step == 5)
        {
            Saver saver = FindObjectOfType<Saver>();
            if (saver != null)
            {
                saver.ChangeGameScene('F');
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("SceneF");
            }
        }
        */
    }
}
