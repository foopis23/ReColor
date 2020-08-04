using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ParController : MonoBehaviour
{
    private TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        Debug.Log(GameConstants.Current.LevelPars.Length);
        text.text = "Par: " + GameConstants.Current.LevelPars[SceneManager.GetActiveScene().buildIndex];
    }
}
