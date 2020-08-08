using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    public void OnClick()
    {
        GameEventSystem.Current.FireEvent(new EndLevelInfo(null, 0));
    }
}
