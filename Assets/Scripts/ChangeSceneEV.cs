using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneEV : MonoBehaviour
{
    public GameConstants gameConstants;

    public void ChangeScene()
    {
        string currScene = SceneManager.GetActiveScene().name;
        string nextScene;
        Debug.Log(currScene);
        
        if (currScene == gameConstants.cliffScene) {
            nextScene = gameConstants.forestScene;
        } else if (currScene == gameConstants.forestScene) {
            nextScene = gameConstants.riverScene;
        } else if (currScene == gameConstants.riverScene) {
            nextScene = gameConstants.bridgeScene;
        } else if (currScene == gameConstants.bridgeScene) {
            nextScene = gameConstants.beachScene;
        } else {
            nextScene = currScene;
        }

        StartCoroutine(LoadScene(nextScene));
    }

    IEnumerator LoadScene(string sceneName)
    {
        Debug.Log("Changing scene: " + sceneName);
        yield return new WaitForSeconds(3.0f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
