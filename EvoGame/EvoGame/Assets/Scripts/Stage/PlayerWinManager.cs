using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinManager : MonoBehaviour
{

    [SerializeField] GameObject winMessagePanel;
    [SerializeField] DataContainer dataContainer;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    public void Win()
    {
        winMessagePanel.SetActive(true);
        gameManager.PauseGame();
        dataContainer.StageComplete(0);
    }
}
