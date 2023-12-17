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

}
