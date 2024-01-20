using UnityEngine;
using Gaskellgames.AudioController;
using UnityEditor;
//using UnityEngine.UI;

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
    public Transform playerTransform;

    public bool isGameOver = false;
    public bool isPaused = false;
    public bool chooseUpgrade = false;

    public SoundController sndCntrl;

    void Awake()
    {
        sndCntrl = FindObjectOfType<SoundController>();

        if (instance == null)
            instance = this;
        else
        {
            Debug.LogWarning("DUPLICATE" + this + "REMOVED");
            Destroy(gameObject);
        }
        //playerTransform = GetComponent<PlayerStats>().transform;
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
        sndCntrl.PlaySoundFX("playerDead");
        sndCntrl.gameObject.SetActive(false);
        sndCntrl.StopMusic();
        //sndCntrl.StopSoundFX();
        gameOverScreen.SetActive(true);


    }


    public void StartEvolution()
    {
        sndCntrl.PlaySoundFX("levelGain01");
        ChangeState(GameState.LevelUp);
        playerLevelSydema.SendMessage("ApplyAndRemoveEvolution");
    }


    public void EndEvolution()
    {
        chooseUpgrade = false;
        Time.timeScale = 1f;
        levelUpScreen.SetActive(false);
        ChangeState(GameState.Gameplay);
        sndCntrl.PlaySoundFX("evolutionSelected");
    }
    public void Start()
    {
        sndCntrl.PlaySoundFX("levelStart");
    }
}

