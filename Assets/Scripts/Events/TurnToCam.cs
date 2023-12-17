using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToCam : MonoBehaviour
{
    Transform cam;

    void Start()
    {
        cam = FindObjectOfType<Camera>().transform;
    }

    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).forward = -(cam.position - transform.GetChild(i).position);
        }
    }
}
