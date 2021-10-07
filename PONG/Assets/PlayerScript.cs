using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public KeyCode upButton = KeyCode.W;
    public KeyCode downButton = KeyCode.S;
    public float speed = 10.0f; // kecepatan
    public float yBoundary = 9.0f; //batas vertikal

    private Rigidbody2D m_Rigidbody2D; //ambil rigid body object ini
    private int score; //score

    //debugging information
    private ContactPoint2D lastContactPoint;
    private Vector2 trajectoryOrigin;
    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }
    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
    // Start is called before the first frame update
    void Start()
    {
        trajectoryOrigin = transform.position;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = m_Rigidbody2D.velocity;
        //input gerakan
        if (Input.GetKey(upButton))
        {
            velocity.y = speed;
        } else if (Input.GetKey(downButton))
        {
            velocity.y = -speed;
        } else
        {
            velocity.y = 0;
        }
        m_Rigidbody2D.velocity = velocity;
        
        //Biar ga lewat batas
        Vector3 position = transform.position;

        if (position.y > yBoundary)
        {
            position.y = yBoundary;
        } else if (position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }
        transform.position = position;
    }
    public void IncrenmentScore()
    {
        score++;
    }
    public void ResetScore()
    {
        score = 0;
    }
    public int Score()
    {
        return score;
    }
}
