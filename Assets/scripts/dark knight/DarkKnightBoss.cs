using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkKnightBoss : MonoBehaviour
{
    private bool facing_right;
    public Transform player;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void choosePlayerPos()
    {
        int choice = Random.Range(1, 3);
        if (choice == 1) { player = GameObject.FindGameObjectWithTag("p1").transform; }
        if (choice == 2) { player = GameObject.FindGameObjectWithTag("p2").transform; }
    }

    public void lookAtPlayer()
    {
        float distance = Vector2.Distance(player.position, rb.position);
        if(distance < 0)
        {
            flip();
        }
    }

    public void faceRight()
    {
        Vector2 scaler = transform.localScale;
        scaler.x = 1;
    }

    public void faceLeft()
    {
        Vector2 scaler = transform.localScale;
        scaler.x = -1;
    }

    public void flip()
    {
        facing_right = !facing_right;
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
