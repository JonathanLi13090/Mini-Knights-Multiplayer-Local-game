using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class training_dummy : MonoBehaviour
{
    public Rigidbody2D rb;
    public float knockbackX;
    public float knockbackY;
    public GameObject audio_handler;
    public GameObject respawn_point;
    public GameObject scoreBoardHandler;    
    public string respawn_tag;
    public float check_radius;
    public LayerMask respawn_layer;
    private bool is_respawn;
    public bool isP1;

    // Start is called before the first frame update
    void Start()
    {
        if (!audio_handler) { audio_handler = GameObject.FindGameObjectWithTag("audioHandler"); }
        if (!scoreBoardHandler) { scoreBoardHandler = GameObject.FindGameObjectWithTag("scoreBoardHandler"); }
    }

    // Update is called once per frame
    void Update()
    {
        handleRespawns();
    }

    public void Knockback(int direction)
    {
        //1 = right, 2 = left
        if (direction == 1)
        {
            rb.velocity = new Vector2(knockbackX, knockbackY);
            audio_handler.GetComponent<Audio_Handler>().PlaySound("Player", "player_hit");
        }
        else if (direction == 2)
        {           
            rb.velocity = new Vector2(-knockbackX, knockbackY);
            audio_handler.GetComponent<Audio_Handler>().PlaySound("Player", "player_hit");
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, knockbackY * 2);
            audio_handler.GetComponent<Audio_Handler>().PlaySound("Player", "player_jump");
        }       
    }

    public void RespawnPlayer()
    {
        if (isP1) { scoreBoardHandler.GetComponent<ScoreBoardHandler>().p1Scored(); }
        else { scoreBoardHandler.GetComponent<ScoreBoardHandler>().p2Scored(); }
        transform.position = new Vector2(respawn_point.transform.position.x, respawn_point.transform.position.y);
        rb.velocity = new Vector2(0, 0);       
        audio_handler.GetComponent<Audio_Handler>().PlaySound("Player", "player_score");
    }

    public void handleRespawns()
    {
        if (!respawn_point) { respawn_point = GameObject.FindGameObjectWithTag(respawn_tag); }
        is_respawn = Physics2D.OverlapCircle(transform.position, check_radius, respawn_layer);
        if (is_respawn)
        {
            RespawnPlayer();
        }
    }
}
