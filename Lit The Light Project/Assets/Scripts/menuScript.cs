using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
    public void Play()
    {
        // если сохранен прогресс на каком-то уровне, загружаем его. иначе начинаем игру сначала
        int Level = (PlayerPrefs.HasKey("Level") && PlayerPrefs.GetInt("Level") > 0) ? PlayerPrefs.GetInt("Level") : 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + Level);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
