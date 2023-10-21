using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Saver saver;
    public CamController enabledController;
    public string item;

    string tempItem;

    readonly float distance = 250, sensitivity = 4000;

    [SerializeField]
    GameObject itemsMenu;

    RectTransform middleThingy;
    Outline[] outlines;
    Collider2D[] colliders;


    //Temp
    [SerializeField]
    Image image;

    private void Start()
    {
        middleThingy = (RectTransform)transform;

        colliders = new Collider2D[itemsMenu.transform.childCount];
        outlines = new Outline[itemsMenu.transform.childCount];

        saver = FindObjectOfType<Saver>();

        if (saver == null)
        {
            return;
        }

        foreach (int i in saver.GetItems())
        {
            itemsMenu.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    public void ItemFound(string name)
    {
        for (int i = 0; i < itemsMenu.transform.childCount; i++)
        {
            GameObject itemObject = itemsMenu.transform.GetChild(i).gameObject;

            if (!itemObject.activeSelf && itemObject.name == name)
            {
                itemObject.SetActive(true);

                if (saver == null)
                {
                    return;
                }

                saver.ItemFound(i);
                return;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            itemsMenu.SetActive(true);
            enabledController.enabled = false;
            middleThingy.anchoredPosition = Vector2.up * distance;
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            middleThingy.anchoredPosition += new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
            middleThingy.anchoredPosition = middleThingy.anchoredPosition.normalized * distance;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            item = tempItem;
            tempItem = "";

            middleThingy.anchoredPosition = Vector2.zero;
            itemsMenu.SetActive(false);
            enabledController.enabled = true;

            string[] names = { "WateringCan", "Mp3Player", "Iron" };
            Color[] colors = { Color.red, Color.green, Color.blue };

            for (int i = 0; i < names.Length; i++)
            {
                if (names[i] == item)
                {
                    image.color = colors[i];
                    return;
                }
            }
            image.color = Color.clear;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int i;
        for (i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] == collision.collider)
            {
                Outline(i, true);
                return;
            }
            else if (colliders[i] == null)
            {
                break;
            }
        }

        colliders[i] = collision.collider;
        outlines[i] = colliders[i].GetComponent<Outline>();
        Outline(i, true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] == collision.collider)
            {
                Outline(i, false);
                return;
            }
        }
    }

    void Outline(int i, bool b)
    {
        outlines[i].enabled = b;
        if (b)
        {
            tempItem = outlines[i].gameObject.name;
        }
        else
        {
            tempItem = "";
        }
    }
}
