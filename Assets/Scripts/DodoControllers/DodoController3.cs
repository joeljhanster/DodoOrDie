using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class DodoController3 : MonoBehaviour
{
    public GameConstants gameConstants;
    public InputActionAsset playerControlsActions;

    public UnityEvent onPlayerEaten;

    public DodoCharacter dodoCharacter;
    public GameObject pauseMenu;
    private PlayerControls controls;

    private Animator dodoAnimator;
    private Rigidbody2D dodoBody;

    public AudioSource failAudio;

    private float moveLeft;
    private float moveRight;
    private float moveUp;
    private float moveDown;

    Vector2 jump = Vector2.zero;

    private Vector3 originalPosition;

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
        dodoBody = GetComponent<Rigidbody2D>();
        dodoAnimator = GetComponent<Animator>();

        dodoCharacter.SetLives(gameConstants.startingLives);

        if (dodoCharacter.taken) {
            // Switch controls
            dodoCharacter.Input.enabled = false;
            dodoCharacter.Input.actions = playerControlsActions;
            dodoCharacter.Input.enabled = true;
            dodoCharacter.Input.SwitchCurrentActionMap("Gameplay");
            dodoCharacter.Input.onActionTriggered += Input_onActionTriggered;
            originalPosition = this.gameObject.transform.localPosition;
        } else {
            this.gameObject.SetActive(false);
        }
    }

    public void resetPosition()
    {
        
        if (this.gameObject.transform.localPosition == originalPosition) {
            return;
        }
        else {
            // Player dies
            // Player loses 1 life
            dodoCharacter.AddLives(-1);
            onPlayerEaten.Invoke();
            failAudio.Play();

            if (dodoCharacter.lives > 0) {
                this.gameObject.transform.localPosition = originalPosition;
            }
            else {
                // Dodo is dead and cannot respawn
                Debug.Log("Dodo is dead");
                this.gameObject.SetActive(false);
            }
            
        }
    }

    public bool isJumping() {
        return jump != Vector2.zero;
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
        if (Mathf.Abs(this.gameObject.transform.localPosition.y) > 5.5) {
            this.gameObject.transform.localPosition = new Vector3(-8.5f, -1.0f, 0.0f);
        }
        // Currently jumping?
        if (isJumping())
        {
            // Remember current position
            Vector2 pos = transform.position;
            
            // Jump a bit further
            transform.position = Vector2.MoveTowards(pos, pos+jump, gameConstants.speedRiver);

            // Subtracts stepsize from jumpvector
            jump -= (Vector2) transform.position - pos;
        }
        // Otherwise allow for next jump
        else
        {
            if (moveUp > 0)
                jump = Vector2.up;
            else if (moveRight > 0)
                jump = Vector2.right;
            else if (moveDown > 0)
                jump = Vector2.down;
            else if (moveLeft > 0)
                jump = Vector2.left;
        }
    }

    // Update is called once per frame
    void Update()
    {
        setAnimation();
    }
}
