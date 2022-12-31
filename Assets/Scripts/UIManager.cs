using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    SpawnManager spawnManager;
    [SerializeField]
    StateManager stateManager;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    Text gameOver_txt;

    [SerializeField]
    Sprite [] livesSprites;

    [SerializeField]
    Image livesUiImage;

    [SerializeField]
    Image Player2LivesUiImage;

    [SerializeField]
    GameObject gameOverScreen;

    [SerializeField]
    GameObject pauseMenu;

    private void Start()
    {
        gameOverScreen.gameObject.SetActive(false);
        gameOver_txt.gameObject.SetActive(false);
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        scoreText.text = "Score: " + 0000;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
        gameOver_txt.gameObject.SetActive(false);
    }

    public void UpdateLives(int currentLives, bool isRightPlayer)
    {
        if(isRightPlayer == false)
        {
            livesUiImage.sprite = livesSprites[currentLives];
        }
        if(isRightPlayer == true)
        {
            Player2LivesUiImage.sprite = livesSprites[currentLives];
        }
            
    }

    public void GameOver()
    {
        StartCoroutine(GameOverFlickerRoutine());
    }
    IEnumerator GameOverFlickerRoutine()
    {
        while (stateManager.isPlayerDead == true)
        {
            gameOverScreen.gameObject.SetActive(true);
            gameOver_txt.gameObject.SetActive(true);
            yield return new WaitForSeconds(.5f);
            gameOver_txt.gameObject.SetActive(false);
            yield return new WaitForSeconds(.5f);
        }
    }
    
    public void PauseMenuUIEnable()
    {
        pauseMenu.gameObject.SetActive(true);
    }    

    public void PauseMenuUIDisable()
    {
        pauseMenu.gameObject.SetActive(false);
    }
}