using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToCam : MonoBehaviour
{
    Transform cam;
    float t;

    readonly float s = 3, a = 0.05f;

    readonly List<Transform> events = new();
    readonly List<Vector3> eventPositions = new();

    void Start()
    {
        cam = FindObjectOfType<Camera>().transform;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).CompareTag("Event"))
            {
                events.Add(transform.GetChild(i));
                eventPositions.Add(transform.GetChild(i).position);
            }
        }
    }

    void Update()
    {
        t += Time.deltaTime;

        if (t > 2 * Mathf.PI)
        {
            t = 0;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            child.forward = -(cam.position - child.position);

            if (child.CompareTag("Event"))
            {
                foreach (Transform c in events)
                {
                    if (c == child)
                    {
                        child.position = eventPositions[events.IndexOf(c)] + Vector3.up * a * Mathf.Sin(s * (i + t));
                        break;
                    }
                }

            }
        }
    }
}
