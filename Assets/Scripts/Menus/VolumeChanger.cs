using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VolumeChanger : MonoBehaviour, IDeselectHandler
{
    readonly KeyCode[] keys = { KeyCode.Return, KeyCode.Space, KeyCode.W, KeyCode.S, KeyCode.Escape };

    readonly int defaultVolume = 5;

    [SerializeField]
    Selectable sound;
    Slider slider;

    AudioSource[] audioSources;

    public void Awoken()
    {
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetInt("volume", defaultVolume);
        }

        audioSources = FindObjectsOfType<AudioSource>();
        slider = GetComponent<Slider>();
        VolumeUpdated();
        slider.value = PlayerPrefs.GetInt("volume");
    }

    private void Update()
    {
        foreach (KeyCode k in keys)
        {
            if (Input.GetKeyDown(k))
            {
                Done();
            }
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Done();
    }

    void Done()
    {
        PlayerPrefs.SetInt("volume", (int)slider.value);
        VolumeUpdated();
        sound.gameObject.SetActive(true);
        sound.Select();
        gameObject.SetActive(false);
    }

    void VolumeUpdated()
    {
        int volume = PlayerPrefs.GetInt("volume");

        foreach (AudioSource a in audioSources)
        {
            a.volume = volume / 10;
        }
    }

}
