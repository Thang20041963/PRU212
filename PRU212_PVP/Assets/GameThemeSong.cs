using UnityEngine;

public class GameThemeSong : MonoBehaviour
{
   
    private static GameThemeSong instance;

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

}
