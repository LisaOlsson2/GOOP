using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    ThirdPController cam;

    public void CamAccess(ThirdPController t)
    {
        cam = t;
    }

    private void OnTriggerEnter(Collider other)
    {
        cam.DidSomething(other.transform);
    }

    public void AnimationFinished(string animation)
    {
        if (animation == "StandUp")
        {
            FindObjectOfType<Sphere>().AnimationEnded();
        }
    }

    public void SoundOnFrame(string sound)
    {
        cam.DidSomething(sound);
    }
}
