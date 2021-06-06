using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    Text gameOver_txt;

    [SerializeField]
    Sprite [] livesSprites;

    [SerializeField]
    Image livesUiImage;

    public bool isGameOver;

    private void Start()
    {
        scoreText.text = "Score: " + 0000;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
        gameOver_txt.gameObject.SetActive(false);
    }

    public void UpdateLives(int currentLives)
    {
        livesUiImage.sprite = livesSprites[currentLives];
    }

    public void GameOver()
    {
        isGameOver = true;
        StartCoroutine(GameOverFlickerRoutine());
        
    }
    IEnumerator GameOverFlickerRoutine()
    {
        while (isGameOver == true)
        {
            gameOver_txt.gameObject.SetActive(true);
            yield return new WaitForSeconds(.5f);
            gameOver_txt.gameObject.SetActive(false);
            yield return new WaitForSeconds(.5f);
        }
    }
}
