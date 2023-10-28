using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu
{
    [SerializeField]
    UIController ui;

    public void Return()
    {
        UnselectCurrent();
        ui.enabled = true;
        ui.enabledController.enabled = true;
        gameObject.SetActive(false);
    }

    public void Exit()
    {
        if (ui.saver != null)
        {
            ui.saver.Exit();
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }
    }
}
