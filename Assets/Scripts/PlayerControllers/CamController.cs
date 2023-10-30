using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CamController : MonoBehaviour
{
    protected UIController ui;

    private void Awake()
    {
        ui = FindObjectOfType<UIController>();
    }

    protected virtual void OnEnable()
    {
        ui.enabledController = this;
    }

}
