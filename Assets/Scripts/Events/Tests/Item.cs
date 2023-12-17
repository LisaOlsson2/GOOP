using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    readonly Vector3 distance = new(1, -2.5f, 0);

    void Update()
    {
        transform.position = transform.parent.position + transform.parent.right * distance.x + Vector3.up * distance.y;
        transform.rotation = Quaternion.Euler(Vector3.up * transform.parent.localEulerAngles.y);
    }
}
