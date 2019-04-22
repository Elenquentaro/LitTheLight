using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class settingsScript : MonoBehaviour
{
    public Text Gamemode;   //текст кнопки, отображающей текущий режим игры
    private string GamemodeValue;   //переменная, в которой будет храниться значение режима игры

    private Rect windowRect = new Rect((Screen.width - 200) / 2, (Screen.height - 60) / 2, 200, 70);
    private bool show = false;

    void Start()
    {
        GameModeText();
        Gamemode.text = GamemodeValue;
    }

    public void ChangeGameMode()
    {
        GameModeText();
        GamemodeValue = GamemodeValue == "Hardcore" ? "Normal" : "Hardcore";
        PlayerPrefs.SetString("Gamemode", GamemodeValue);
        Gamemode.text = GamemodeValue;
    }

    void GameModeText()
    {
        if (PlayerPrefs.HasKey("Gamemode"))
        {
            GamemodeValue = PlayerPrefs.GetString("Gamemode") == "Hardcore" ? "Hardcore" : "Normal";
        }
        else
        {
            PlayerPrefs.SetString("Gamemode", "Normal");
            GamemodeValue = "Normal";
        }
    }


    public void OnGUI()
    {
        if (show)
            windowRect = GUI.Window(0, windowRect, DialogWindow, "DELETE ALL SAVE DATA?");
    }

    void DialogWindow(int windowID)
    {

        if (GUI.Button(new Rect(5, 30, windowRect.width - 120, 35), "Delete"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            show = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (GUI.Button(new Rect(windowRect.width - 85, 30, windowRect.width - 120, 35), "Cancel"))
        {
            show = false;
        }
    }

    public void SubmitWindow()
    {
        show = true;
    }
}
