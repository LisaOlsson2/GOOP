using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu
{
    UIController ui;
    CursorLockMode cursor;

    bool controllerAbled;

    public void Return()
    {
        Cursor.lockState = cursor;
        Time.timeScale = 1;
        UnselectCurrent();
        ui.gameObject.SetActive(controllerAbled);
        ui.enabledController.enabled = controllerAbled;
        gameObject.SetActive(false);
    }

    public void Open(UIController u)
    {
        ui = u;
        controllerAbled = ui.enabledController.enabled;
        cursor = Cursor.lockState;

        Time.timeScale = 0;
        ui.enabledController.enabled = false;
        ui.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        gameObject.SetActive(true);
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
