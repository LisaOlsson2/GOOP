using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainHouse : Events
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            AllDone();
            toEnable = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventStartedElseWhere();
        transform.GetChild(0).gameObject.SetActive(true);
        toEnable.transform.position = new Vector3(-20, -20, toEnable.transform.position.z);
    }
}
