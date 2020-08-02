using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwapController : MonoBehaviour
{
    [SerializeField]
    private int colorID;

    [SerializeField]
    private ColorScriptableObject colorList;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = colorList.colors[colorID];
        color.a = 0.5f;
        spriteRenderer.color = color;
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player)
        {
            player.pushColor(colorID);
        }
    }
}
