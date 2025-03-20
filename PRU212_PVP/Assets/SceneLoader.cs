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
        else
        {
            PlayerPrefs.SetInt("isRandomMode", 0);
        }
       
        StartCoroutine(LoadScene(scene));
        
    }


    IEnumerator LoadScene(string scene)
    {
        Debug.Log("heheh");
        transistion.SetTrigger("fade");

        yield return new WaitForSeconds(transistionTime);

        SceneManager.LoadScene(scene);
    }
}
