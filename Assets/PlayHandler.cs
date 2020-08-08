using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHandler : MonoBehaviour
{
    public void OnClick()
    {
        GameEventSystem.Current.FireEvent(new EndLevelInfo(null));
    }
}
