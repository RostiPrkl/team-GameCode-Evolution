using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinManager : MonoBehaviour
{

    [SerializeField] GameObject winMessagePanel;

    public void Win()
    {
        winMessagePanel.SetActive(true);
    }
}
