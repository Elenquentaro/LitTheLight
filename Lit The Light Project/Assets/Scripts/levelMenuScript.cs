using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelMenuScript : MonoBehaviour
{
    public Sprite imageNull, imageSecond, imageSecret, imageOpened;
    public GameObject doorSecond, doorSecret;

    void Start()
    {
        int LevelAccess = 1;

        if (PlayerPrefs.HasKey("LevelAccess"))
        {
            LevelAccess = PlayerPrefs.GetInt("LevelAccess");
        }


        // в соответствии с прогрессом прохождения уровней устанавливаем соответствующие спрайты на двери и распределяем доступ
        switch (LevelAccess)
        {
            case 0:
                goto case 1;
            case 1:
                doorSecond.GetComponent<Button>().enabled = false;
                doorSecond.GetComponent<Image>().sprite = imageNull;
                doorSecond.GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
                goto case 2;
            case 2:
                doorSecret.GetComponent<Button>().enabled = false;
                doorSecret.GetComponent<Image>().sprite = imageNull;
                doorSecret.GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
                break;
        }
    }

    public void LevelSelected(int Index)
    {
        PlayerPrefs.SetInt("Level", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + Index);
    }
}
