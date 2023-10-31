using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneBeyond : MonoBehaviour
{
    [SerializeField]
    Transform t;
    [SerializeField]
    int place;
    [SerializeField]
    float coord;
    [SerializeField]
    char scene;

    void Update()
    {
        if ((coord < 0 && GetCoord() < coord) || ( coord > 0 && GetCoord() > coord))
        {
            Saver saver = FindObjectOfType<Saver>();
            if (saver != null)
            {
                saver.ChangeGameScene(scene);
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Scene" + scene);
            }
        }
    }

    float GetCoord()
    {
        if (place == 0)
        {
            return t.position.x;
        }
        if (place == 1)
        {
            return t.position.y;
        }
        return t.position.z;
    }
}
