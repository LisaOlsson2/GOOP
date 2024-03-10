using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sphere : Events
{
    readonly float flashTime = 0.1f, waitTime = 0.5f, fadeSpeed = 3, cutsceneDuration = 4;


    float t, transparency = 0;
    
    [SerializeField]
    Image black;

    [SerializeField]
    GameObject cutscene, ground, background;

    [SerializeField]
    Animator girl;

    [SerializeField]
    SpriteRenderer g;

    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    Vector3[] positions;

    readonly float distance = 20;

    protected override void OnEnable()
    {
        base.OnEnable();

        if (GameObject.Find("deer"))
        {
            enabled = false;
            return;
        }

        AbleControllers(false);
        black.gameObject.SetActive(true);

        Destroy(GetComponent<SphereCollider>());

        for (int i = 0; i < toEnable.transform.childCount; i++)
        {
            Destroy(toEnable.transform.GetChild(i).gameObject);
        }


        ground.SetActive(true);
        girl.gameObject.SetActive(true);


        toEnable.transform.rotation = Quaternion.Euler(Vector3.up * 90);
        g.gameObject.SetActive(true);

    }

    protected override void Update()
    {
        base.Update();

        if (step == 0)
        {
            if (toEnable.transform.position != transform.position - Vector3.right * distance)
            {
                toEnable.transform.position = transform.position - Vector3.right * distance;
            }

            t += Time.deltaTime;

            if (t > waitTime)
            {
                StartCoroutine(Flash(0));
                t = 0;
                StepDone();
            }
        }
        else if (step == 2)
        {
            t += Time.deltaTime;

            if (t > waitTime)
            {
                StartCoroutine(Flash(1));
                t = 0;
                StepDone();
            }
        }
        else if (step == 4)
        {
            t += Time.deltaTime;

            if (t > waitTime)
            {
                StartCoroutine(Flash(2));
                t = 0;
                StepDone();
            }
        }
        else if (step == 6)
        {
            t += Time.deltaTime;

            if (t > waitTime)
            {
                StartCoroutine(Flash(sprites.Length));
                t = 0;
                StepDone();
            }
        }
        else if (step == 8)
        {
            t += Time.deltaTime;

            if (t > cutsceneDuration)
            {
                cutscene.SetActive(false);
                background.SetActive(false);
                StartCoroutine(Flash(sprites.Length));
                t = 0;
                StepDone();
            }
        }

    }

    protected override void StepDone()
    {

        base.StepDone();

        if (step == 10)
        {
            girl.enabled = true;


        }

    }

    public void AnimationEnded()
    {
        girl.enabled = false;
        AbleControllers(true);
        Destroy(gameObject);
    }
    
    IEnumerator Flash(int sprite)
    {
        while(black.color.a < 1)
        {
            black.color += Color.black * Time.deltaTime * fadeSpeed;
            yield return null;
        }
        if (sprite < sprites.Length)
        {
            g.sprite = sprites[sprite];
            g.transform.position = positions[sprite];

            if (sprite == 1)
            {
                GetComponent<Renderer>().material.color = new Color(103 / 255f, 102 / 255f, 96 / 255f);
                transparency = 0.8f;
                Destroy(transform.GetChild(0).gameObject);
            }

        }
        else if (step == 7)
        {
            g.gameObject.SetActive(false);

            toEnable.transform.position = Vector3.up * -25;
            toEnable.transform.rotation = Quaternion.Euler(Vector3.zero);

            cutscene.SetActive(true);
            background.SetActive(true);
        }
        else if (step == 9)
        {
            toEnable.transform.position = Vector3.up * -50;
        }

        yield return new WaitForSeconds(flashTime);

        while (black.color.a > transparency)
        {
            black.color -= Color.black * Time.deltaTime * fadeSpeed;
            yield return null;
        }

        StepDone();
    }
}
