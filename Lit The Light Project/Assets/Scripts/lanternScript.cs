using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lanternScript : MonoBehaviour
{
    private Animator animLantern;
    private bool Lit = false;

    void Start()
    {
        animLantern = gameObject.GetComponent<Animator>();
        if (PlayerPrefs.GetInt("Level") == SceneManager.GetActiveScene().buildIndex)
        {
            Lit = PlayerPrefs.GetInt(gameObject.name) == 1 ? true : false;
            animLantern.SetBool("Lit", Lit);
            if (Lit)
                animLantern.Play("blueLantern");
        }
    }

    void addCOIN()
    {
        Lit = true;
        PlayerScript script = GameObject.Find("Player").GetComponent<PlayerScript>();
        script.GetCOIN();
        script.Save();
    }

    void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetInt(gameObject.name, Lit ? 1 : 0);
        }
    }
}
