using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundOverHandler : MonoBehaviour
{
    public GameObject SceneChanger;
    public string SceneChangerTag;
    public string roundOverScene;
    public Text win_text;
    public PlayerScores player_scores;
    public Text P1_score;
    public Text P2_score;

    // Start is called before the first frame update
    void Start()
    {
        if (!SceneChanger) { SceneChanger = GameObject.FindGameObjectWithTag(SceneChangerTag); }

        RoundOver();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RoundOver()
    {
        P1_score.text = player_scores.P1_score.ToString();
        P2_score.text = player_scores.P2_score.ToString();

        if (player_scores.P1_score > player_scores.P2_score)
        {
            win_text.text = "Red Knight Wins!";
        }
        else if(player_scores.P2_score > player_scores.P1_score)
        {
            win_text.text = "Blue Knight Wins!";
        }
        else if(player_scores.P1_score == player_scores.P2_score)
        {
            win_text.text = "Tie Game!";
        }

        player_scores.P1_score = 0;
        player_scores.P2_score = 0;
    }
}
