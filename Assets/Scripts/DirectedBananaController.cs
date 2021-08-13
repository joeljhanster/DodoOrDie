using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DirectedBananaController : MonoBehaviour
{
    public UnityEvent onPlayerEaten;
    public DodoCharacter flowerDodo;
    public DodoCharacter goldenDodo;
    public DodoCharacter pirateDodo;
    public DodoCharacter rgbDodo;
    // private float originalX;
    // private float originalY;
    // private float playerX;
    // private float playerY;
    private Rigidbody2D bananaBody;
    private AudioSource bananaThrowingAudio;
    private Vector2 velocity;
    // public List<GameObject> players; 

    // Start is called before the first frame update
    void Start()
    {
        bananaBody = GetComponent<Rigidbody2D>();
        bananaThrowingAudio = GetComponent<AudioSource>();
        // originalX = transform.position.x;
        // originalY = transform.position.y;
        // playerX = player.transform.position.x;
        // playerY = player.transform.position.y;
        ComputeVelocity();
        bananaThrowingAudio.Play();
    }

    // void ComputerDirection()
    // {
    //     GameObject targetPlayer = null;
    //     foreach(GameObject dodo in players) {
    //         if (!targetPlayer) {
    //             targetPlayer = dodo;
    //         }
    //         if (dodo.activeSelf) {
    //             float distance = 
    //         }
    //     }
    // }

    void ComputeVelocity()
    {
        velocity = new Vector2(0, - 50);
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