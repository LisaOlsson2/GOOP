using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStuff : MonoBehaviour
{
    [SerializeField]
    VolumeChanger volumeChanger;

    private void Awake()
    {
        volumeChanger.Awoken();
    }
}
