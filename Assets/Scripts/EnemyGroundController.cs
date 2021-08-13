using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyGroundController : MonoBehaviour
{
	public UnityEvent onPlayerEaten;

	public DodoCharacter flowerDodo;
    public DodoCharacter goldenDodo;
    public DodoCharacter pirateDodo;
    public DodoCharacter rgbDodo;

    private  int moveRight;
	private  float originalX;
	private  Vector2 velocity;
	private  Rigidbody2D enemyBody;
    private int maxOffset = 5;
    private int enemyPatroltime = 1;
    private float groundSurface = -4.3f;
    private SpriteRenderer enemySprite;
    private AudioSource monkeyScream;
    private bool monkeyDead = false;
    // Start is called before the first frame update
    void Start()
    {
        enemyBody  =  GetComponent<Rigidbody2D>();
		
		// get the starting position
		originalX  =  transform.position.x;

        enemySprite = GetComponent<SpriteRenderer>();

        monkeyScream = GetComponent<AudioSource>();
	
		// randomise initial direction
		moveRight  =  Random.Range(0, 2) ==  0  ?  -1  :  1;

        if(moveRight == 1){
            enemySprite.flipX = true;
        }
		
		// compute initial velocity
		ComputeVelocity();
        
    }

    void  ComputeVelocity()
	{
			velocity  =  new  Vector2((moveRight) *  maxOffset  /  enemyPatroltime, 0);
	}
  
	void  MoveEnemy()
	{
        if(!monkeyDead){
		    enemyBody.MovePosition(enemyBody.position  +  velocity  *  Time.fixedDeltaTime);
        }
	}

	void  Update()
	{
		if (Mathf.Abs(enemyBody.position.x  -  originalX) <  maxOffset)
		{// move enemy
			MoveEnemy();
		}
		else
		{
			// change direction
			moveRight  *=  -1;
            if(enemySprite.flipX){
                enemySprite.flipX = false;
            }else{enemySprite.flipX = true;}
			ComputeVelocity();
			MoveEnemy();
		}
	}

    void  OnTriggerEnter2D(Collider2D other){
		// check if it collides with player
		// if (other.gameObject.tag  ==  "Player"){
        //     Debug.Log("Collided with Player");
		// 	// check if collides on top
		// 	float yoffset = (other.transform.position.y  -  this.transform.position.y);
        //     Debug.Log("y offset: " + yoffset);
		// 	if (yoffset  >  0.34f){
        //         Debug.Log("Kill Enemy");
		// 		KillSelf();
		// 	}
		// 	else{
        //         Debug.Log("Kill Player");
		// 		// Kill Player
		// 		CentralManager.centralManagerInstance.killPlayer();
		// 	}
		// }

		if (other.gameObject.CompareTag("FlowerDodo")) {
            Debug.Log("Sky monkey collided with flower dodo");
            // check if collides on top
            float yoffset = (other.transform.position.y - this.transform.position.y);
            Debug.Log("y offset: " + yoffset);
            if (yoffset > 0.34f) {
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
            if (yoffset > 0.34f) {
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
            if (yoffset > 0.34f) {
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
            if (yoffset > 0.34f) {
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

    void  KillSelf(){
		// enemy dies
        monkeyDead = true;
        monkeyScream.Play();
		StartCoroutine("flatten");
		Debug.Log("Kill sequence ends");
	}

    IEnumerator flatten(){
		Debug.Log("Flatten starts");
		int steps =  30;
		float stepper =  1.0f/(float) steps;
		enemyBody.isKinematic = true;
		for (int i =  0; i  <  steps; i  ++){
			this.transform.localScale  =  new  Vector3(this.transform.localScale.x, this.transform.localScale.y  -  stepper, this.transform.localScale.z);

			// make sure enemy is still above ground
			this.transform.position  =  new  Vector3(this.transform.position.x, groundSurface  +  GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
			yield  return  null;
		}
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
}
