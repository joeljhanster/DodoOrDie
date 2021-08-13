using UnityEngine;
using System.Collections;
using System;

public class Frog : MonoBehaviour {
    private Animator dodoAnimator;
    private GameObject Player;

    private Rigidbody2D dodoBody;

    // Jump Speed
    public float speed = 0.1f;
    private float moveLeft;
    private float moveRight;
    private float moveUp;
    private float moveDown;
    // Current jump
    Vector2 jump = Vector2.zero;

    private PlayerControls controls;
    public Vector3 vector3;
    public UpdateScore other;

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

    void  Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        dodoBody = GetComponent<Rigidbody2D>();
        dodoAnimator = GetComponent<Animator>();
        Player = GameObject.Find("Player");
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Is the Frog currently jumping?
    public bool isJumping() {
        return jump != Vector2.zero;
    }
    
    // FixedUpdate for Physics Stuff
    void FixedUpdate () {
        if (Player.transform.position.y > 5.5 ||  Player.transform.position.y < -5.5){
            Player.transform.position = vector3;
            other.GetComponent<UpdateScore>().ifdie();
        }
        // Currently jumping?
        if (isJumping())
        {
            // Remember current position
            Vector2 pos = transform.position;
            
            // Jump a bit further
            transform.position = Vector2.MoveTowards(pos, pos+jump, speed);
            
            // Subtract stepsize from jumpvector
            jump -= (Vector2)transform.position-pos;
        }
        // Otherwise allow for next jump
        else
        {
            if (moveUp > 0)
                jump = Vector2.up;
            else if (moveRight > 0)
                jump = Vector2.right;
            else if (moveDown > 0)
                jump = -Vector2.up; // -up means down
            else if (moveLeft > 0)
                jump = -Vector2.right; // -right means left
        } 
    }
    
    // void OnCollisionEnter2D(Collision2D coll) {
    //     // Game Over
    //     Destroy(gameObject);
    // }

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
