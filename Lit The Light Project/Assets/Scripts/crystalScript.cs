using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class crystalScript : MonoBehaviour
{
    private Animator animCrystal;
    public GameObject Key;
    void Start()
    {
        animCrystal = GetComponent<Animator>();
        int Level = PlayerPrefs.HasKey("Level") ? PlayerPrefs.GetInt("Level") : 0;
        if (Level == SceneManager.GetActiveScene().buildIndex
            && PlayerPrefs.GetInt("hasKey") == 1)
        {
            Crushed();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            animCrystal.SetBool("isActive", false);
        }
    }

    void Crushed()
    {
        PlayerScript pScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        pScript.hasKey = true;
        Key.SetActive(true);
        gameObject.SetActive(false);
    }
}
