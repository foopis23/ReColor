using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    private void Awake()
    {
        GameConstants.current = this;
    }
}
