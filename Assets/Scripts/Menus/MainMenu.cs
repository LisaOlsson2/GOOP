using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [SerializeField]
    Transform contineMenu;

    public void Play(Transform save)
    {
        Saver saver = FindObjectOfType<Saver>();

        if (!PlayerPrefs.HasKey("save" + save.GetSiblingIndex()))
        {
            PlayerPrefs.SetString("save" + save.GetSiblingIndex(), saver.startFormat);
        }

        saver.StartPlaying(save.GetSiblingIndex());
    }

    public void ShowSaves(Transform menu)
    {
        // starts at 1 because the first child is the back button
        for (int i = 1; i < menu.childCount; i++)
        {
            string texty = "Empty";

            if (PlayerPrefs.HasKey("save" + i))
            {
                texty = "Save" + i;

                if (menu.gameObject.name == "Clear")
                {
                    texty += "\n" + PlayerPrefs.GetString("save" + i);
                }
            }

            menu.GetChild(i).GetComponent<Text>().text = texty;
        }
    }

    public void ClearSave(Text save)
    {
        PlayerPrefs.DeleteKey("save" + save.transform.GetSiblingIndex());
        save.text = "Empty";
        UnselectCurrent();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
