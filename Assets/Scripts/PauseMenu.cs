using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public UnityEvent onRestartLevel;
    // private float moveUp;
    // private float moveDown;
    
    // private PlayerControls controls;

    // void Awake()
    // {
    //     controls = new PlayerControls();

    //     controls.Gameplay.MoveUp.performed += ctx => moveUp = ctx.ReadValue<float>();
    //     controls.Gameplay.MoveUp.canceled += ctx => moveUp = 0.0f;

    //     controls.Gameplay.MoveDown.performed += ctx => moveDown = ctx.ReadValue<float>();
    //     controls.Gameplay.MoveDown.canceled += ctx => moveDown = 0.0f;
    // }

    // void OnEnable()
    // {
    //     controls.Gameplay.Enable();
    // }

    // void OnDisable()
    // {
    //     controls.Gameplay.Disable();
    // }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (moveUp>0){
    //         Debug.Log("pressed up");
    //     }
    //     else if (moveDown>0){
    //        Debug.Log("pressed down");
            
    //     }
    // }

    public void ResumeGame()
    {
        Debug.Log("Resume game");
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
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
