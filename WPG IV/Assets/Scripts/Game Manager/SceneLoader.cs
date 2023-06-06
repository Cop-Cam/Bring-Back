using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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

    public IEnumerator LoadSceneAsyncWaitForSecondTimescaled(string sceneName, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadSceneAsync(sceneName);
    }
}