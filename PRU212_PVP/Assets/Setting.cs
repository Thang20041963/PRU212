using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Setting : MonoBehaviour
{
    public TMP_Text soundtext ;
    public TMP_Text NoRtext ;
    public TMP_Text healthtext ;

    private int sound;
    private int health;
    private int nor;
    void Start()
    {
        sound = PlayerPrefs.GetInt("SoundSetting", 100);
        health = PlayerPrefs.GetInt("HealthSetting", 100);
        nor = PlayerPrefs.GetInt("NoRSetting", 1);
        healthtext.text = health.ToString();
        NoRtext.text = nor.ToString();
        soundtext.text = sound.ToString();
    }

    

    public void SoundSetting(int volume)
    {
        sound = sound + volume;
        if (sound <= 0)
        {
            sound = 0;
        }
        if (sound >= 100)
        {
            sound = 100;
        }
        PlayerPrefs.SetInt("SoundSetting", sound);
        soundtext.text = sound.ToString();
    }

    public void NoRSetting(int round)
    {
        nor = nor + round;
        if (nor <= 1)
        {
            nor = 1;
        }
        if (nor >= 5)
        {
            nor = 5;
        }
        PlayerPrefs.SetInt("NoRSetting", nor);
        NoRtext.text = nor.ToString();
    }

    public void HealthSetting(int blood)
    {
        health = health + blood;
        if (health <= 100)
        {
            health = 100;
        }
        if (health >= 500)
        {
            health = 500;
        }
        PlayerPrefs.SetInt("HealthSetting", health);
        healthtext.text = health.ToString();
    }

}
