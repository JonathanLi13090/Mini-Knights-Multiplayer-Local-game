//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class RoundOverHandler : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    public void handleRoundOver()
//    {
//        if (P1_score_num > P2_score_num)
//        {
//            P1_wins = true;
//            P2_wins = false;
//            print("P1 wins!");
//            WinText.text = "Blue Knight Wins!";
//        }
//        else if (P2_score_num > P1_score_num)
//        {
//            P2_wins = true;
//            P1_wins = false;
//            print("P2 wins!");
//            WinText.text = "Red Knight Wins!";
//        }
//        else
//        {
//            print("tie");
//            WinText.text = "Close Match! Tie Game!";
//        }
//    }
//}
