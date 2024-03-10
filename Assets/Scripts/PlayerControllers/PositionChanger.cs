using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionChanger : CamController
{
    readonly float reach = 12;

    public Camera cam;

    bool[] changed;

    protected override void OnEnable()
    {
        base.OnEnable();
        Cursor.lockState = CursorLockMode.None;
        ui.Hide(true);

        cam = GetComponent<Camera>();

        changed = new bool[GameObject.Find("spritechangers").transform.childCount];
    }

    void Update()
    {
        if (ui.InteractKey())
        {
            if (Physics.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), transform.forward, out RaycastHit info, reach, LayerMask.GetMask("LeftClickable")))
            {
                if (info.transform.CompareTag("PositionChanger"))
                {
                    transform.position = Vector3.right * 20 * int.Parse(info.transform.gameObject.name);
                }
                else if (info.transform.CompareTag("SpriteChanger"))
                {
                    if (!changed[info.transform.GetSiblingIndex()])
                    {
                        info.transform.GetComponent<ChangeSprite>().Change();
                        changed[info.transform.GetSiblingIndex()] = true;
                    }
                }
                else if (info.transform.CompareTag("OnOff"))
                {
                    info.transform.GetChild(0).gameObject.SetActive(!info.transform.GetChild(0).gameObject.activeSelf);
                }
                else
                {
                    Interact(info.transform);
                }
            }

        }
    }

}
