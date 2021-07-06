using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float rockEffect = 10;

    private Rigidbody2D marioBody;

    void  Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        marioBody = GetComponent<Rigidbody2D>();
    }


    // FixedUpdate may be called once per frame. See documentation for details.
    void FixedUpdate(){   
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Rock"))
        {
            marioBody.AddForce(Vector2.up * rockEffect, ForceMode2D.Impulse);
        }

    }


    void Update(){
    }
}
