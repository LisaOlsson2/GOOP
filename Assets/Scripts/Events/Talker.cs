using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talker : Events
{
    [SerializeField]
    protected GameObject button;

    protected static Text text;
    readonly float delay = 0.05f;

    protected override void OnEnable()
    {
        base.OnEnable();

        if (text == null)
        {
            text = GameObject.Find("TextBox").GetComponent<Text>();
        }

        AbleControllers(false);
    }

    protected IEnumerator ShowText(string[] linylines)
    {
        for (int i = 0; i < linylines.Length; i++)
        {
            char[] brokenLine = linylines[i].ToCharArray();

            yield return null;

            foreach (char c in brokenLine)
            {
                text.text += c;
                float t = 0;

                bool b = false;

                while(t < delay)
                {
                    if (GetUI().InteractKey())
                    {
                        text.text = linylines[i];
                        b = true;
                        break;
                    }

                    t += Time.deltaTime;
                    yield return null;
                }

                if (b)
                {
                    break;
                }

            }

            yield return null;

            if (i < linylines.Length - 1)
            {
                yield return new WaitUntil(GetUI().InteractKey);
                text.text = "";
            }
        }
        StepDone();
    }

    public virtual void End()
    {
        text.text = "";
        button.SetActive(false);
    }
}
