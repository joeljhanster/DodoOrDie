using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectedBananaController : MonoBehaviour
{
    private float originalX;
    private float originalY;
    private float playerX;
    private float playerY;
    private Rigidbody2D bananaBody;
    private AudioSource bananaThrowingAudio;
    private Vector2 velocity;
    public  GameObject player; 

    // Start is called before the first frame update
    void Start()
    {
        bananaBody = GetComponent<Rigidbody2D>();
        bananaThrowingAudio = GetComponent<AudioSource>();
        originalX = transform.position.x;
        originalY = transform.position.y;
        playerX = player.transform.position.x;
        playerY = player.transform.position.y;
        ComputeVelocity();
        bananaThrowingAudio.Play();
    }

    void ComputeVelocity()
    {
        velocity = new Vector2(- originalX + playerX, originalY - playerY + 3.5f);

    }

    // Update is called once per frame
    void Update()
    {

        bananaBody.MovePosition(bananaBody.position + velocity * Time.fixedDeltaTime);
        
    }

    void OnBecameInvisible(){
        Debug.Log("Banana Destroyed!");
	    Destroy(gameObject);	
    }

    void  OnTriggerEnter2D(Collider2D other){
		// check if it collides with player
		if (other.gameObject.tag  ==  "Player"){
            Debug.Log("Collided with Player");
            Debug.Log("Kill Player");
            // Kill Player
            CentralManager.centralManagerInstance.killPlayer();
			
	    }
    }
}