using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    UIManager uIManager;
    int score = 0;

    void Start()
    {
        uIManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        if (uIManager == null)
        {
            Debug.LogError("UI Manager is null, in player script");
        }
        else if (uIManager != null)
        {
            Debug.Log("Unity sucks");
        }
    }

    public void ScoreKeeper()
    {
        score += 10;
        uIManager.UpdateScore(score);
    }
}
