using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;

    //force awal bola
    public float xInitialForce;
    public float yInitialForce;
    public float speed;

    public bool is_reset = true;

    public Vector2 TrajectoryOrigin;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        RestartGame();
        TrajectoryOrigin = transform.position;
    }
    void ResetBall()
    {
        //reset posisi
        transform.position = Vector2.zero;
        //reset kecepatan
        m_Rigidbody2D.velocity = Vector2.zero;
    }
    void PushBall()
    {
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);
        float randomDirection = Random.Range(0,2);
        if (randomDirection < 1.0f)
        {
            m_Rigidbody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce).normalized * speed * 5);
        }
        else
        {
            m_Rigidbody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce).normalized * speed * 5);
        }
        is_reset = true;
    }
 
    void RestartGame()
    {
        if (is_reset)
        {
            is_reset = false;
            ResetBall();
            Invoke("PushBall", 2);
        }
    }
}
