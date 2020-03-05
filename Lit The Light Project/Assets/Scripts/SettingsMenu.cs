using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Text gamemodeDisplay;
    [SerializeField] private Text jumpmodeDisplay;

    private Settings currentSettings;

    void Start()
    {
        currentSettings = SaveLoader.LoadSettings();

        gamemodeDisplay.text = currentSettings.gamemode.ToString();
        jumpmodeDisplay.text = currentSettings.jumpmode.ToString();
    }

    public void ChangeGameMode()
    {
        currentSettings.NextGameMode();
        gamemodeDisplay.text = currentSettings.gamemode.ToString();
        SaveLoader.SaveSettings(currentSettings);
    }

    public void ChangeJumpMode()
    {
        currentSettings.NextJumpMode();
        jumpmodeDisplay.text = currentSettings.jumpmode.ToString();
        SaveLoader.SaveSettings(currentSettings);
    }

    public void ResetProgress()
    {
        SaveLoader.SaveProgress(new Progress());
    }
}
