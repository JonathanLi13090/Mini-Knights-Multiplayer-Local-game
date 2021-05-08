using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPowerup : MonoBehaviour
{
    public ParticleSystem particles;
    public float timeLeft;
    private bool timerStart;
    public SpriteRenderer spriteRenderer;
    public GameObject audio_handler;

    private void Start()
    {
        if (!audio_handler) { audio_handler = GameObject.FindGameObjectWithTag("audioHandler"); }
    }
    private void Update()
    {
        destructTimer();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("p1") || collision.CompareTag("p2"))
        {
            collision.GetComponent<player_controller>().attackPowerupStart();
            particles.Play();
            spriteRenderer.enabled = false;
            timerStart = true;
        }
    }

    void destructTimer()
    {
        if (timerStart)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
