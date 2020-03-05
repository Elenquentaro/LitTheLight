using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class Progress
{
    public int Level = 0;

    public bool HasKey = false;

    public Vector3 LastCheckpoint = Vector3.zero;

    public int COINs => (from bool isLited in LitedLanterns where isLited select isLited).Count();

    [SerializeField] bool[] litedLanterns = new bool[3];

    public bool[] LitedLanterns => litedLanterns;

    public void LanternLit(int index)
    {
        if (index < 0 || index >= litedLanterns.Length) throw new InvalidOperationException();
        litedLanterns[index] = true;
    }

    public static Progress GetFromJson(string json)
    {
        Progress settings = JsonUtility.FromJson<Progress>(json);
        return settings ?? new Progress();
    }

    public override string ToString()
    {
        return JsonUtility.ToJson(this, true);
    }
}