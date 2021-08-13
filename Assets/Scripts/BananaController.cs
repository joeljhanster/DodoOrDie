using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BananaController : MonoBehaviour
{
    public UnityEvent onPlayerEaten;
    public DodoCharacter flowerDodo;
    public DodoCharacter goldenDodo;
    public DodoCharacter pirateDodo;
    public DodoCharacter rgbDodo;
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
		// if (other.gameObject.tag  ==  "Player"){
        //     Debug.Log("Collided with Player");
        //     Debug.Log("Kill Player");
        //     // Kill Player
        //     CentralManager.centralManagerInstance.killPlayer();
			
	    // }

        if (other.gameObject.CompareTag("FlowerDodo")) {
            if (!other.gameObject.GetComponent<DodoController2>().getImmunity()) {
                Debug.Log("Banana killed flower dodo");
                flowerDodo.AddLives(-1);
                onPlayerEaten.Invoke();
                other.gameObject.GetComponent<DodoController2>().PlayerDiesSequence();
                OnBecameInvisible();
            }
        }
        else if (other.gameObject.CompareTag("GoldenDodo")) {
            if (!other.gameObject.GetComponent<DodoController2>().getImmunity()) {
                Debug.Log("Banana killed golden dodo");
                goldenDodo.AddLives(-1);
                onPlayerEaten.Invoke();
                other.gameObject.GetComponent<DodoController2>().PlayerDiesSequence();
                OnBecameInvisible();
            }
        }
        else if (other.gameObject.CompareTag("PirateDodo")) {
            if (!other.gameObject.GetComponent<DodoController2>().getImmunity()) {
                Debug.Log("Banana killed pirate dodo");
                pirateDodo.AddLives(-1);
                onPlayerEaten.Invoke();
                other.gameObject.GetComponent<DodoController2>().PlayerDiesSequence();
                OnBecameInvisible();
            }
        }
        else if (other.gameObject.CompareTag("RGBDodo")) {
            if (!other.gameObject.GetComponent<DodoController2>().getImmunity()) {
                Debug.Log("Banana killed rgb dodo");
                rgbDodo.AddLives(-1);
                onPlayerEaten.Invoke();
                other.gameObject.GetComponent<DodoController2>().PlayerDiesSequence();
                OnBecameInvisible();
            }
        }
    }
}