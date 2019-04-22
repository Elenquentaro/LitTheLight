using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pauseScript : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject panel;

    public Texture textureCOIN;
    public int COINCounter = 0;

    private Rect rectCOIN1 = new Rect(140, 30, 40, 40);
    private Rect rectCOIN2 = new Rect(160, 30, 40, 40);
    private Rect rectCOIN3 = new Rect(180, 30, 40, 40);



    void Start()
    {
        if (PlayerPrefs.GetInt("Level") == SceneManager.GetActiveScene().buildIndex && PlayerPrefs.HasKey("COINCounter"))
            COINCounter = PlayerPrefs.GetInt("COINCounter");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Time.timeScale = 0;
                panel.SetActive(true);
                isPaused = true;
            }
            else
                Resume();
        }

    }

    public void Resume()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
        isPaused = false;
    }

    public void Quit()
    {
        PlayerPrefs.Save();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }



    public void OnGUI()
    {
        switch (COINCounter)
        {
            case 1:
                GUI.DrawTexture(rectCOIN1, textureCOIN);
                break;
            case 2:
                GUI.DrawTexture(rectCOIN1, textureCOIN);
                GUI.DrawTexture(rectCOIN2, textureCOIN);
                break;
            case 3:
                GUI.DrawTexture(rectCOIN1, textureCOIN);
                GUI.DrawTexture(rectCOIN2, textureCOIN);
                GUI.DrawTexture(rectCOIN3, textureCOIN);
                break;

        }
    }
}
