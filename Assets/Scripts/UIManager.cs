using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    private void Start()
    {
        scoreText.text = "Score: " + 0000;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

}
