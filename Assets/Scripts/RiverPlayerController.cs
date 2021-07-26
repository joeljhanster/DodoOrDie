using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class RiverPlayerController : MonoBehaviour
{

    public float rockEffect = 10;

    private Rigidbody2D dodoBody;
    private Animator dodoAnimator;

    public float maxSpeed;
    public float speed;

    private float moveLeft;
    private float moveRight;
    private float moveUp;
    private float moveDown;

    private PlayerControls controls;
    
    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.MoveLeft.performed += ctx => moveLeft = ctx.ReadValue<float>();
        controls.Gameplay.MoveLeft.canceled += ctx => moveLeft = 0.0f;
        
        controls.Gameplay.MoveRight.performed += ctx => moveRight = ctx.ReadValue<float>();
        controls.Gameplay.MoveRight.canceled += ctx => moveRight = 0.0f;

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
        dodoBody = GetComponent<Rigidbody2D>();
        dodoAnimator = GetComponent<Animator>();
    }


    // FixedUpdate may be called once per frame. See documentation for details.
    void FixedUpdate(){
        Vector2 direction = new Vector2(moveRight - moveLeft, moveUp - moveDown);
        dodoBody.AddForce(direction * speed); 
    }

    // void OnTriggerEnter2D(Collision2D col)
    // {
    //     if (col.gameObject.CompareTag("Eagle"))
    //     {
    //         Debug.Log("Player eaten by eagle!");
    //     }
    // }

    // void OnCollisionEnter2D(Collision2D col)
    // {
    //     if (col.gameObject.CompareTag("Rock"))
    //     {
    //         dodoBody.AddForce(Vector2.up * rockEffect, ForceMode2D.Impulse);
    //     }

    // }


    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(moveRight - moveLeft, moveUp - moveDown);
        Debug.Log(direction);
        // dodoBody.MovePosition(dodoBody.position + speed * direction * Time.fixedDeltaTime);
        if (moveRight > 0) {
            dodoAnimator.SetBool("moveRight", true);
            dodoAnimator.SetBool("moveLeft", false);
        } else if (moveLeft > 0) {
            dodoAnimator.SetBool("moveRight", false);
            dodoAnimator.SetBool("moveLeft", true);
        } else if (moveUp > 0) {
            dodoAnimator.SetBool("moveRight", false);
            dodoAnimator.SetBool("moveLeft", false);
            dodoAnimator.SetBool("moveUp", true);
        } else {
            dodoAnimator.SetBool("moveRight", false);
            dodoAnimator.SetBool("moveLeft", false);
            dodoAnimator.SetBool("moveUp", false);
        }
    }
}


