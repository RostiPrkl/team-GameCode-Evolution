using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugConsole : MonoBehaviour
{
    private string inputBuffer = "";
    private bool consoleVisible = false;
    private List<string> debugLogs = new List<string>();
    [SerializeField] PlayerStats playerStats;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
            ToggleConsole();
    }

    private void OnGUI()
    {
        if (consoleVisible)
        {
            GUI.Box(new Rect(0, 0, 600, 20), "Debug Console");
            inputBuffer = GUI.TextField(new Rect(0, 20, 400, 20), inputBuffer);

            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
                ExecuteCommand();
            
            for (int i = 0; i < debugLogs.Count; i++)
            {
                GUI.Label(new Rect(0, 50 + i * 20, 500, 20), debugLogs[i]);
            }
        }
    }


    private void LogCommand(string logMessage)
    {
        debugLogs.Add(logMessage);

        if (debugLogs.Count > 10)
        {
            debugLogs.RemoveAt(0);
        }
    }
    


    private void ToggleConsole()
    {
        consoleVisible = !consoleVisible;
    }


    private void ExecuteCommand()
    {
        string[] commandParts = inputBuffer.Split(' ');
        
        if (commandParts.Length > 0)
        {
            string command = commandParts[0].ToLower();

            switch (command)
            {
                case "godmode":
                    GodMode();
                    Debug.Log("Sending godmode");
                    break;
                
                // case "addlevel":
                //     AddLevel();
                //     break;

                default:
                    LogCommand("Unknown command: " + command);
                    break;
            }
        }

        inputBuffer = "";
    }

    private void GodMode()
    {
        Debug.Log("Receiving godmode");
        playerStats.debugInvincible = !playerStats.debugInvincible;

        if (playerStats.debugInvincible)
            LogCommand("GodMode ON");
        else
            LogCommand("GodMode OFF");
    }


    // private void AddLevel()
    // {
        
    // }

}
