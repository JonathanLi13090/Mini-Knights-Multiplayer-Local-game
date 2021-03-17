using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float time_remaining = 10;
    public bool timer_running = false;
    public Text timer_text;

    //idea from https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/
    private void Start()
    {
        // start timer
        timer_running = true;
    }

    void Update()
    {
        handleTimer();
    }

    public void handleTimer()
    {
        if (timer_running)
        {
            if (time_remaining > 0)
            {
                time_remaining -= Time.deltaTime;
                DisplayTime(time_remaining);
            }
            else
            {
                //do something once time has run out
                time_remaining = 0;
                timer_running = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timer_text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
