using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{

    Text text;

    readonly float speed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        text = FindObjectOfType<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (text.color.a < 1)
        {
            text.color += Color.black * Time.deltaTime * speed;
        }

        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
