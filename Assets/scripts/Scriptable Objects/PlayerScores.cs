using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Scores", menuName = "PlayerScores")]
public class PlayerScores : ScriptableObject
{
    public int P1_score;
    public int P2_score;
}
