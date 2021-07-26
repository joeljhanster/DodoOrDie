using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private Rigidbody2D snakeBody;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate =  30;
        snakeBody = GetComponent<Rigidbody2D>();
        
    }
     void FixedUpdate(){
        Vector2 direction = new Vector2(-speed, 0);
        snakeBody.AddForce(direction * speed); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
