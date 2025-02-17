using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
   
    public void OpenScene(string sceneName)
    {
        // Ensure the new scene replaces the current one
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void OpenSceneByIndex(int sceneIndex)
    {
        // Ensure the new scene replaces the current one
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
}
