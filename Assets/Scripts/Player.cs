using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SceneChanger"))
        {
            Saver saver = FindObjectOfType<Saver>();
            if (saver != null)
            {
                saver.ChangeGameScene(other.gameObject.name.ToCharArray()[0]);
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Scene" + other.gameObject.name.ToCharArray()[0]);
            }
        }
    }
}
