using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState
    {
        Gameplay,
        Pause,
        LevelUp,
        GameOver
    }

    public GameState currentState;
    public GameState previousState; //prevents weird logic bugs when pausing the game
    public GameObject gameOverScreen;
    public GameObject levelUpScreen;
    public GameObject pauseScreen;
    public GameObject playerLevelSydema;

    public bool isGameOver = false;
    public bool isPaused = false;
    public bool chooseUpgrade = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.LogWarning("DUPLICATE" + this + "REMOVED");
            Destroy(gameObject);
        }
        DisableScreen();
    }

    
    void Update()
    {
        switch (currentState)
        {
            case GameState.Gameplay:
                PauseInput();
                break;
            case GameState.Pause:
                PauseInput();
                break;
            case GameState.LevelUp:
                if (!chooseUpgrade)
                {
                    chooseUpgrade = true;
                    Time.timeScale = 0;
                    levelUpScreen.SetActive(true);
                }
                break;
            case GameState.GameOver:
                if (!isGameOver)
                {
                    isGameOver = true;
                    Time.timeScale = 0f;
                    GameOverScreen();
                }
                break;

            default:
                Debug.Log("NULL STATE");
                break;
        }
    }


    public void ChangeState(GameState newState) => currentState = newState;
    
    public void GameOver() => ChangeState(GameState.GameOver);
    
    
    public void DisableScreen()
    {
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        levelUpScreen.SetActive(false);
    }


    public void PauseGame()
    {
        if (currentState != GameState.Pause)
        {
            previousState = currentState;
            ChangeState(GameState.Pause);
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            isPaused = true;
        }
    }


    public void Resume()
    {
        if (currentState == GameState.Pause)
        {    
            ChangeState(previousState);
            Time.timeScale = 1f;
            DisableScreen();
            isPaused = false;
        }
    }


    void PauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Pause)
                Resume();
            else
                PauseGame();
        }
    }


    public void GameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }


    public void StartEvolution()
    {
        ChangeState(GameState.LevelUp);
        playerLevelSydema.SendMessage("ApplyAndRemoveEvolution");
    }


    public void EndEvolution()
    {
        chooseUpgrade = false;
        Time.timeScale = 1f;
        levelUpScreen.SetActive(false);
        ChangeState(GameState.Gameplay);
    }
}
