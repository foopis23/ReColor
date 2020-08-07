using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataList", menuName = "Level Data List")]
public class LevelDataList : ScriptableObject
{
    public LevelData[] levelDataList;
}
