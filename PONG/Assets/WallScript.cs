using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public PlayerScript player;
    [SerializeField]
    private GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Ball")
        {
            player.IncrenmentScore();
        }

        if (player.Score() < gameManager.maxScore)
        {
            collision.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
        }
    }
}
