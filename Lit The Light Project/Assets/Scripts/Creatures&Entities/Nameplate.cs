using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nameplate : MonoBehaviour
{
    public GameObject text;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerController>())
        {
            text.SetActive(true);
        }
    }

    void OnTriggerExit2D()
    {
        text.SetActive(false);
    }
}
