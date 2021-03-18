using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardHandler : MonoBehaviour
{
    public Text P1_score;
    public Text P2_score;
    public static int P1_score_num = 0;
    public static int P2_score_num = 0;

    public float time_remaining = 10;
    public bool timer_running = false;
    public Text timer_text;

    public GameObject SceneChanger;
    public string SceneChangerTag;

    public string roundOverScene;
    public bool P1_wins;
    public bool P2_wins;
    public Text WinText;

    // Start is called before the first frame update
    void Start()
    {
        if (!SceneChanger) { SceneChanger = GameObject.FindGameObjectWithTag(SceneChangerTag); }

        P1_score.text = P1_score_num.ToString();
        P2_score.text = P2_score_num.ToString();
        timer_running = true;
    }

    // Update is called once per frame
    void Update()
    {
        handleTimer();
    }

    public void handleTimer()
    {
        //idea from https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/

        if (timer_running)
        {
            if (time_remaining > 0)
            {
                time_remaining -= Time.deltaTime;
                DisplayTime(time_remaining);
            }
            else
            {
                handleRoundOver();
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

    public void reset_score()
    {
        P1_score_num = 0;
        P2_score_num = 0;
        P1_score.text = P1_score_num.ToString();
        P2_score.text = P2_score_num.ToString();
    }

    public void p1_scored()
    {
        P1_score_num += 1;
        P1_score.text = P1_score_num.ToString();
    }

    public void p2_scored()
    {
        P2_score_num += 1;
        P2_score.text = P2_score_num.ToString();
    }

    public void handleRoundOver()
    {
        SceneChanger.GetComponent<SceneChange>().ChangeScene(roundOverScene);       
    }
}
