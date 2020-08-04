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

public class PlayerEnterColorLaser : EventInfo
{
    public Player Target;
    public ColorLaserController Src;

    public PlayerEnterColorLaser(Player target, ColorLaserController src)
    {
        Target = target;
        Src = src;
    }
}

public class PlayerExitColorLaser : EventInfo
{
    public Player Target;
    public ColorLaserController Src;

    public PlayerExitColorLaser(Player target, ColorLaserController src)
    {

    }
}

public class PlayerInteractWithColorLaser : EventInfo
{
    public Player Src;
    public ColorLaserController Target;

    public PlayerInteractWithColorLaser(Player src, ColorLaserController target)
    {
        Src = src;
        Target = target;

    }
}

public class EndLevelInfo : EventInfo
{
    public Player Ender;
    public int GoToScene;

    public EndLevelInfo(Player ender, int goToScene=-1)
    {
        Ender = ender;
        GoToScene = goToScene;
    }
}