using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : GenericSingletonClass<SceneLoader>
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}