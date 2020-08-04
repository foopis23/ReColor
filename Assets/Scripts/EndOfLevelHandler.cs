using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevelHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("test");
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            GameEventSystem.Current.FireEvent(new EndLevelInfo(player));
        }

    }
}
