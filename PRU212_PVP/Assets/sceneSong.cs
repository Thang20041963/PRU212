using UnityEngine;
using UnityEngine.Audio;

public class sceneSong : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource.Play();
    }
}
