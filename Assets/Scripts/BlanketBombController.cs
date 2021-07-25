using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlanketBombController : MonoBehaviour
{
    private bool triggered = false;
    public  GameObject banana; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(triggered){

            StartCoroutine(SpawnBananas());
        }
        
    }

    void  OnTriggerEnter2D(Collider2D other){
		// check if it collides with player
		if (other.gameObject.tag  ==  "Player"){
            Debug.Log("Blanket Bomb Start!");
            triggered = true;
		}
	}
    void  OnTriggerExit2D(Collider2D other){
		// check if it collides with player
		if (other.gameObject.tag  ==  "Player"){
            Debug.Log("Blanket Bomb End!");
            triggered = false;
		}
	}

    IEnumerator SpawnBananas(){
        
        if(triggered){
            Instantiate(banana, new  Vector3(this.transform.position.x + Random.Range(-10, 10), this.transform.position.y + 5.0f, this.transform.position.z), Quaternion.identity);
            yield return null;
        }else{yield return null;}
        
    }
}
