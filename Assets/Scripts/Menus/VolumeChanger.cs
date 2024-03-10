using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VolumeChanger : MonoBehaviour, IDeselectHandler
{
    readonly KeyCode[] keys = { KeyCode.Return, KeyCode.Space, KeyCode.W, KeyCode.S, KeyCode.Escape };

    [SerializeField]
    GameObject source;

    [SerializeField]
    Selectable button;
    
    Slider slider;

    AudioSource[] audioSources;
    public void Awoken()
    {
        if (source == null)
        {
            Saver saver = FindObjectOfType<Saver>();

            if (saver != null)
            {
                source = saver.gameObject;
            }
            else
            {
                source = gameObject;
            }
        }

        audioSources = source.GetComponents<AudioSource>();

        slider = GetComponent<Slider>();
        VolumeUpdated();
        slider.value = PlayerPrefs.GetInt(gameObject.name);
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
        PlayerPrefs.SetInt(gameObject.name, (int)slider.value);
        VolumeUpdated();
        button.gameObject.SetActive(true);
        button.Select();
        gameObject.SetActive(false);
    }

    protected virtual void VolumeUpdated()
    {
        int volume = PlayerPrefs.GetInt(gameObject.name);

        foreach (AudioSource a in audioSources)
        {
            a.volume = volume / 10f;
        }
    }


}
