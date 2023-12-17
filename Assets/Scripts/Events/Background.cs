using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    SpriteRenderer sr, csr;

    [SerializeField]
    Sprite[] sprites;

    int sprite;
    readonly float speed = 0.5f;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        csr = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Color c = sr.color;

        if (c.a < 1)
        {
            c.a += Time.deltaTime * speed;
        }
        else
        {
            csr.sprite = sprites[sprite];
            sprite++;

            if (sprite == sprites.Length)
            {
                sprite = 0;
            }

            sr.sprite = sprites[sprite];
            c.a = 0;
        }
        sr.color = c;
    }
}
