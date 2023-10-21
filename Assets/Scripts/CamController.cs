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

    // "pos.x,pos.y,pos.z,rot.x,rot.y,rot.z,"
    protected void ChangePerspective(string posAndRot)
    {
        char[] brokenName = posAndRot.ToCharArray();
        string s = "";
        List<float> f = new();
        foreach (char c in brokenName)
        {
            if (c == ',')
            {
                f.Add(float.Parse(s, System.Globalization.CultureInfo.InvariantCulture));
                s = "";
            }
            else
            {
                s += c;
            }
        }

        transform.position = new(f[0], f[1], f[2]);
        transform.localRotation = Quaternion.Euler(f[3], f[4], f[5]);

        if (other == null)
        {
            other = GetOther();
        }

        other.enabled = true;
        this.enabled = false;
    }

    protected virtual void Update()
    {

    }

    protected virtual CamController GetOther()
    {
        return null;
    }
}
