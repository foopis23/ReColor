using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ParController : MonoBehaviour
{
    private TMP_Text text;
    private LevelData levelData;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        levelData = GameConstants.Current.getLevelData(SceneManager.GetActiveScene().buildIndex);

        if (levelData != null)
        {
            text.text = "Par: " + levelData.ParScore;
        }
    }

    private void Update()
    {
        //This is wasteful code. I did this because I think there is some kind of race condition going on with the game constants. Idk why but here we are
        if (levelData == null)
        {
            levelData = GameConstants.Current.getLevelData(SceneManager.GetActiveScene().buildIndex);

            if (levelData != null)
            {
                text.text = "Par: " + levelData.ParScore;
            }
        }
    }
}
