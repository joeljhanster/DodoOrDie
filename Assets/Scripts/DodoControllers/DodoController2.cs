using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class DodoController2 : MonoBehaviour
{

    public GameConstants gameConstants;
    public InputActionAsset playerControlsActions;

    public UnityEvent onPlayerEaten;
    public UnityEvent onPlayerSurvives;

    public DodoCharacter dodoCharacter;

    public GameObject pauseMenu;

    private PlayerControls controls;

    private SpriteRenderer dodoSprite;
    private Rigidbody2D dodoBody;
    private BoxCollider2D dodoBox;
    private Animator dodoAnimator;
    private AudioSource dodoAudio;

    public AudioClip dodo_jump;
    public AudioClip dodo_death;
    private Vector3 dodoOriginalPosition;

    private bool faceRightState = true;
    private bool onGroundState = true;


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
    // private bool survived = false;
    // private Transform eagle;

    void Awake()
    {
        controls = new PlayerControls();
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        Debug.Log(obj.action.name);
        if (obj.action.name == controls.Gameplay.MoveLeft.name)
        {
            moveLeft = obj.ReadValue<float>();
        }
        else if (obj.action.name == controls.Gameplay.MoveRight.name)
        {
            moveRight = obj.ReadValue<float>();
        }
        else if (obj.action.name == controls.Gameplay.MoveUp.name)
        {
            moveUp = obj.ReadValue<float>();
        }
        else if (obj.action.name == controls.Gameplay.MoveDown.name)
        {
            moveDown = obj.ReadValue<float>();
        }
        else if (obj.action.name == controls.Gameplay.Jump.name)
        {
            jump = obj.ReadValue<float>();
        }
        else if (obj.action.name == controls.Gameplay.Pause.name)
        {
            Debug.Log("Pausing game");
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        dodoSprite = GetComponent<SpriteRenderer>();
        dodoBody = GetComponent<Rigidbody2D>();
        dodoBox = GetComponent<BoxCollider2D>();
        dodoAnimator = GetComponent<Animator>();
        dodoAudio = GetComponent<AudioSource>();

        dodoCharacter.SetLives(gameConstants.startingLives);

        // bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        
        // viewportHalfWidthX = Mathf.Abs(bottomLeft.x - Camera.main.transform.position.x);
        // viewportHalfHeightY = Mathf.Abs(bottomLeft.y - Camera.main.transform.position.y);

        originalGravity = dodoBody.gravityScale;
        if (dodoCharacter.taken) {
            // Switch controls
            dodoCharacter.Input.enabled = false;
            dodoCharacter.Input.actions = playerControlsActions;
            dodoCharacter.Input.enabled = true;
            dodoCharacter.Input.SwitchCurrentActionMap("Gameplay");
            dodoCharacter.Input.onActionTriggered += Input_onActionTriggered;
            // StartCoroutine("enableImmunity");
        } else {
            this.gameObject.SetActive(false);
        }

        // GameManager.OnPlayerDeath += PlayerDiesSequence;
    }

    IEnumerator enableImmunity()
    {
        immune = true;
        
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
    }

    void setAnimation()
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
    void FixedUpdate()
    {
        Vector2 horizontalDirection = new Vector2(moveRight - moveLeft, 0);
        if (dodoBody.velocity.magnitude < gameConstants.maxSpeedForest) {
            dodoBody.AddForce(horizontalDirection * gameConstants.speedForest);
        }

        if (jump > 0 && onGroundState) {
            dodoAudio.PlayOneShot(dodo_jump);
            dodoBody.AddForce(Vector2.up * gameConstants.upForceForest, ForceMode2D.Impulse);
            onGroundState = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("EndLevel"))
        {
            dodoBody.bodyType = RigidbodyType2D.Static;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Collision with Ground!");
            onGroundState = true;   // back on ground
        }
    }

    // Update is called once per frame
    void Update()
    {
        setAnimation();
    }

    public void PlayerDiesSequence()
    {
        dodoOriginalPosition = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
        dodoAnimator.SetBool("isDead", true);
        dodoAudio.PlayOneShot(dodo_death);
        GetComponent<Collider2D>().enabled = false;
        dodoBody.AddForce(Vector2.up * gameConstants.dieUpForceForest, ForceMode2D.Impulse);
        dodoBody.gravityScale = 2;
        StartCoroutine("dead");
        // dodoBody.position = dodoOriginalPosition;
    }

    public bool getImmunity()
    {
        return immune;
    }

    IEnumerator dead()
    {
        yield return new WaitForSeconds(3.0f);
        // dodoBody.bodyType = RigidbodyType2D.Static;
        if (dodoCharacter.lives > 0) {
            dodoAnimator.SetBool("isDead", false);
            transform.position = dodoOriginalPosition;
            GetComponent<Collider2D>().enabled = true;
            dodoBody.gravityScale = 1;
            StartCoroutine("enableImmunity");
        } else {
            // Dodo is dead and cannot respawn
            Debug.Log("Dodo is dead");
            this.gameObject.SetActive(false);
        }
        
    }
}
