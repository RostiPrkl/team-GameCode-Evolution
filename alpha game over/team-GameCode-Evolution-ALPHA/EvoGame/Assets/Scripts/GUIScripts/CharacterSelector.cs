using UnityEngine;
using Gaskellgames.AudioController;


public class CharacterSelector : MonoBehaviour
{
    public static CharacterSelector instance;
    public PlayerScriptableObject playerData;

    //public SoundManager sndMngr;


    void Awake()
    {

       // sndMngr = FindObjectOfType<SoundManager>();
        //sndMngr.PlayMusic("menu2");

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
