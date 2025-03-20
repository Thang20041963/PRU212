using UnityEngine;

public class GameThemeSong : MonoBehaviour
{
   
    private static GameThemeSong instance;
    public AudioSource AudioSource;
    private float soundSetting;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }
   
    private void Update()
    {
        soundSetting = PlayerPrefs.GetInt("SoundSetting", 100) / 100f;
        AudioSource.volume = soundSetting;
    }


}
