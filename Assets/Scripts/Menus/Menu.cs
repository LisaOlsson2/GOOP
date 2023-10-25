using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    [SerializeField]
    Selectable firstSelection;

    readonly KeyCode[] directionals = { KeyCode.A, KeyCode.S, KeyCode.W, KeyCode.D, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.UpArrow, KeyCode.DownArrow };
    bool keys;
    Vector3 mousePos;

    [SerializeField]
    bool b;

    void Update()
    {
        if (b)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                for (int i = 1; i < PlayerPrefs.GetInt("saves") + 1; i++)
                {
                    PlayerPrefs.DeleteKey("save" + i);
                }

                PlayerPrefs.DeleteKey("saves");
            }
        }

        if (!keys)
        {
            foreach (KeyCode k in directionals)
            {
                if (Input.GetKeyDown(k))
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    StartCoroutine(LockCursor());
                    break;
                }
            }
        }
        else if (Input.mousePosition != mousePos)
        {
            keys = false;
            EventSystem.current.SetSelectedGameObject(null);
            Cursor.lockState = CursorLockMode.None;
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

    IEnumerator LockCursor()
    {
        yield return new WaitUntil(CursorLocked);
        firstSelection.Select();
        keys = true;
    }

    private bool CursorLocked()
    {
        return Input.mousePosition == mousePos;
    }
}
