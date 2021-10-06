using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    //force awal bola
    public float xInitialForce;
    public float yInitialForce;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ResetBall()
    {
        //reset posisi
        transform.position = Vector2.zero;
        //reset kecepatan
        rigidbody2D.velocity = Vector2.zero;
    }
    void PushBall()
    {
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);
        float randomDirection = Random.Range(0,2);

        if (randomDirection < 1.0f)
        {
            rigidbody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce));
        } else
        {
            rigidbody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce));
        }
    }
    void RestartGame()
    {
        ResetBall();
        Invoke("PushBall", 2);
    }
}
