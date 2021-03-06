using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerControllerMiniGame2 : MonoBehaviour
{

    public float rockEffect = 10;
    private Vector3 dodoOriginalPosition;
    private Rigidbody2D dodoBody;
    private Animator dodoAnimator;
    private Animator eggAnimator;
    private AudioSource dodoAudio;
    public AudioClip dodo_jump;
    public AudioClip dodo_death;
    public GameObject dodoImage;
    public GameObject egg;

    private bool faceRightState = true;
    private bool onGroundState = true;
    public float maxSpeed = 20;
    public float speed = 30;
    public float upForce = 20;

    private float moveLeft;
    private float moveRight;
    private float moveUp;
    private float moveDown;
    private float jump;
    private float action;

    public bool alive = true;

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

        controls.Gameplay.Jump.performed += ctx => jump = ctx.ReadValue<float>();
        controls.Gameplay.Jump.canceled += ctx => jump = 0.0f;

        controls.Gameplay.Action.performed += ctx => action = ctx.ReadValue<float>();
        controls.Gameplay.Action.canceled += ctx => action = 0.0f;
        
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
        eggAnimator = GetComponent<Animator>();
        dodoAudio = GetComponent<AudioSource>();
        GameManager.OnPlayerDeath += PlayerDiesSequence;
    }


    // FixedUpdate may be called once per frame. See documentation for details.
    void FixedUpdate(){

        // dynamic rigidbody
        // float moveHorizontal = Input.GetAxis("Horizontal");
        // Debug.Log("moveRight: "+ moveRight);
        // Debug.Log("moveLeft: "+ moveLeft);
        // Debug.Log(moveRight - moveLeft);
        Vector2 horizontalDirection = new Vector2(moveRight - moveLeft, 0);
        if (dodoBody.velocity.magnitude < maxSpeed){
            dodoBody.AddForce(horizontalDirection * speed); 
        }
        
        if(dodoBody.velocity.magnitude == 0){
            dodoAnimator.SetBool("moveRight", false);
            dodoAnimator.SetBool("moveLeft", false);
            dodoAnimator.SetBool("moveUp", false);
        }
        if ((jump > 0) && onGroundState)
        {
            PlayJumpSound();
            dodoBody.AddForce(Vector2.up * upForce, ForceMode2D.Impulse);
            onGroundState = false;
        }
        
        if (action > 0){
            GameObject newObject = Instantiate(egg,transform.position,Quaternion.identity) as GameObject;
            newObject.transform.localScale = new Vector3(0.07045084f, 0.07045084f, 0.07045084f);

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Eagle"))
        {
            Debug.Log("Player eaten by eagle!");
        }
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Died");
            onGroundState = true; // back on ground
        }
        if (col.gameObject.CompareTag("PirateBeard"))
        {
            CentralManager.centralManagerInstance.killPlayer();
        }
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Rock"))
        {
            dodoBody.AddForce(Vector2.up * rockEffect, ForceMode2D.Impulse);
        }

        if (col.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Collision with Ground!");
            onGroundState = true; // back on ground
        }
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Died");
            onGroundState = true; // back on ground
        }
        
    }


    // Update is called once per frame
    void Update()
    {

        // toggle state
        if ((moveLeft > moveRight) && (moveLeft >  0)){
            // faceRightState = false;
            dodoAnimator.SetBool("moveRight", false);
            dodoAnimator.SetBool("moveLeft", true);
            dodoAnimator.SetBool("moveUp", false);
            // marioSprite.flipX = true;
        }

        if ((moveRight > moveLeft) && (moveRight >  0)){
            // faceRightState = true;
            dodoAnimator.SetBool("moveRight", true);
            dodoAnimator.SetBool("moveLeft", false);
            dodoAnimator.SetBool("moveUp", false);
            // marioSprite.flipX = false;
        }

    }

    void  PlayJumpSound(){
	    dodoAudio.PlayOneShot(dodo_jump);
    }

    void PlayerDiesSequence()
    {
        alive=false;
        if (alive ==false){
            dodoOriginalPosition = new Vector3(transform.position.x,transform.position.y,transform.position.z); 
            dodoAnimator.SetBool("isDead", true);
            dodoAudio.PlayOneShot(dodo_death);
            GetComponent<Collider2D>().enabled = false;
            dodoBody.AddForce(Vector2.up  *  30, ForceMode2D.Impulse);
            // dodoBody.gravityScale = 2;
            dodoImage.GetComponent<Renderer>().enabled = false;
            StartCoroutine(dead());
        }
        alive = true;
    }

    IEnumerator dead()
    {
        // yield return new WaitForSeconds(5.0f);
        // dodoBody.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(3.0f);
        // dodoBody.bodyType = RigidbodyType2D.Static;
        dodoAnimator.SetBool("isDead", false);
        transform.position = dodoOriginalPosition;
        GetComponent<Collider2D>().enabled = true;
        dodoImage.GetComponent<Renderer>().enabled = true;
    }
}
