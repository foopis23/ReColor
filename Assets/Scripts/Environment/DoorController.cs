using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DoorController : MonoBehaviour
{
    [SerializeField]
    private int colorID;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        ColorScriptableObject colorList = GameConstants.Current.ColorList;
        Color color = colorList.colors[colorID];
        spriteRenderer.color = color;

        if (Application.isPlaying)
        {
            GameEventSystem.Current.RegisterListener<PlayerPopColorInfo>(popHandle);
            GameEventSystem.Current.RegisterListener<PlayerPushColorInfo>(pushHandle);
        }
    }

    private void OnDestroy()
    {
        if (Application.isPlaying)
        {
            GameEventSystem.Current.UnregisterListener<PlayerPopColorInfo>(popHandle);
            GameEventSystem.Current.UnregisterListener<PlayerPushColorInfo>(pushHandle);
        }
    }

    public void pushHandle(PlayerPushColorInfo info)
    {
        if (info.Color == colorID)
        {
            boxCollider.enabled = false;
        }
        else
        {
            boxCollider.enabled = true;
        }
    }

    public void popHandle(PlayerPopColorInfo info)
    {
        if (info.Color == colorID)
        {
            boxCollider.enabled = false;
        }
        else
        {
            boxCollider.enabled = true;
        }
    }
}
