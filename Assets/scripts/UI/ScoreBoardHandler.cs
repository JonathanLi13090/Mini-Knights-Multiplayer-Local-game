using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardHandler : MonoBehaviour
{
    public Text P1_score;
    public Text P2_score;
    public int P1_score_num = 0;
    public int P2_score_num = 0;


    // Start is called before the first frame update
    void Start()
    {
        P1_score.text = P1_score_num.ToString();
        P2_score.text = P2_score_num.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
