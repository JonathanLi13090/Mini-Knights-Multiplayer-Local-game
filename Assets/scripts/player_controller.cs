using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    public bool is_p1;

    public Rigidbody2D rb;
    public float move_speed;
    public float jump_force;
    public int max_jumps;
    private int jumps_left;

    public Transform ground_check;
    public float check_pos_distance;
    public float check_radius;
    private bool is_grounded;

    private bool facing_right = false;

    public LayerMask what_is_ground;
    public LayerMask what_is_enemy;

    public Transform attack_point;
    public Vector2 still_attack_point;
    public Vector2 moving_attack_point;
    public float attack_range;

    private bool shieldUp;

    public Animator animator;

    public GameObject respawn_point;
    public LayerMask respawn_layer;
    private bool is_respawn;
    public string respawn_tag;

    public float knockbackX;
    public float knockbackY;

    public GameObject scoreBoardHandler;

    public string right_button;
    public string left_button;
    public string up_button;
    public string down_button;
    public string attack_button;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handleAnimations();
        handleJumpCount();        
        handleJump();
        handleAttack();
        handleAttackPos();
        handleShieldUp();
        handleRespawns();
    }

    private void FixedUpdate()
    {
        handleHorizontalMovement();
    }

    //jump
    private void handleJump()
    {
        if (Input.GetKeyDown(up_button))
        {
            if (is_grounded == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, jump_force);          
            }
            else if (jumps_left > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jump_force);
                jumps_left -= 1;
            }
        }
    }

    private void handleJumpCount()
    {
        is_grounded = Physics2D.OverlapCircle(ground_check.position, check_radius, what_is_ground);
        if (is_grounded == true)
        {
            jumps_left = max_jumps;
        }
    }

    //movement
    private void handleHorizontalMovement()
    {
        if (Input.GetKey(right_button))
        {
            if(shieldUp == true) { rb.velocity = new Vector2(move_speed/2, rb.velocity.y); }
            else { rb.velocity = new Vector2(move_speed, rb.velocity.y); }
        }
        else if (Input.GetKey(left_button))
        {
            if(shieldUp == true) { rb.velocity = new Vector2(-move_speed/2, rb.velocity.y); }
            else { rb.velocity = new Vector2(-move_speed, rb.velocity.y); }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x/1.3f, rb.velocity.y);
        }

        if (facing_right == true && Input.GetKey(left_button))
        {
            Flip();
        }
        else if (facing_right == false && Input.GetKey(right_button))
        {
            Flip();
        }
    }

    public void Flip()
    {
        facing_right = !facing_right;
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    //attack
    public void handleAttack()
    {
        if(is_grounded == true && shieldUp == false)
        {
            if (Input.GetKeyDown(attack_button))
            {
                animator.SetTrigger("isAttacking");
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attack_point.position, attack_range, what_is_enemy);
                foreach (Collider2D enemy in hitEnemies)
                {
                    if (facing_right == true) { enemy.GetComponent<player_controller>().Knockback(1); }
                    else if (facing_right == false) { enemy.GetComponent<player_controller>().Knockback(2); }
                }
            }
        }        
    }

    public void handleAttackPos()
    {
        if (Mathf.Abs(rb.velocity.x) > 1f)
        {
            attack_point.localPosition = moving_attack_point;
        }
        else
        {
            attack_point.localPosition = still_attack_point;
        }
    }

    //knockback
    public void Knockback(int direction)
    {
        //1 = right, 2 = left
        if(shieldUp == true)
        {
            if (direction == 1)
            {
                if (facing_right == false) { rb.velocity = new Vector2(knockbackX / 2, knockbackY / 2); }
                else { rb.velocity = new Vector2(knockbackX, knockbackY); }
            }
            else if (direction == 2)
            {
                if (facing_right == true) { rb.velocity = new Vector2(-knockbackX / 2, knockbackY / 2); }
                else { rb.velocity = new Vector2(-knockbackX, knockbackY); }
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, knockbackY * 2);
            }
        }
        else
        {
            if (direction == 1)
            {
                rb.velocity = new Vector2(knockbackX, knockbackY);
            }
            else if (direction == 2)
            {
                rb.velocity = new Vector2(-knockbackX, knockbackY);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, knockbackY * 2);
            }
        }
        
    }

    //shield up
    public void handleShieldUp()
    {
        if (Input.GetKey(down_button))
        {
            shieldUp = true;
            animator.SetBool("shieldUp", true);
        }
        else
        {
            shieldUp = false;
            animator.SetBool("shieldUp", false);
        }
    }

    //animations
    public void handleAnimations()
    {
        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        if(is_grounded == true) { animator.SetBool("isJumping", false); }
        else { animator.SetBool("isJumping", true); }
    }

    
    //respawns
    public void RespawnPlayer()
    {
        transform.position = new Vector2(respawn_point.transform.position.x, respawn_point.transform.position.y);
        rb.velocity = new Vector2(0, 0);
        if (is_p1) { scoreBoardHandler.GetComponent<ScoreBoardHandler>().p1_scored(); }
        else { scoreBoardHandler.GetComponent<ScoreBoardHandler>().p2_scored(); }
    }

    public void handleRespawns()
    {
        if (!respawn_point) { respawn_point = GameObject.FindGameObjectWithTag(respawn_tag); }
        is_respawn = Physics2D.OverlapCircle(ground_check.position, check_radius, respawn_layer);
        if (is_respawn)
        {
            RespawnPlayer();
        }
    }
}
