using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    Sprite [] livesSprites;

    [SerializeField]
    Image livesUiImage;

    private void Start()
    {
        scoreText.text = "Score: " + 0000;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int currentLives)
    {
        livesUiImage.sprite = livesSprites[currentLives];
    }

}
