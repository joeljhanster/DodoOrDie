using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private Rigidbody2D coinBody;
    private AudioSource collectCoinAudio;
    // Start is called before the first frame update
    void Start()
    {
        coinBody = GetComponent<Rigidbody2D>();
        collectCoinAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void  OnTriggerEnter2D(Collider2D col){
        
        if (col.gameObject.CompareTag("Player")){
            Debug.Log("Coin Collected");
            CentralManager.centralManagerInstance.increaseScore();
            collectCoinAudio.Play();
            Debug.Log("HERE");
            this.GetComponent<SpriteRenderer>().enabled  =  false;
            this.GetComponent<BoxCollider2D>().enabled  =  false;
		    StartCoroutine(collectCoin());
            // GetComponent<EdgeCollider2D>().enabled  =  false;
        }
    }

    IEnumerator  collectCoin(){
		Debug.Log("Collecting Coin");
        while (collectCoinAudio.isPlaying)
        {
            yield return null;
        }
        this.gameObject.SetActive(false);
        yield  break;
	}
}
