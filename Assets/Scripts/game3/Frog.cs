using UnityEngine;
using System.Collections;
using System;

public class Frog : MonoBehaviour {
    private Animator dodoAnimator;
    // Jump Speed
    public float speed = 0.1f;
    private float moveLeft;
    private float moveRight;
    private float moveUp;
    private float moveDown;
    // Current jump
    Vector2 jump = Vector2.zero;

    // Is the Frog currently jumping?
    public bool isJumping() {
        return jump != Vector2.zero;
    }
    
    // FixedUpdate for Physics Stuff
    void FixedUpdate () {
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
            if (Input.GetKey(KeyCode.UpArrow))
                jump = Vector2.up;
            else if (Input.GetKey(KeyCode.RightArrow))
                jump = Vector2.right;
            else if (Input.GetKey(KeyCode.DownArrow))
                jump = -Vector2.up; // -up means down
            else if (Input.GetKey(KeyCode.LeftArrow))
                jump = -Vector2.right; // -right means left
        }
        
        // Animation Parameters
        GetComponent<Animator>().SetFloat("X", jump.x);
        GetComponent<Animator>().SetFloat("Y", jump.y);
        GetComponent<Animator>().speed = Convert.ToSingle(isJumping());

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
    
    void OnCollisionEnter2D(Collision2D coll) {
        // Game Over
        Destroy(gameObject);
    }
}
