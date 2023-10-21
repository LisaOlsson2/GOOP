using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    [SerializeField]
    Selectable firstSelection;

    [SerializeField]
    Transform contineMenu;

    readonly KeyCode[] directionals = { KeyCode.A, KeyCode.S, KeyCode.W, KeyCode.D, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.UpArrow, KeyCode.DownArrow };
    bool keys;
    Vector3 mousePos;
    int saveSlots;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 1; i < PlayerPrefs.GetInt("saves") + 1; i++)
            {
                PlayerPrefs.DeleteKey("save" + i);
            }

            PlayerPrefs.DeleteKey("saves");
        }

        if (!keys)
        {
            foreach (KeyCode k in directionals)
            {
                if (Input.GetKeyDown(k))
                {
                    firstSelection.Select();
                    keys = true;
                }
            }
        }
        else if (Input.mousePosition != mousePos)
        {
            keys = false;
            EventSystem.current.SetSelectedGameObject(null);
        }

        mousePos = Input.mousePosition;
    }

    
    public void MenuChanged(Selectable s)
    {
        firstSelection = s;

        if (keys)
        {
            firstSelection.Select();
        }
    }
    

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
