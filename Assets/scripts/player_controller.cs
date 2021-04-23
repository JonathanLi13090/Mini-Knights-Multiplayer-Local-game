using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    //variables

    //audio
    public GameObject audio_handler;

    //physics
    public Rigidbody2D rb;

    public float current_move_speed;
    public float move_speed;
    public float slow_move_speed;

    public float current_jump_force;
    public float jump_force;
    public float small_jump_force;

    public int max_jumps;
    private int jumps_left;

    public float knockbackX;
    public float knockbackY;

    public Transform ground_check;
    public float check_pos_distance;
    public float check_radius;
    private bool is_grounded;

    //layermasks
    public LayerMask what_is_ground;
    public LayerMask what_is_enemy;

    //attack stuff
    public Transform attack_point;
    public Vector2 still_attack_point;
    public Vector2 moving_attack_point;
    public float attack_range;

    private bool shieldUp;

    //repawn stuff
    public GameObject respawn_point;
    public LayerMask respawn_layer;
    private bool is_respawn;
    public string respawn_tag;

    //scoreboard stuff
    public GameObject scoreBoardHandler;
    public string scoreBoard_tag;

    //input stuff
    public string right_button;
    public string left_button;
    public string up_button;
    public string down_button;
    public string attack_button;

    //other stuff
    public GameObject camShake;
    public bool is_p1;
    private bool facing_right = false;
    public Animator animator;

    //-----------------------------------------------------------------------------------------------------------------------------------\\
    void Start()
    {
        if(!audio_handler) { audio_handler = GameObject.FindGameObjectWithTag("audioHandler"); }
        if (!scoreBoardHandler) { scoreBoardHandler = GameObject.FindGameObjectWithTag(scoreBoard_tag); }
        if (!camShake) { camShake = GameObject.FindGameObjectWithTag("shake"); }
        current_move_speed = move_speed;
    }

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
                rb.velocity = new Vector2(rb.velocity.x, current_jump_force);
                audio_handler.GetComponent<Audio_Handler>().PlaySound("Player", "player_jump");
            }
            else if (jumps_left > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, current_jump_force);
                audio_handler.GetComponent<Audio_Handler>().PlaySound("Player", "player_jump");
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

    public void change_jump_amount(bool less_jump)
    {
        if (less_jump) { current_jump_force = small_jump_force; }
        else { current_jump_force = jump_force; }
    }

    //horizontal movement
    private void handleHorizontalMovement()
    {
        if (Input.GetKey(right_button))
        {
            if(shieldUp == true) { rb.velocity = new Vector2(current_move_speed / 2, rb.velocity.y); }
            else { rb.velocity = new Vector2(current_move_speed, rb.velocity.y); }
        }
        else if (Input.GetKey(left_button))
        {
            if(shieldUp == true) { rb.velocity = new Vector2(-current_move_speed / 2, rb.velocity.y); }
            else { rb.velocity = new Vector2(-current_move_speed, rb.velocity.y); }
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

    public void change_move_speed(bool slow_down)
    {
        if (slow_down) { current_move_speed = slow_move_speed; }
        else { current_move_speed = move_speed; }
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
        if(shieldUp == false)
        {
            if (Input.GetKeyDown(attack_button))
            {
                animator.SetTrigger("isAttacking");
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attack_point.position, attack_range, what_is_enemy);
                foreach (Collider2D enemy in hitEnemies)
                {
                    if (facing_right == true)
                    {
                        enemy.SendMessage("Knockback", 1);
                    }
                    else if (facing_right == false)
                    {
                        enemy.SendMessage("Knockback", 2);
                    }
                }
                if(hitEnemies.Length < 1)
                {
                    audio_handler.GetComponent<Audio_Handler>().PlaySound("Player", "player_attack");
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
                camShake.GetComponent<cameraShake>().shakeCamera();
                if (facing_right == false) { rb.velocity = new Vector2(knockbackX / 3, knockbackY / 3); }
                else { rb.velocity = new Vector2(knockbackX, knockbackY); }
                audio_handler.GetComponent<Audio_Handler>().PlaySound("Player", "player_hit");
            }
            else if (direction == 2)
            {
                camShake.GetComponent<cameraShake>().shakeCamera();
                if (facing_right == true) { rb.velocity = new Vector2(-knockbackX / 3, knockbackY / 3); }
                else { rb.velocity = new Vector2(-knockbackX, knockbackY); }
                audio_handler.GetComponent<Audio_Handler>().PlaySound("Player", "player_hit");
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, knockbackY * 2);
                audio_handler.GetComponent<Audio_Handler>().PlaySound("Enemy", "player_bounce");
            }
        }
        else
        {
            if (direction == 1)
            {
                camShake.GetComponent<cameraShake>().shakeCamera();
                rb.velocity = new Vector2(knockbackX, knockbackY);
                audio_handler.GetComponent<Audio_Handler>().PlaySound("Player", "player_big_hit");
            }
            else if (direction == 2)
            {
                camShake.GetComponent<cameraShake>().shakeCamera();
                rb.velocity = new Vector2(-knockbackX, knockbackY);
                audio_handler.GetComponent<Audio_Handler>().PlaySound("Player", "player_big_hit");
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, knockbackY * 2);
                audio_handler.GetComponent<Audio_Handler>().PlaySound("FX", "player_bounce");
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
        if (is_p1) { scoreBoardHandler.GetComponent<ScoreBoardHandler>().p1KnockedScore(); }
        else { scoreBoardHandler.GetComponent<ScoreBoardHandler>().p2KnockedScore(); }
        audio_handler.GetComponent<Audio_Handler>().PlaySound("Background", "player_score");
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
