using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving_platform : MonoBehaviour
{
    public int move_distance;
    public float speed;
    public bool goingUp;
    public float startY;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
    }

    private void FixedUpdate()
    {
        handle_move();
    }

    public void handle_move()
    {
        rb.velocity = new Vector2(0, speed);

        if (goingUp == true)
        {
            if (startY + move_distance < transform.position.y)
            {
                goingUp = false;
                speed = speed * -1;
            }
        }
        else
        {
            if (transform.position.y < startY)
            {
                goingUp = true;
                speed = speed * -1;
            }
        }
    }
}
