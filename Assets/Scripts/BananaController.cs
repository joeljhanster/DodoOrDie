using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaController : MonoBehaviour
{
    private float originalX;
    private Rigidbody2D bananaBody;
    private AudioSource bananaThrowingAudio;
    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        bananaBody = GetComponent<Rigidbody2D>();
        bananaThrowingAudio = GetComponent<AudioSource>();
        // GetComponent<Collider2D>().enabled = true;
        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();
        // velocity = new Vector2((moveRight)*maxOffset / mushroomMovementTime, 0);
        // float force = (float)Random.Range(-1000,1000);
        // bananaBody.AddForce(direction * force, ForceMode2D.Impulse);
        bananaThrowingAudio.Play();
        // int rand = Random.Range(0, 1000);
        // Debug.Log("Random output for mushroom direction:" + rand.ToString());
        // if (rand % 2 == 0){
        //     moveRight = -1;
        // }
        // else{moveRight = 1;}
    }

    void ComputeVelocity()
    {
        // velocity = new Vector2((float)Random.Range(-1000,1000), (float)Random.Range(-1000,1000));
        velocity = new Vector2((float)Random.Range(-10,10), (float)Random.Range(-10,10));

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