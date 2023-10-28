using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [SerializeField]
    Transform contineMenu;

    int saveSlots;

    public void NewSave()
    {
        Saver saver = FindObjectOfType<Saver>();


        saveSlots = contineMenu.childCount - 1;
        int saves = PlayerPrefs.GetInt("saves");

        if (saves < saveSlots - 1)
        {
            saves++;
            PlayerPrefs.SetInt("saves", saves);
            PlayerPrefs.SetString("save" + saves, saver.startFormat);
            saver.StartPlaying(saves);
        }
        else
        {
            print("No Saveslots left");
        }
    }

    public void ShowSaves(Transform menu)
    {
        // starts at 1 because the first child is the back button
        for (int i = 1; i < PlayerPrefs.GetInt("saves") + 1; i++)
        {
            menu.GetChild(i).GetComponent<Text>().text = "Save " + i + "\n" + PlayerPrefs.GetString("save" + i);
            menu.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }

    public void Continue(Transform save)
    {
        FindObjectOfType<Saver>().StartPlaying(save.GetSiblingIndex());
    }

    public void ClearSave(Text save)
    {
        PlayerPrefs.SetString("save" + save.transform.GetSiblingIndex(), FindObjectOfType<Saver>().startFormat);
        save.text = "Save " + save.transform.GetSiblingIndex() + "\n" + PlayerPrefs.GetString("save" + save.transform.GetSiblingIndex());
        UnselectCurrent();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
