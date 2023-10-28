using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Saver : MonoBehaviour
{
    // This script keeps track of any ways of altering progress values. Other scripts just use the easiest and then they're converted here

    //private readonly string[] progressPlaces = { "scene", "WateringCan", "Mp3Player", "Iron" };

    public readonly string startFormat = "00000";
    
    private int save;
    private char[] progress;

    public void StartPlaying(int saveSlot)
    {
        DontDestroyOnLoad(gameObject);
        save = saveSlot;
        progress = PlayerPrefs.GetString("save" + save).ToCharArray();
        SceneManager.LoadScene("Scene" + progress[0]);
    }

    public void Save()
    {
        string s = "";
        foreach(char c in progress)
        {
            s += c;
        }

        PlayerPrefs.SetString("save" + save, s);
        print("saved");
    }

    public void Exit()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Menu");
        Destroy(gameObject);
    }

    public void ChangeGameScene(char scene)
    {
        progress[0] = scene;
        SceneManager.LoadScene("Scene" + scene);
    }

    public int[] GetItems()
    {
        List<int> items = new();

        for (int i = 1; i < 4; i++)
        {
            if (progress[i] == '1')
            {
                items.Add(i - 1);
            }
        }

        return items.ToArray();
    }
    public void ItemFound(int item)
    {
        progress[item + 1] = '1';
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
