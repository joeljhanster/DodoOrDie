using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    private float moveLeft;
    private float moveRight;
    private float moveUp;
    private float moveDown;
    public GameConstants gameConstants;

    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.MoveUp.performed += ctx => moveUp = ctx.ReadValue<float>();
        controls.Gameplay.MoveUp.canceled += ctx => moveUp = 0.0f;

        controls.Gameplay.MoveDown.performed += ctx => moveDown = ctx.ReadValue<float>();
        controls.Gameplay.MoveDown.canceled += ctx => moveDown = 0.0f;
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
    void  Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
    }
    void Update()
    {
        if (moveUp>0){
            Debug.Log("pressed up");
        }
        else if (moveDown>0){
           Debug.Log("pressed down");
            
        }
    }


   public void PlayGame(){
       SceneManager.LoadScene(gameConstants.cliffScene);
   }
   public void QuitGame(){
       Debug.Log("Quit game");
       Application.Quit();
   }
}
