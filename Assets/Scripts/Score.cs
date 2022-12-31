using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    int score;
    string playerName;

    public Score(string newPlayerName, int newScore)
    {
        playerName = newPlayerName;
        score = newScore;
    }
}
