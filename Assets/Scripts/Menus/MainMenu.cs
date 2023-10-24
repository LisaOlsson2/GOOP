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

    public void ShowSaves()
    {
        // starts at 1 because the first child is the back button
        for (int i = 1; i < PlayerPrefs.GetInt("saves") + 1; i++)
        {
            contineMenu.GetChild(i).GetComponent<Text>().text = "Save " + i;
            contineMenu.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }

    public void Continue(Transform save)
    {
        FindObjectOfType<Saver>().StartPlaying(save.GetSiblingIndex());
    }

    public void Quit()
    {
        Application.Quit();
    }
}
