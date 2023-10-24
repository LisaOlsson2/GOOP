using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPController : CamController
{
    // interacting
    readonly float reach = 20;
    Transform hit;
    int hitListPlace;
    readonly List<Transform> hits = new();
    readonly List<Outlines> outlines = new();

    // movement
    readonly float sensitivity = 8, speed = 25;
    readonly KeyCode forward = KeyCode.W, back = KeyCode.S, right = KeyCode.D, left = KeyCode.A;


    protected override void OnEnable()
    {
        base.OnEnable();
        Cursor.lockState = CursorLockMode.Locked;
        ui.Hide(false);
    }
    
    private void Update()
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

        Vector3 forward2 = new(Mathf.Sin(Mathf.Deg2Rad * transform.localEulerAngles.y), 0, Mathf.Cos(Mathf.Deg2Rad * transform.localEulerAngles.y));

        if (Input.GetKey(forward))
        {
            transform.position += forward2 * Time.deltaTime * speed;
        }
        if (Input.GetKey(back))
        {
            transform.position -= forward2 * Time.deltaTime * speed;
        }

        if (Input.GetKey(right))
        {
            transform.position += transform.right * Time.deltaTime * speed;
        }
        if (Input.GetKey(left))
        {
            transform.position -= transform.right * Time.deltaTime * speed;
        }


        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit info, reach, LayerMask.GetMask("LeftClickable")))
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
        if (hit.CompareTag("Savepoint"))
        {
            if (ui.saver != null)
            {
                ui.saver.Save();
            }
        }
        else if (hit.CompareTag("Item"))
        {
            ui.ItemFound(hit.gameObject.name);
        }
        else if (ui.item != "" && hit.CompareTag(ui.item))
        {
            print("It got " + ui.item + "d");
        }
        else if (hit.CompareTag("SceneChanger"))
        {
            if (ui.saver != null)
            {
                ui.saver.ChangeGameScene(hit.gameObject.name.ToCharArray()[0]);
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Scene" + hit.gameObject.name.ToCharArray()[0]);
            }
        }
        else if (hit.CompareTag("Event"))
        {
            hit = null;
            outlines[hitListPlace].enabled = false;
            ui.StartEvent(hitSaved);
        }
    }

    protected override CamController MakeOtherRef()
    {
        return GetComponent<ThirdPController>();
    }
}
