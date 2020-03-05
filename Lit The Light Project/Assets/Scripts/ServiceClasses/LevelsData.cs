using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Resources/Create Levels Data File", fileName = "Levels Data")]
public class LevelsData : ScriptableObject
{
    [SerializeField] private int availableLevels = 2;
    public int AvailableLevels => availableLevels;
}
