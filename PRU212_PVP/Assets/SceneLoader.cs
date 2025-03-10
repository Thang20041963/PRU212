using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public Animator transistion ;
    public float transistionTime = 1f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            LoadNextScene();
        }
    }
    public void LoadNextScene()
    {
        StartCoroutine(LoadScene("MainMenuScene"));
        
    }


    IEnumerator LoadScene(string scene)
    {
        transistion.SetTrigger("fade");

        yield return new WaitForSeconds(transistionTime);

        SceneManager.LoadScene(scene);
    }
}
