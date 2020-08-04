using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score;
    private TMP_Text text;

    void Start()
    {
        score = 0;
        text = GetComponent<TMP_Text>();
        GameEventSystem.Current.RegisterListener<ColorChangerPlayerPushColorInfo>(ChangeColorHanlder);
    }

    void OnDestroy()
    {
        if (GameEventSystem.Current != null)
        {
            GameEventSystem.Current.UnregisterListener<ColorChangerPlayerPushColorInfo>(ChangeColorHanlder);
        }
    }

    public void ChangeColorHanlder(ColorChangerPlayerPushColorInfo info)
    {
        score += 1;
        text.text = "Score: " + score;
    }
}
