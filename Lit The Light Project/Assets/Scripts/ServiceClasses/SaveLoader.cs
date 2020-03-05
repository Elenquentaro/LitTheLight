using System;
using System.IO;
using UnityEngine;

public class SaveLoader : MonoBehaviour
{
    private static string dataPath = Application.persistentDataPath;
    private static string settingsFileName = "settings.dat";
    private static string progressFileName = "progress.dat";

    public static void SaveProgress(Progress progress)
    {
        var path = Path.Combine(dataPath, progressFileName);
        File.WriteAllText(path, progress.ToString());
    }

    public static Progress LoadProgress()
    {
        var path = Path.Combine(dataPath, progressFileName);
        if (!File.Exists(path)) return new Progress();
        return Progress.GetFromJson(File.ReadAllText(path));
    }

    public static void SaveSettings(Settings settings)
    {
        var path = Path.Combine(dataPath, settingsFileName);
        File.WriteAllText(path, settings.ToString());
    }

    public static Settings LoadSettings()
    {
        var path = Path.Combine(dataPath, settingsFileName);
        if (!File.Exists(path)) return new Settings();
        return Settings.GetFromJson(File.ReadAllText(path));
    }
}