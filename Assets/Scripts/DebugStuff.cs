using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugStuff : MonoBehaviour
{
    private static DebugStuff current;
    public static DebugStuff Current
    {
        get
        {
            return current;
        }
    }

    private TMP_Text text;

    private void Awake()
    {
        current = this;
        text = GetComponent<TMP_Text>();
    }

    public void Log(string message)
    {
        text.text = message;
    }
}
