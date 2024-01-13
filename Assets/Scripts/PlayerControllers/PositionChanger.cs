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

        cam = GetComponent<Camera>();

        changed = new bool[GameObject.Find("ClickThings").transform.childCount];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
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
                else if (info.transform.CompareTag("Event"))
                {
                    ui.StartEvent(info.transform.GetComponent<Events>());
                }
            }

        }
    }
}
