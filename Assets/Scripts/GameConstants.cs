using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GameConstants : MonoBehaviour
{
    private static GameConstants current;

    public static GameConstants Current
    {
        get
        {
            return GameConstants.current;
        }
    }

    [SerializeField]
    private ColorScriptableObject colorList;
    public ColorScriptableObject ColorList
    {
        get
        {
            return this.colorList;
        }
    }

    [SerializeField]
    private int[] levelPars;
    public int[] LevelPars
    {
        get
        {
            return this.levelPars;
        }
    }


    private void Awake()
    {
        GameConstants.current = this;
    }
}
