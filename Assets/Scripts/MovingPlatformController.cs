using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{

    private float originalX;
    private float maxOffset = 2.0f;
    private float platformMovementTime = 0.75f;
    private int moveRight = -1;
    private Vector2 velocity;
    private Rigidbody2D platformBody;

    void Start()
    {
        platformBody = GetComponent<Rigidbody2D>();
        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();
    }
    void ComputeVelocity(){
        velocity = new Vector2((moveRight)*maxOffset / platformMovementTime, 0);
    }
    void MovePlatform(){
        platformBody.MovePosition(platformBody.position + velocity * Time.fixedDeltaTime);
    }


    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(platformBody.position.x - originalX) < maxOffset)
        {// move cloud
            MovePlatform();
        }
        else{
            // change direction
            moveRight *= -1;
            ComputeVelocity();
            MovePlatform();
        }
    }
}
