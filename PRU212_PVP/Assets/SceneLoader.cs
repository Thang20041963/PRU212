using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public Animator transistion ;
    public float transistionTime = 1f;
    
    
    public void LoadNextScene(string scene)
    {
        StartCoroutine(LoadScene(scene));
        
    }


    IEnumerator LoadScene(string scene)
    {
        transistion.SetTrigger("fade");

        yield return new WaitForSeconds(transistionTime);

        SceneManager.LoadScene(scene);
    }
}
