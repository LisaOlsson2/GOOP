using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CamController : MonoBehaviour
{
    protected UIController ui;

    protected readonly KeyCode forward = KeyCode.W, back = KeyCode.S, right = KeyCode.D, left = KeyCode.A;
    private void Awake()
    {
        ui = FindObjectOfType<UIController>();
    }

    protected virtual void OnEnable()
    {
        ui.enabledController = this;

    }

    protected void Interact(Transform hitSaved)
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
            ui.StartEvent(hitSaved.GetComponent<Events>());
        }
    }
}
