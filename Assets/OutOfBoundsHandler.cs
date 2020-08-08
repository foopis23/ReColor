using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBoundsHandler : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            GameEventSystem.Current.FireEvent(new EndLevelInfo(player, SceneManager.GetActiveScene().buildIndex));
        }
    }
}
