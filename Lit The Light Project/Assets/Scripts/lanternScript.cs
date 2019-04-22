using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lanternScript : MonoBehaviour
{
    private Animator animLantern;

    void Start()
    {
        animLantern = gameObject.GetComponent<Animator>();
        if (PlayerPrefs.GetInt("Level") == SceneManager.GetActiveScene().buildIndex)
        {
            animLantern.SetBool("Lit", PlayerPrefs.GetInt(gameObject.name) == 1 ? true : false);
            animLantern.Play("blueLantern");
        }
    }

    void addCOIN()
    {
        PlayerScript script = GameObject.Find("Player").GetComponent<PlayerScript>();
        script.GetCOIN();
    }

    void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetInt(gameObject.name, animLantern.GetBool("Lit") ? 1 : 0);
        }
    }
}
