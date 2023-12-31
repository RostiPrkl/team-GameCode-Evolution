using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    public static CharacterSelector instance;
    public PlayerScriptableObject playerData;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }


    public static PlayerScriptableObject GetData()
    {
        return instance.playerData;
    }


    public void SelectCharacter(PlayerScriptableObject character)
    {
        playerData = character;
    }


    public void DestroySingleton()
    {
        instance = null;
        Destroy(gameObject);
    }
}
