using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ThirdPController : CamController
{
    protected Player player;
    protected Animator animator;
    protected SpriteRenderer sr;
    readonly float speed = 7;

    [SerializeField]
    protected Sprite idle;

    [SerializeField]
    Vector3[] CamPositions;

    int currentPos;
    float distanceY;
    float distanceX;

    protected override void OnEnable()
    {
        base.OnEnable();

        Cursor.lockState = CursorLockMode.Locked;
        ui.Hide(true);

        if (player == null)
        {
            player = FindObjectOfType<Player>();
            player.CamAccess(this);
            animator = player.GetComponent<Animator>();
            sr = player.GetComponent<SpriteRenderer>();

            sr.sprite = idle;

            distanceY =  Mathf.Sin(GetComponent<Camera>().fieldOfView/2 * Mathf.Deg2Rad) * (player.transform.position.z - transform.position.z);
            distanceX = distanceY * Screen.width / Screen.height;
        }
    }

    protected virtual void Update()
    {
        if (transform.position != CamPositions[currentPos])
        {
            if ((CamPositions[currentPos] - transform.position).magnitude > ((CamPositions[currentPos] - transform.position).normalized * speed * Time.deltaTime).magnitude)
            {
                transform.position += (CamPositions[currentPos] - transform.position).normalized * speed * Time.deltaTime;
            }
            else
            {
                transform.position = CamPositions[currentPos];
            }
        }


        if (Mathf.Abs(player.transform.position.x - transform.position.x) > distanceX || Mathf.Abs(player.transform.position.y - transform.position.y) > distanceY)
        {

            int closest = CamPositions.Length;
            for (int i = 0; i < CamPositions.Length; i++)
            {
                if (Mathf.Abs(player.transform.position.x - CamPositions[i].x) < distanceX && Mathf.Abs(player.transform.position.y - CamPositions[i].y ) < distanceY)
                {
                    if (closest == CamPositions.Length || (CamPositions[i] - transform.position).magnitude < (CamPositions[closest] - transform.position).magnitude)
                    {
                        closest = i;
                    }
                }
            }

            if (closest < CamPositions.Length)
            {
                currentPos = closest;
            }
        }
    }

    public void DidSomething(object o)
    {
        if (o is Transform t)
        {
            Interact(t);
        }
        else if (o is string s)
        {
            ui.PlaySound(s);
        }
    }
}
