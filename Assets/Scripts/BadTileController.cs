using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadTileController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void  OnTriggerEnter2D(Collider2D other){
		// check if it collides with player
		if (other.gameObject.tag  ==  "Player"){
            Debug.Log("Kill Player");
            // Kill Player
            CentralManager.centralManagerInstance.killPlayer();
		}
	}

}
