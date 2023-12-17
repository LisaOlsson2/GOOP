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

    readonly char[] itemScenes = { '0', 'Å', 'Ä' };

    [SerializeField]
    GameObject itemsMenu, pauseMenu;

    [SerializeField]
    Transform itemsParent;

    RectTransform middleThingy;
    Outline[] outlines;
    Collider2D[] colliders;

    Image thisImage;

    private void Close()
    {
        item = tempItem;
        tempItem = "";

        middleThingy.anchoredPosition = Vector2.zero;
        itemsMenu.SetActive(false);

        SetItem();
    }

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

        bool[] b = saver.GetItems();
        char c = saver.GetScene();
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");

        for (int i = 0; i < itemsMenu.transform.childCount; i++)
        {
            itemsMenu.transform.GetChild(i).gameObject.SetActive(b[i]);

            if (b[i] && c == itemScenes[i])
            {
                foreach (GameObject g in items)
                {
                    if (g.name == itemsMenu.transform.GetChild(i).gameObject.name)
                    {
                        Destroy(g);
                        break;
                    }
                }
            }
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

                if (saver != null)
                {
                    saver.ItemFound(i);
                }

                return;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            pauseMenu.SetActive(true);
            Disable();
            this.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            thisImage.enabled = true;
            itemsMenu.SetActive(true);
            enabledController.enabled = false;
            middleThingy.anchoredPosition = Vector2.up * distance;
        }

        if (itemsMenu.activeSelf)
        {
            middleThingy.anchoredPosition += new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
            middleThingy.anchoredPosition = middleThingy.anchoredPosition.normalized * distance;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Close();
            enabledController.enabled = true;
        }
    }

    private void SetItem() // make it so it doesn't have to be children of the player
    {
        if (itemsParent == null)
        {
            return;
        }

        GameObject unactivate = null;
        GameObject activate = null;
        for (int i = 0; i < itemsParent.childCount; i++)
        {
            GameObject child = itemsParent.GetChild(i).gameObject;

            if (child.activeSelf)
            {
                unactivate = child;
            }

            if (child.name == item)
            {
                activate = child;
            }
        }

        if (unactivate != null)
        {
            unactivate.SetActive(false);
        }

        if (activate != null)
        {
            activate.SetActive(true);
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

    public void StartEvent(Events e)
    {
        e.enabled = true;
        Disable();
    }

    private void Disable()
    {
        if (itemsMenu.activeSelf)
        {
            Close();
        }
    }

    public void Hide(bool hide)
    {
        if (thisImage == null)
        {
            thisImage = GetComponent<Image>();
        }

        thisImage.enabled = !hide;
    }
}
