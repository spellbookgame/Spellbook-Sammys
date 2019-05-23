using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneHandler : MonoBehaviour
{
    private AsyncOperation asyncLoad;

    void Start()
    {
        StartCoroutine(AsyncLoad());   
    }

    /*IEnumerator AsyncLoad()
    {
        yield return null;

        asyncLoad = SceneManager.LoadSceneAsync(LoadHandler.instance.sceneBuildIndex, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;

        Debug.Log("Setup complete: " + LoadHandler.instance.setupComplete);

        while(asyncLoad.progress < 0.9f)
        {
            if (LoadHandler.instance.setupComplete)
            {
                asyncLoad.allowSceneActivation = true;
                LoadHandler.instance.setupComplete = false;
            }

            yield return null;
        }

        // SceneManager.UnloadSceneAsync(LoadHandler.instance.loadSceneIndex);
    }*/

    IEnumerator AsyncLoad()
    {
        float timer = 0f;
        float minTime = 1.5f;

        asyncLoad = SceneManager.LoadSceneAsync(LoadHandler.instance.sceneBuildIndex);
        asyncLoad.allowSceneActivation = false;

        while(!asyncLoad.isDone)
        {
            timer += Time.deltaTime;

            if(timer > minTime)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }

        yield return null;
    }
}
