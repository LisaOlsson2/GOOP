using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStuff : MonoBehaviour
{
    readonly int defaultVolume = 5;

    [SerializeField]
    VolumeChanger[] volumeChangers;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("volume") || !PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetInt("volume", defaultVolume);
            PlayerPrefs.SetInt("music", defaultVolume);
        }

        foreach (VolumeChanger volumeChanger in volumeChangers)
        {
            volumeChanger.Awoken();
        }

        if (PlayerPrefs.HasKey("saves"))
        {
            for (int i = 1; i <= PlayerPrefs.GetInt("saves"); i++)
            {
                PlayerPrefs.DeleteKey("save" + i);
            }

            PlayerPrefs.DeleteKey("saves");
        }
    }
}
