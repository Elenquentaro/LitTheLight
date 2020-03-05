using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour
{
    [SerializeField] private Lantern[] lanterns;

    void Awake()
    {
        GameManager.onGameStarted.AddListener((progress) =>
        {
            AssignLanterns(progress.LitedLanterns);
        });
    }

    public void AssignLanterns(bool[] litedLanterns)
    {
        for (int i = 0; i < lanterns.Length; i++)
        {
            lanterns[i].Assign(i);
            if (i < litedLanterns.Length && litedLanterns[i]) lanterns[i].PlayLited();
        }
    }
}
