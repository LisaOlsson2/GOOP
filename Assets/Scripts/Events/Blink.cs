using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : Events
{
    SpriteRenderer sr;
    readonly float speed = 7;

    protected override void OnEnable()
    {
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }

        StartCoroutine(Blinky());
    }

    IEnumerator Blinky()
    {
        while(sr.color.r > 0 || sr.color.g > 0 || sr.color.b > 0)
        {
            sr.color -= Color.white * Time.deltaTime * speed;
            yield return null;
        }
        yield return new WaitForSeconds(0.05f);
        while (sr.color.r < 1 || sr.color.g < 1 || sr.color.b < 1 || sr.color.a < 1)
        {
            sr.color += Color.white * Time.deltaTime * speed;
            yield return null;
        }
        enabled = false;
    }
}
