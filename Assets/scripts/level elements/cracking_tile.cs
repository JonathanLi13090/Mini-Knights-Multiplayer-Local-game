using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cracking_tile : MonoBehaviour
{
    public float break_time = 2;
    public float current_break_time;
    public float reset_time = 1;
    public float current_reset_time = 1;
    public bool break_timer_running = false;
    public bool reset_timer_running = false;
    public BoxCollider2D boxCollider;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        current_break_time = break_time;
        current_reset_time = reset_time;
    }

    // Update is called once per frame
    void Update()
    {
        handleBreakTimer();
        handleResetTimer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!break_timer_running && !reset_timer_running)
        {
            break_timer_running = true;
            animator.SetTrigger("tile_break");
        }     
    }

    public void handleBreakTimer()
    {
        if (break_timer_running)
        {
            if (current_break_time > 0)
            {
                current_break_time -= Time.deltaTime;             
            }
            else
            {
                boxCollider.enabled = !boxCollider.enabled;
                reset_timer_running = true;
                current_break_time = break_time;
                break_timer_running = false;
            }
        }
    }

    public void handleResetTimer()
    {
        if (reset_timer_running)
        {
            if (current_reset_time > 0)
            {
                current_reset_time -= Time.deltaTime;
            }
            else
            {
                animator.SetTrigger("tile_reset");
                boxCollider.enabled = !boxCollider.enabled;
                current_reset_time = reset_time;
                reset_timer_running = false;
            }
        }
    }
}
