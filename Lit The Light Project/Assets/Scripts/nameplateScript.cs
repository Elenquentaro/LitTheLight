using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nameplateScript : MonoBehaviour
{
    public GameObject text;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Player")
        {
            text.SetActive(true);
        }
    }

    void OnTriggerExit2D()
    {
        text.SetActive(false);
    }
}
