using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public GameConstants gameConstants;
    private PlayerControls controls;    // Assign to player?

    private SpriteRenderer dodoSprite;
    private Rigidbody2D dodoBody;
    private BoxCollider2D dodoBox;
    private Animator dodoAnimator;

    private float viewportHalfWidthX;
    private float viewportHalfHeightY;

    private Vector3 bottomLeft;

    private float moveLeft;
    private float moveRight;
    private float moveUp;
    private float moveDown;
    private bool eaten = false;
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
        dodoSprite = GetComponent<SpriteRenderer>();
        dodoBody = GetComponent<Rigidbody2D>();
        dodoBox = GetComponent<BoxCollider2D>();
        dodoAnimator = GetComponent<Animator>();

        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        
        viewportHalfWidthX = Mathf.Abs(bottomLeft.x - Camera.main.transform.position.x);
        viewportHalfHeightY = Mathf.Abs(bottomLeft.y - Camera.main.transform.position.y);
    }


    // FixedUpdate may be called once per frame. See documentation for details.
    void FixedUpdate(){
        Vector2 direction = new Vector2(moveRight - moveLeft, moveUp - moveDown);
        dodoBody.AddForce(direction * gameConstants.speed); 
    }

    IEnumerator RespawnPlayer() {
        yield return new WaitForSeconds(3.0f);
        transform.position = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, 0.0f);
        // dodoSprite.enabled = true;
        // dodoBox.enabled = true;
        eaten = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Eagle") && !eaten)
        {
            Debug.Log("Player eaten by eagle!");
            // Disable all movement, position at eagle's mouth
            eaten = true;
            eagle = col.gameObject.transform;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Rock"))
        {
            dodoBody.AddForce(Vector2.up * gameConstants.rockEffect, ForceMode2D.Impulse);
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (eaten) {
            dodoBody.MovePosition(eagle.position);
            if (Mathf.Abs(Camera.main.transform.position.x - dodoBody.position.x) > Mathf.Abs(viewportHalfWidthX - dodoBox.size.x)) {
                // dodoSprite.enabled = false;
                // dodoBox.enabled = false;
                transform.position = new Vector3(bottomLeft.x - viewportHalfWidthX, bottomLeft.y - viewportHalfHeightY, 0.0f);
                StartCoroutine("RespawnPlayer");
            }
        } else {
            Vector2 direction = new Vector2(moveRight - moveLeft, moveUp - moveDown);
            // Debug.Log(direction);
            // dodoBody.MovePosition(dodoBody.position + gameConstants.speed * direction * Time.fixedDeltaTime);
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
}
