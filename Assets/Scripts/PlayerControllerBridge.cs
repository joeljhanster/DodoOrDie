using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerControllerBridge : MonoBehaviour
{
    public GameConstants gameConstants;

    public UnityEvent onPlayerEaten;

    public UnityEvent onPlayerSurvives;

    public IntVariable dodoLives;
    public Text countdownText;
    public Text timer;
    public AudioSource countdownAudio;

    private PlayerControls controls;    // Assign to player?

    private SpriteRenderer dodoSprite;
    private Rigidbody2D dodoBody;
    private BoxCollider2D dodoBox;
    private Animator dodoAnimator;
    private AudioSource dodoAudio;

    private float viewportHalfWidthX;
    private float viewportHalfHeightY;

    private Vector3 bottomLeft;

    private float moveLeft;
    private float moveRight;
    private float moveUp;
    private float moveDown;
    private float jump;

    private float originalGravity;
    private bool eaten = false;
    private bool immune = false;
    private bool survived = false;
    private int timeLeft;
    private string minutes;
    private string seconds;
    private Transform eagle;
    
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
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }


    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        dodoSprite = GetComponent<SpriteRenderer>();
        dodoBody = GetComponent<Rigidbody2D>();
        dodoBox = GetComponent<BoxCollider2D>();
        dodoAnimator = GetComponent<Animator>();
        dodoAudio = GetComponent<AudioSource>();

        dodoLives.SetValue(gameConstants.startingLives);

        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        
        viewportHalfWidthX = Mathf.Abs(bottomLeft.x - Camera.main.transform.position.x);
        viewportHalfHeightY = Mathf.Abs(bottomLeft.y - Camera.main.transform.position.y);

        originalGravity = dodoBody.gravityScale;
        timeLeft = gameConstants.gameDuration;

        minutes = Mathf.Floor(timeLeft / 60).ToString("00");
        seconds = Mathf.RoundToInt(timeLeft % 60).ToString("00");
        timer.text = minutes + ":" + seconds;

        StartCoroutine("countdown");
        StartCoroutine("enableImmunity");
    }

    IEnumerator countdown()
    {
        countdownText.text = "3";
        countdownAudio.Play();
        yield return new WaitForSeconds(0.7f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1.0f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1.0f);
        countdownText.text = "Escape!";
        StartCoroutine("startTimer");
        yield return new WaitForSeconds(1.0f);
        countdownText.text = string.Empty;
    }

    // FixedUpdate may be called once per frame. See documentation for details.
    void FixedUpdate(){
        if (moveDown>0){
            transform.position = new Vector2(transform.position.x, transform.position.y-2f);
        }
        if (moveUp>0){
            transform.position = new Vector2(transform.position.x, transform.position.y+2f);
        }
        Vector2 direction = new Vector2(moveRight - moveLeft, 0);
        dodoBody.AddForce(direction * gameConstants.speed); 
    }

    IEnumerator enableImmunity()
    {
        immune = true;
        if (!survived) {
            dodoBody.gravityScale = 0.0f;
        }

        for (int i=0; i < gameConstants.immunityDuration; i++) {
            dodoSprite.enabled = false;
            yield return new WaitForSeconds(.2f);
            dodoSprite.enabled = true;
            yield return new WaitForSeconds(.3f);
            dodoSprite.enabled = false;
            yield return new WaitForSeconds(.2f);
            dodoSprite.enabled = true;
            yield return new WaitForSeconds(.3f);
        }

        immune = false;
        if (!survived) {
            dodoBody.gravityScale = originalGravity;
        }
    }

    IEnumerator respawnPlayer()
    {
        yield return new WaitForSeconds(4.0f);

        if (dodoLives.Value > 0) {
            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - dodoBox.size.y, 0.0f);
            Debug.Log("Setting eaten to false");
            eaten = false;
            dodoBox.enabled = true;
            StartCoroutine("enableImmunity");
        } else {
            // Dodo is dead and cannot respawn
            Debug.Log("Dodo is dead");
        }
    }

    IEnumerator playerFalls()
    {
        dodoBody.gravityScale = 0.0f;
        // Wait 5 seconds to show scene transition
        yield return new WaitForSeconds(5.0f);
        
        for (int i=0; i<3; i++) {
            dodoBody.gravityScale += 0.5f;
            yield return new WaitForSeconds(1.0f);
        }
    }

    void playerEaten()
    {
        Debug.Log("Player eaten by eagle!");
        dodoAudio.Play();
        eaten = true;
        onPlayerEaten.Invoke();
        dodoBox.enabled = false;
        StartCoroutine("respawnPlayer");
    }

    // Update is called once per frame
    void Update()
    {   
        Vector2 direction = new Vector2(moveRight - moveLeft, moveUp - moveDown);
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

    public void playerSurvivesResponse()
    {
        Debug.Log("Player survives");
        survived = true;
        StartCoroutine("playerFalls");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Eagle") && !eaten && !immune && !survived)
        {
            playerEaten();
            // Disable all movement, position at eagle's mouth
            eagle = col.gameObject.transform;
        }

        if (col.gameObject.CompareTag("Snake"))
        {
            Debug.Log("Player killed by snake");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Rock") && !immune & !survived)
        {
            dodoBody.AddForce(Vector2.up * gameConstants.rockEffect, ForceMode2D.Impulse);
        }
    }
}
