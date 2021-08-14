using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneEV : MonoBehaviour
{
    public GameConstants gameConstants;
    public List<DodoCharacter> dodoCharacters;

    public void RestartLevel()
    {
        string currScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currScene);
        foreach(DodoCharacter dodo in dodoCharacters) {
            dodo.SetScore(dodo.startScore);
            dodo.SetLives(gameConstants.startingLives);
        }
    }

    public void ChangeScene()
    {
        string currScene = SceneManager.GetActiveScene().name;
        string nextScene;
        Debug.Log(currScene);
        if (currScene == gameConstants.menuScene) {
            nextScene = gameConstants.selectScene;
        } else if (currScene == gameConstants.selectScene) {
            nextScene = gameConstants.cliffScene;
            // nextScene = gameConstants.forestScene;
            // nextScene = gameConstants.riverScene;
            // nextScene = gameConstants.beachScene;
            
            // Reset score
            foreach(DodoCharacter dodo in dodoCharacters)
            {
                dodo.SetScore(0);
                dodo.SetStartScore(0);
            }
        } else if (currScene == gameConstants.cliffScene) {
            // nextScene = gameConstants.forestScene;
            nextScene = gameConstants.cutScene12;

            // Set starting score for level
            foreach(DodoCharacter dodo in dodoCharacters)
            {
                dodo.SetStartScore(dodo.score);
            }
        } else if (currScene == gameConstants.forestScene) {
            // nextScene = gameConstants.riverScene;
            nextScene = gameConstants.cutScene23;
            
            // Set starting score for level
            foreach(DodoCharacter dodo in dodoCharacters)
            {
                dodo.SetStartScore(dodo.score);
            }
        } else if (currScene == gameConstants.riverScene) {
            // nextScene = gameConstants.beachScene;
            nextScene = gameConstants.cutScene34;

            // Set starting score for level
            foreach(DodoCharacter dodo in dodoCharacters)
            {
                dodo.SetStartScore(dodo.score);
            }
        } else if (currScene == gameConstants.beachScene) {
            // nextScene = currScene;
            nextScene = gameConstants.cutSceneFinal;
        } else {
            nextScene = currScene;
        }

        // Reset lives
        foreach(DodoCharacter dodo in dodoCharacters)
        {
            if (dodo.taken) {
                dodo.SetLives(gameConstants.startingLives);
            } else {
                dodo.SetLives(0);
            }
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
