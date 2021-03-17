using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce_pad : MonoBehaviour
{
    public LayerMask what_is_player;
    public Transform check_pos;
    public float check_range;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        handleBounce();
    }

    public void handleBounce()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(check_pos.position, check_range, what_is_player);        
        foreach (Collider2D player in hitPlayers)
        {
            player.GetComponent<player_controller>().Knockback(3);            
        }
        if(hitPlayers.Length > 0)
        {
            animator.SetTrigger("bounce");
        }
    }
}
