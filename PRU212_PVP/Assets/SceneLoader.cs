using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public Animator transistion ;
    public float transistionTime = 1f;
    
    
    public void LoadNextScene(string scene)
    {
        Debug.Log(scene);

        if(scene == "randomMode")
        {
            PlayerPrefs.SetInt("isRandomMode", 1);
            scene = "CharacterSelection";
        }
        if (scene == "normalMode")
        {
            PlayerPrefs.SetInt("isRandomMode", 0);
            scene = "CharacterSelection";
        }
       
        StartCoroutine(LoadScene(scene));
        
    }


    IEnumerator LoadScene(string scene)
    {
        Debug.Log("heheh");
        Time.timeScale = 1f;
        transistion.SetTrigger("fade");

        yield return new WaitForSeconds(transistionTime);

        SceneManager.LoadScene(scene);
    }
}
