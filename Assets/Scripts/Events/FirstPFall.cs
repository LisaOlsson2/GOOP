using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPFall : Events
{
    [SerializeField]
    Transform floor;

    readonly float delay = 3;

    float t;

    protected override void OnEnable()
    {
        base.OnEnable();

        ((PositionChanger)toEnable).cam.orthographic = false;

        toEnable.transform.position = Vector3.forward * floor.transform.position.z;
        toEnable.transform.rotation = Quaternion.Euler(Vector3.up * 180);

        Rigidbody rb = toEnable.gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

    }

    private void Update()
    {
        if (step == 0 && toEnable.transform.position.y < -2)
        {
            StartCoroutine(RotateThing(toEnable.transform, new Vector3(270, 180, 0), 100));
            StepDone();
        }
        
        if (step == 2 && toEnable.transform.position.y < -500)
        {
            toEnable.transform.rotation = Quaternion.Euler(Vector3.right * 90);
            toEnable.transform.GetChild(0).GetComponent<AudioSource>().Play();
            StepDone();
        }

        if (step == 3)
        {
            t += Time.deltaTime;

            if (t > delay)
            {
                ChangeScene('S');
                StepDone();
            }
        }
    }
}
