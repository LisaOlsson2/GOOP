using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPMovementT : FirstPController
{
    readonly float speed = 5, a = 0.1f, wobbleSpeed = 10;
    
    [SerializeField]
    float x1, x2, z1, z2;

    bool useBordersX, useBordersZ, wobble;

    float t;

    protected override void OnEnable()
    {
        base.OnEnable();

        useBordersX = x1 != x2;
        useBordersZ = z1 != z2;
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKey(forward))
        {
            transform.position += forward2 * Time.deltaTime * speed;
            wobble = true;
        }
        if (Input.GetKey(back))
        {
            transform.position -= forward2 * Time.deltaTime * speed;
            wobble = true;
        }

        if (Input.GetKey(right))
        {
            transform.position += new Vector3(transform.right.x, 0, transform.right.z) * Time.deltaTime * speed;
            wobble = true;
        }
        if (Input.GetKey(left))
        {
            transform.position -= new Vector3(transform.right.x, 0, transform.right.z) * Time.deltaTime * speed;
            wobble = true;
        }

        if (wobble)
        {
            t += Time.deltaTime;
            wobble = false;
        }
        else if (t > 0)
        {
            t = (Mathf.PI * 2) / wobbleSpeed % t;
            t -= Time.deltaTime;
        }
        
        transform.position = new Vector3(transform.position.x, Mathf.Sin(t * wobbleSpeed) * a, transform.position.z);

        if (useBordersX)
        {
            if (transform.position.x < x1)
            {
                transform.position = new Vector3(x1, transform.position.y, transform.position.z);
            }
            if (transform.position.x > x2)
            {
                transform.position = new Vector3(x2, transform.position.y, transform.position.z);
            }
        }

        if (useBordersZ)
        {
            if (transform.position.z < z1)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, z1);
            }
            if (transform.position.z > z2)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, z2);
            }
        }
    }
}
