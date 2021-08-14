using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class DodoController4 : MonoBehaviour
{
    public GameConstants gameConstants;
    public InputActionAsset playerControlsActions;

    public UnityEvent onPlayerEaten;

    public DodoCharacter dodoCharacter;
    public GameObject pauseMenu;
    private PlayerControls controls;

    private Vector3 dodoOriginalPosition;
    private SpriteRenderer dodoSprite;
    private Rigidbody2D dodoBody;
    private BoxCollider2D dodoBox;
    private Animator dodoAnimator;
    private AudioSource dodoAudio;  // dodo_death
    public GameObject egg;

    private float viewportHalfWidthX;
    private float viewportHalfHeightY;

    private Vector3 bottomLeft;

    private float moveLeft;
    private float moveRight;
    private float moveUp;
    private float moveDown;
    private float action;   // plant egg bomb

    private bool immune = false;
    private bool eaten = false;
    private bool survived = false;

    private float ignoreInputTime = 0.0f;
    private float ignoreInputInterval = 0.3f;

    void Awake()
    {
        controls = new PlayerControls();
        // Comment out when multiplayer
        // controls.Gameplay.MoveLeft.performed += ctx => moveLeft = ctx.ReadValue<float>();
        // controls.Gameplay.MoveLeft.canceled += ctx => moveLeft = 0.0f;
        
        // controls.Gameplay.MoveRight.performed += ctx => moveRight = ctx.ReadValue<float>();
        // controls.Gameplay.MoveRight.canceled += ctx => moveRight = 0.0f;

        // controls.Gameplay.MoveUp.performed += ctx => moveUp = ctx.ReadValue<float>();
        // controls.Gameplay.MoveUp.canceled += ctx => moveUp = 0.0f;

        // controls.Gameplay.MoveDown.performed += ctx => moveDown = ctx.ReadValue<float>();
        // controls.Gameplay.MoveDown.canceled += ctx => moveDown = 0.0f;

        // controls.Gameplay.Action.performed += ctx => action = ctx.ReadValue<float>();
        // controls.Gameplay.Action.canceled += ctx => action = 0.0f;
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
        else if (obj.action.name == controls.Gameplay.Action.name)
        {
            action = obj.ReadValue<float>();
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

        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        
        viewportHalfWidthX = Mathf.Abs(bottomLeft.x - Camera.main.transform.position.x);
        viewportHalfHeightY = Mathf.Abs(bottomLeft.y - Camera.main.transform.position.y);

        if (dodoCharacter.taken) {
            // Switch controls
            dodoCharacter.Input.enabled = false;
            dodoCharacter.Input.actions = playerControlsActions;
            dodoCharacter.Input.enabled = true;
            dodoCharacter.Input.SwitchCurrentActionMap("Gameplay");
            dodoCharacter.Input.onActionTriggered += Input_onActionTriggered;
            StartCoroutine("enableImmunity");
            // originalPosition = this.gameObject.transform.localPosition;
        } else {
            this.gameObject.SetActive(false);
        }
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

    IEnumerator respawnPlayer()
    {
        yield return new WaitForSeconds(3.0f);

        if (dodoCharacter.lives > 0) {
            // Respawn
            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - dodoBox.size.y, 0.0f);
            eaten = false;
            dodoBox.enabled = true;
            StartCoroutine("enableImmunity");
        }
        else {
            // Dodo is dead and cannot respawn
            Debug.Log("Dodo is dead");
            this.gameObject.SetActive(false);
        }
    }

    public void playerSurvivesResponse()
    {
        Debug.Log("Player survives");
        survived = true;
    }

    public bool getImmuneStatus()
    {
        return immune || eaten;
    }

    void FixedUpdate()
    {
        if (!survived) {
            Vector2 direction = new Vector2(moveRight - moveLeft, moveUp - moveDown);
            if (dodoBody.velocity.magnitude < gameConstants.maxSpeedBeach) {
                dodoBody.AddForce(direction * gameConstants.speedBeach);
            }
            
            if (action > 0 && Time.time > ignoreInputTime) {
                GameObject eggBomb = (GameObject) Instantiate(egg, transform.position, Quaternion.identity);
                eggBomb.transform.localScale = new Vector3(0.07045084f, 0.07045084f, 0.07045084f);
                ignoreInputTime = Time.time + ignoreInputInterval;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PirateBeard") && !eaten && !immune && !survived)
        {
            playerEaten();
        }
    }

    void playerEaten()
    {
        Debug.Log("Player eaten by pirates!");
        dodoAudio.Play();
        eaten = true;
        dodoCharacter.AddLives(-1);
        onPlayerEaten.Invoke();

        dodoBox.enabled = false;
        StartCoroutine("respawnPlayer");
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

    // Update is called once per frame
    void Update()
    {
        if (survived) {
            // Do celebratory dance
            dodoAnimator.SetBool("moveRight", false);
            dodoAnimator.SetBool("moveLeft", false);
            dodoAnimator.SetBool("moveUp", false);
        } else if (eaten) {
            dodoBody.MovePosition(new Vector3(bottomLeft.x - viewportHalfWidthX, bottomLeft.y - viewportHalfHeightY, 0.0f));
        } else {
            // Control animation
            setAnimation();
        }
    }
}
