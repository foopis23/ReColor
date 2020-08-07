using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class GameConstants : MonoBehaviour
{
    private static GameConstants current;

    public bool Paused = false;

    public static GameConstants Current
    {
        get
        {
            return current;
        }
    }

    [SerializeField]
    private ColorScriptableObject colorList;
    public ColorScriptableObject ColorList
    {
        get
        {
            return colorList;
        }
    }

    [SerializeField]
    private LevelDataList levelDataList;
    public LevelDataList LevelDataList
    {
        get
        {
            return levelDataList;
        }
    }

   [SerializeField]
   private int buildIndexOffset = 0;


    public LevelData getLevelData(int buildIndex = -1)
    {
        int arrIndex;

        if (buildIndex == -1)
        {
            arrIndex = SceneManager.GetActiveScene().buildIndex - buildIndexOffset;
        }
        else
        {
            arrIndex = buildIndex - buildIndexOffset;
        }

        if (levelDataList != null && levelDataList.levelDataList.Length > arrIndex && arrIndex > -1)
            return levelDataList.levelDataList[arrIndex];

        return null;
    }
    


    private void Awake()
    {
        current = this;
    }
}
