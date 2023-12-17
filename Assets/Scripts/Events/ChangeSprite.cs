using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    [SerializeField]
    Sprite sprite;

    public void Change()
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
