using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider_web : MonoBehaviour
{
    public LayerMask what_is_player;
    public float check_range;

    public GameObject p1;
    public GameObject p2;


    // Start is called before the first frame update
    void Start()
    {
        if (!p1)
        {
            p1 = GameObject.FindGameObjectWithTag("p1");
        }
        if (!p2)
        {
            p2 = GameObject.FindGameObjectWithTag("p2");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, check_range, what_is_player);
        if(hitPlayers.Length > 0)
        {
            p1.GetComponent<player_controller>().change_move_speed(true);
            p1.GetComponent<player_controller>().change_jump_amount(true);
            p2.GetComponent<player_controller>().change_move_speed(true);
            p2.GetComponent<player_controller>().change_jump_amount(true);
            print("hit");
        }
        else
        {
            p1.GetComponent<player_controller>().change_move_speed(false);
            p1.GetComponent<player_controller>().change_jump_amount(false);
            p2.GetComponent<player_controller>().change_move_speed(false);
            p2.GetComponent<player_controller>().change_jump_amount(false);
        }
    }
}
