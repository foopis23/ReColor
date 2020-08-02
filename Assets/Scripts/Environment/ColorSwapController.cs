using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider2D))]
public class ColorSwapController : MonoBehaviour
{
    [SerializeField]
    private int colorID;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = GameConstants.Current.ColorList.colors[colorID];
        color.a = 0.5f;
        spriteRenderer.color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Application.isPlaying)
        {
            Player player = collision.GetComponent<Player>();

            if (player != null)
            {
                GameEventSystem.Current.FireEvent(new ColorChangerPlayerPushColorInfo(player, colorID));
            }
        }
    }
}
