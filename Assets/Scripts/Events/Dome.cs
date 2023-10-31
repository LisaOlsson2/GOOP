using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dome : MonoBehaviour
{
    bool platformGone;

    [SerializeField]
    Transform platform;

    readonly float speed = 10;

    private void Update()
    {
        if (platform.localScale.z > 10)
        {
            if (!platformGone)
            {
                if (platform.localScale.z < 265)
                {
                    platformGone = true;
                }
            }

            platform.localScale -= Vector3.forward * Time.deltaTime * speed;
            platform.position = new Vector3(0, -7, -30 - platform.localScale.z / 2);
        }
        else
        {
            enabled = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!enabled)
        {
            if (collision.transform.position.z > -295)
            {
                collision.rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                collision.transform.GetComponent<FirstPMovementT>().enabled = false;
                collision.transform.GetComponent<FirstPMovementP>().enabled = true;
                enabled = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (platformGone)
        {
            collision.rigidbody.constraints = RigidbodyConstraints.None;
        }
    }
}
