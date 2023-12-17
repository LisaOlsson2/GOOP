using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class FirstPController : CamController
{
    // interacting
    readonly float reach = 30;
    Transform hit;
    int hitListPlace;
    readonly List<Transform> hits = new();
    readonly List<Outlines> outlines = new();

    // movement
    readonly float sensitivity = 8;
    protected Vector3 forward2;


    protected override void OnEnable()
    {
        base.OnEnable();
        Cursor.lockState = CursorLockMode.Locked;
        ui.Hide(false);
    }
    
    protected virtual void Update()
    {
        transform.localRotation = Quaternion.Euler(transform.localEulerAngles + new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * sensitivity);

        if (transform.localEulerAngles.x < 180)
        {
            if (transform.localEulerAngles.x > 85)
            {
                transform.localRotation = Quaternion.Euler(85, transform.localEulerAngles.y, 0);
            }
        }
        else if (transform.localEulerAngles.x < 275)
        {
            transform.localRotation = Quaternion.Euler(275, transform.localEulerAngles.y, 0);
        }

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit info, reach, LayerMask.GetMask("Outliner")))
        {
            if (info.transform != hit)
            {
                if (hit != null)
                {
                    outlines[hitListPlace].enabled = false;
                }

                hit = info.transform;
                GetOutlines(hit).enabled = true;
            }
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Interact(hit);
            }
        }
        else if (hit != null)
        {
            hit = null;
            outlines[hitListPlace].enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit info2, reach, LayerMask.GetMask("LeftClickable")))
            {
                Interact(info2.transform);
            }
        }

        forward2 = new(Mathf.Sin(Mathf.Deg2Rad * transform.localEulerAngles.y), 0, Mathf.Cos(Mathf.Deg2Rad * transform.localEulerAngles.y));
    }

    private Outlines GetOutlines(Transform hit)
    {
        for (hitListPlace = 0; hitListPlace < hits.Count; hitListPlace++)
        {
            if (hit == hits[hitListPlace])
            {
                return outlines[hitListPlace];
            }
        }

        hits.Add(hit);
        outlines.Add(hit.GetComponent<Outlines>());
        
        return outlines[hitListPlace];
    }

    private void Interact(Transform hitSaved)
    {
        if (hitSaved.CompareTag("Savepoint"))
        {
            if (ui.saver != null)
            {
                ui.saver.Save();
            }
        }
        else if (hitSaved.CompareTag("Item"))
        {
            ui.ItemFound(hitSaved.gameObject.name);
            Destroy(hitSaved.gameObject);
        }
        else if (hitSaved.CompareTag("SceneChanger"))
        {
            if (ui.saver != null)
            {
                ui.saver.ChangeGameScene(hitSaved.gameObject.name.ToCharArray()[0]);
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Scene" + hitSaved.gameObject.name.ToCharArray()[0]);
            }
        }
        else if (hitSaved.CompareTag("Event") || (ui.item != null && ui.item != "" && hitSaved.CompareTag(ui.item)))
        {
            if (hit != null)
            {
                hit = null;
                outlines[hitListPlace].enabled = false;
            }
            ui.StartEvent(hitSaved.GetComponent<Events>());
        }
    }

}
