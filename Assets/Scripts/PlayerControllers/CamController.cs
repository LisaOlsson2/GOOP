using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    private CamController other;
    protected UIController ui;

    private void Awake()
    {
        ui = FindObjectOfType<UIController>();
    }

    protected virtual void OnEnable()
    {
        ui.enabledController = this;
    }

    protected CamController GetOther()
    {
        if (other == null)
        {
            other = MakeOtherRef();
        }

        return other;
    }

    protected virtual CamController MakeOtherRef()
    {
        return null;
    }
}
