using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName ="Level Data")]
public class LevelData : ScriptableObject
{
    public string Name;
    public int ParScore;
    [Min(1)]
    public int historySize = 1;
}
