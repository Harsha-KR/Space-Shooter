using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    List<Score> scores = new List<Score>();

    void Start()
    {
        scores.Add(new Score("Harsha", 50));
        scores.Add(new Score("bharath", 20));
        scores.Add(new Score("Raghav", 35));

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
