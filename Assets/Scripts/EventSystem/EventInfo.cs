using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Stolen from https://github.com/quill18/UnityCallbackAndEventTutorial
//Modified by Eric Sexton
public class EventInfo
{

}

public class PlayerPopColorInfo : EventInfo
{
    public Player Sender;
    public int Color;

    public PlayerPopColorInfo(Player sender, int color)
    {
        Sender = sender;
        Color = color;
    }
}

public class PlayerPushColorInfo : EventInfo
{
    public Player Sender;
    public int Color;

    public PlayerPushColorInfo(Player sender, int color)
    {
        Sender = sender;
        Color = color;
    }
}

public class ColorChangerPlayerPushColorInfo : EventInfo
{
    public Player Target;
    public int Color;

    public ColorChangerPlayerPushColorInfo(Player target, int color)
    {
        Target = target;
        Color = color;
    }
}