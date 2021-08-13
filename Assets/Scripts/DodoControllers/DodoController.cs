using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class DodoController : MonoBehaviour
{
    public GameConstants gameConstants;
    public InputActionAsset playerControlsActions;

    public UnityEvent onPlayerEaten;

    // public UnityEvent onPlayerSurvives;
    public DodoCharacter dodoCharacter;

    public GameObject pauseMenu;

    private PlayerControls controls;

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
    private Transform eagle;

    void Awake()
    {
        controls = new PlayerControls();
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
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
        // Screen.SetResolution(Screen.width, Screen.width / 16 * 9);
        dodoSprite = GetComponent<SpriteRenderer>();
        dodoBody = GetComponent<Rigidbody2D>();
        dodoBox = GetComponent<BoxCollider2D>();
        dodoAnimator = GetComponent<Animator>();
        dodoAudio = GetComponent<AudioSource>();

        dodoCharacter.SetLives(gameConstants.startingLives);

        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        
        viewportHalfWidthX = Mathf.Abs(bottomLeft.x - Camera.main.transform.position.x);
        viewportHalfHeightY = Mathf.Abs(bottomLeft.y - Camera.main.transform.position.y);

        originalGravity = dodoBody.gravityScale;
        if (dodoCharacter.taken) {
            // Switch controls
            dodoCharacter.Input.enabled = false;
            dodoCharacter.Input.actions = playerControlsActions;
            dodoCharacter.Input.enabled = true;
            dodoCharacter.Input.SwitchCurrentActionMap("Gameplay");
            dodoCharacter.Input.onActionTriggered += Input_onActionTriggered;
            StartCoroutine("enableImmunity");
        } else {
            this.gameObject.SetActive(false);
        }
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

        if (dodoCharacter.lives > 0) {
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

    public void playerSurvivesResponse()
    {
        Debug.Log("Player survives");
        survived = true;
        StartCoroutine("playerFalls");
    }

    // FixedUpdate may be called once per frame. See documentation for details.
    void FixedUpdate()
    {
        if (!survived) {
            Vector2 direction = new Vector2(moveRight - moveLeft, moveUp - moveDown);
            dodoBody.AddForce(direction * gameConstants.speed); 

            if (direction != Vector2.zero) {
                dodoBody.gravityScale = originalGravity;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Eagle") && !eaten && !immune && !survived)
        {
            playerEaten();
            // Disable all movement, position at eagle's mouth
            eagle = col.gameObject.transform;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Rock") && !immune & !survived)
        {
            dodoBody.AddForce(Vector2.up * gameConstants.rockEffect, ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (survived) {
            dodoAnimator.SetBool("moveRight", false);
            dodoAnimator.SetBool("moveLeft", false);
            dodoAnimator.SetBool("moveUp", false);
        } else if (eaten) {
            dodoBody.MovePosition(eagle ? eagle.position : new Vector3(bottomLeft.x - viewportHalfWidthX, bottomLeft.y - viewportHalfHeightY, 0.0f));
            if (Mathf.Abs(Camera.main.transform.position.x - dodoBody.position.x) > Mathf.Abs(viewportHalfWidthX - dodoBox.size.x)) {
                transform.position = new Vector3(bottomLeft.x - viewportHalfWidthX, bottomLeft.y - viewportHalfHeightY, 0.0f);
            }
        } else {
            // Control animation
            setAnimation();
            
            if (
                (Mathf.Abs(Camera.main.transform.position.y - dodoBody.position.y) > Mathf.Abs(viewportHalfHeightY)) ||
                (Mathf.Abs(Camera.main.transform.position.x - dodoBody.position.x) > Mathf.Abs(viewportHalfWidthX))
            ) {
                playerEaten();
            }
        }
    }
}
