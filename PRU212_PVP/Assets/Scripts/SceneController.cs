using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
   
    public void OpenScene(string sceneName)
    {
        // Ensure the new scene replaces the current one
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    /// <summary>
    /// Closes the current scene and opens a new one by its build index.
    /// </summary>
    /// <param name="sceneIndex">The index of the scene to open.</param>
    public void OpenSceneByIndex(int sceneIndex)
    {
        // Ensure the new scene replaces the current one
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
}
