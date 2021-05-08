using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupHandler : MonoBehaviour
{
    public GameObject[] spawnpoints = new GameObject[3];
    private float time_remaining = 10;
    public float total_time;
    public bool timer_running = false;
    public GameObject SwordPowerup;
    private Transform currentSpawn;

    // Start is called before the first frame update
    void Start()
    {
        time_remaining = total_time;
    }

    // Update is called once per frame
    void Update()
    {
        powerupTimer();
    }

    public void powerupTimer()
    {
        if (timer_running)
        {
            if (time_remaining > 0)
            {
                time_remaining -= Time.deltaTime;
            }
            else
            {
                spawnPowerup();
                time_remaining = 0;
                timer_running = false;
            }
        }
        else
        {
            timer_running = true;
            time_remaining = total_time;
        }
    }

    public void spawnPowerup()
    {
        int randChoice = Random.Range(0, 4);
        currentSpawn = spawnpoints[randChoice].transform;
        Instantiate(SwordPowerup, currentSpawn.position, Quaternion.identity);
    }
}
