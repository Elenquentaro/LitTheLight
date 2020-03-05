using System;
using UnityEngine;

[Serializable]
public class Settings
{
    public enum Gamemode { Normal, Hardcore }

    public enum Jumpmode { Classic, Guided }

    [SerializeField] private Gamemode m_gamemode = Gamemode.Normal;
    public Gamemode gamemode => m_gamemode;

    [SerializeField] private Jumpmode m_jumpmode = Jumpmode.Classic;
    public Jumpmode jumpmode => m_jumpmode;

    public static Settings GetFromJson(string json)
    {
        Settings settings = JsonUtility.FromJson<Settings>(json);
        return settings ?? new Settings();
    }

    public void NextGameMode()
    {
        m_gamemode = GetNextState<Gamemode>(m_gamemode);
    }

    public void NextJumpMode()
    {
        m_jumpmode = GetNextState<Jumpmode>(m_jumpmode);
    }

    public static T GetNextState<T>(T current) where T : Enum
    {
        var values = Enum.GetValues(typeof(T));
        int curIndex = Array.IndexOf(values, current);
        return (T)(curIndex + 1 < values.Length ? values.GetValue(curIndex + 1) : values.GetValue(0));
    }

    public override string ToString()
    {
        return JsonUtility.ToJson(this, true);
    }
}