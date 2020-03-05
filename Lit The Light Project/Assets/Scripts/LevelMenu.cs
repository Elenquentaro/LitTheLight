using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public Sprite imageNull, imageSecond, imageSecret, imageOpened;
    public GameObject[] doors;

    [SerializeField] private Progress progress;

    void Start()
    {
        progress = SaveLoader.LoadProgress();

        switch (progress.Level)
        {
            case 0:
                goto case 1;
            case 1:
                doors[1].GetComponent<Image>().sprite = imageNull;
                doors[1].GetComponent<Button>().interactable = false;
                goto case 2;
            case 2:
                doors[2].GetComponent<Image>().sprite = imageNull;
                doors[2].GetComponent<Button>().interactable = false;
                break;
        }
    }

    public void LevelSelected(int index)
    {
        SceneManager.LoadScene(index);
    }
}
