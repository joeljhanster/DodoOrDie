using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySkyController : MonoBehaviour
{
    public UnityEvent onPlayerEaten;

    public DodoCharacter flowerDodo;
    public DodoCharacter goldenDodo;
    public DodoCharacter pirateDodo;
    public DodoCharacter rgbDodo;

	private  Rigidbody2D monkeyBody;
    private float groundSurface = -4.3f;
    private SpriteRenderer monkeySprite;
    private AudioSource monkeyScream;
    private bool monkeyDead = false;
    public  GameObject banana; 
    public float duration = 5f;
    private float timeLeft = 0.5f;
    private bool visible;
    // Start is called before the first frame update
    void Start()
    {
        monkeyBody  =  GetComponent<Rigidbody2D>();

        monkeySprite = GetComponent<SpriteRenderer>();

        monkeyScream = GetComponent<AudioSource>();
        
    }
    void OnBecameVisible(){
        visible = true;
    }   
    void OnBecameInvisible(){
        visible = false;	
    }


    // Update is called once per frame
    void Update()
    {
        if(visible){
        StartCoroutine(SpawnBananas());
        }
    }

    void  OnTriggerEnter2D(Collider2D other){
		// check if it collides with player
		// if (other.gameObject.tag  ==  "Player"){
        //     Debug.Log("Collided with Player");
		// 	// check if collides on top
		// 	float yoffset = (other.transform.position.y  -  this.transform.position.y);
        //     Debug.Log("y offset: " + yoffset);
		// 	if (yoffset  >  0.64f){
        //         Debug.Log("Kill Enemy");
		// 		KillSelf();
		// 	}
		// 	else{
        //         Debug.Log("Kill Player");
		// 		// Kill Player
        //         CentralManager.centralManagerInstance.killPlayer();

		// 	}
		// }

        if (other.gameObject.CompareTag("FlowerDodo")) {
            Debug.Log("Sky monkey collided with flower dodo");
            // check if collides on top
            float yoffset = (other.transform.position.y - this.transform.position.y);
            Debug.Log("y offset: " + yoffset);
            if (yoffset > 0.5f) {
                Debug.Log("Kill Enemy");
                KillSelf();
            }
            else if (!other.gameObject.GetComponent<DodoController2>().getImmunity()) {
                Debug.Log("Kill Player");
                flowerDodo.AddLives(-1);
                onPlayerEaten.Invoke();
                other.gameObject.GetComponent<DodoController2>().PlayerDiesSequence();
            }
        }
        else if (other.gameObject.CompareTag("GoldenDodo")) {
            Debug.Log("Sky monkey collided with golden dodo");
            // check if collides on top
            float yoffset = (other.transform.position.y - this.transform.position.y);
            Debug.Log("y offset: " + yoffset);
            if (yoffset > 0.5f) {
                Debug.Log("Kill Enemy");
                KillSelf();
            }
            else if (!other.gameObject.GetComponent<DodoController2>().getImmunity()) {
                Debug.Log("Kill Player");
                goldenDodo.AddLives(-1);
                onPlayerEaten.Invoke();
                other.gameObject.GetComponent<DodoController2>().PlayerDiesSequence();
            }
        }
        else if (other.gameObject.CompareTag("PirateDodo")) {
            Debug.Log("Sky monkey collided with pirate dodo");
            // check if collides on top
            float yoffset = (other.transform.position.y - this.transform.position.y);
            Debug.Log("y offset: " + yoffset);
            if (yoffset > 0.5f) {
                Debug.Log("Kill Enemy");
                KillSelf();
            }
            else if (!other.gameObject.GetComponent<DodoController2>().getImmunity()) {
                Debug.Log("Kill Player");
                pirateDodo.AddLives(-1);
                onPlayerEaten.Invoke();
                other.gameObject.GetComponent<DodoController2>().PlayerDiesSequence();
            }
        }
        else if (other.gameObject.CompareTag("RGBDodo")) {
            Debug.Log("Sky monkey collided with rgb dodo");
            // check if collides on top
            float yoffset = (other.transform.position.y - this.transform.position.y);
            Debug.Log("y offset: " + yoffset);
            if (yoffset > 0.5f) {
                Debug.Log("Kill Enemy");
                KillSelf();
            }
            else if (!other.gameObject.GetComponent<DodoController2>().getImmunity()) {
                Debug.Log("Kill Player");
                rgbDodo.AddLives(-1);
                onPlayerEaten.Invoke();
                other.gameObject.GetComponent<DodoController2>().PlayerDiesSequence();
            }
        }
	}
    
    void KillSelf(){
		// enemy dies
        monkeyDead = true;
        monkeyScream.Play();
		StartCoroutine("fall");
		Debug.Log("Kill sequence ends");
	}

    IEnumerator fall(){
		Debug.Log("fall starts");
		// int steps =  30;
		// float stepper =  1.0f/(float) steps;
        monkeyBody.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        monkeyBody.GetComponent<Collider2D>().enabled = false;
        monkeyBody.AddForce(Vector2.up  *  30, ForceMode2D.Impulse);
        monkeyBody.gravityScale = 10;
		// for (int i =  0; i  <  steps; i  ++){
		// 	this.transform.localScale  =  new  Vector3(this.transform.localScale.x, this.transform.localScale.y  -  stepper, this.transform.localScale.z);

		// 	// make sure enemy is still above ground
		// 	this.transform.position  =  new  Vector3(this.transform.position.x, groundSurface  +  GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
		// 	yield  return  null;
		// }
        while (monkeyScream.isPlaying)
        {
            yield return null;
        }
		Debug.Log("Flatten ends");
        // this.gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled  =  false;
        // this.gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled  =  false;
        this.gameObject.SetActive(false);
        Debug.Log("Enemy returned to pool");
        yield  break;
        
    }

    IEnumerator SpawnBananas(){
        
        // Debug.Log("timeLeft: " + timeLeft);
        if(timeLeft > 0){
            timeLeft -= Time.deltaTime;
            // Debug.Log("timeLeft: " + timeLeft);
            yield return null;
        }
        if(timeLeft <= 0){
            timeLeft = duration;
            Debug.Log("Throwing Banana!");
            // Debug.Log("timeLeft after Throwing Banana: " + timeLeft);
            Instantiate(banana, new  Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            yield return null;
        }
    }
}
