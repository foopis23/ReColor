using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColorLaserController : MonoBehaviour
{
    [SerializeField]
    private int color;
    public int Color
    {
        get
        {
            return this.color;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color temp = GameConstants.Current.ColorList.colors[color];
        temp.a = 0.5f;
        spriteRenderer.color = temp;

        if (GameEventSystem.Current != null)
        {
            GameEventSystem.Current.RegisterListener<PlayerInteractWithColorLaser>(handlePlayerInteraction);
        }
    }

    void OnDestroy()
    {
        if (GameEventSystem.Current != null)
        {
            GameEventSystem.Current.UnregisterListener<PlayerInteractWithColorLaser>(handlePlayerInteraction);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        
        if (player != null)
        {
            GameEventSystem.Current.FireEvent(new PlayerEnterColorLaser(player, this));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            GameEventSystem.Current.FireEvent(new PlayerExitColorLaser(player, this));
        }
    }

    private void handlePlayerInteraction(PlayerInteractWithColorLaser info)
    {
        if (info.Target == this)
            GameEventSystem.Current.FireEvent(new ColorChangerPlayerPushColorInfo(info.Src, color));
    }
}
