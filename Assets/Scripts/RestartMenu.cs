using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour
{
    public UnityEvent onRestartLevel;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    public void RestartLevel()
    {
        Debug.Log("Restart level");
        onRestartLevel.Invoke();
        Time.timeScale = 1;
        // this.gameObject.SetActive(false);
    }

    public void BackMainMenu()
    {
        Debug.Log("Back to main menu");
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
        // this.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
        Time.timeScale = 1;
        // this.gameObject.SetActive(false);
    }
}
