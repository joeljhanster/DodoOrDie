using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinController : MonoBehaviour
{
    public GameConstants gameConstants;
    public UnityEvent onWoodCollected;

    public DodoCharacter flowerDodo;
    public DodoCharacter goldenDodo;
    public DodoCharacter pirateDodo;
    public DodoCharacter rgbDodo;

    private Rigidbody2D coinBody;
    private SpriteRenderer coinSprite;
    private BoxCollider2D coinBox;
    private AudioSource collectCoinAudio;

    // Start is called before the first frame update
    void Start()
    {
        coinBody = GetComponent<Rigidbody2D>();
        collectCoinAudio = GetComponent<AudioSource>();
        coinSprite = GetComponent<SpriteRenderer>();
        coinBox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void  OnTriggerEnter2D(Collider2D col){
        
        // if (col.gameObject.CompareTag("Player")){
        //     Debug.Log("Coin Collected");
        //     CentralManager.centralManagerInstance.increaseScore();
        //     collectCoinAudio.Play();
        //     Debug.Log("HERE");
        //     this.GetComponent<SpriteRenderer>().enabled  =  false;
        //     this.GetComponent<BoxCollider2D>().enabled  =  false;
		//     StartCoroutine(collectCoin());
        //     // GetComponent<EdgeCollider2D>().enabled  =  false;
        // }

        if (col.gameObject.CompareTag("FlowerDodo")) {
            Debug.Log("Wood Collected by flower dodo");
            collectCoinAudio.Play();

            // Add score to dodo
            flowerDodo.AddScore(gameConstants.score);

            onWoodCollected.Invoke();
            coinSprite.enabled = false;
            coinBox.enabled = false;
            StartCoroutine("collectCoin");
        }
        else if (col.gameObject.CompareTag("GoldenDodo")) {
            Debug.Log("Wood Collected by golden dodo");
            collectCoinAudio.Play();

            // Add score to dodo
            goldenDodo.AddScore(gameConstants.score);

            onWoodCollected.Invoke();
            coinSprite.enabled = false;
            coinBox.enabled = false;
            StartCoroutine("collectCoin");
        }
        else if (col.gameObject.CompareTag("PirateDodo")) {
            Debug.Log("Wood Collected by pirate dodo");
            collectCoinAudio.Play();

            // Add score to dodo
            pirateDodo.AddScore(gameConstants.score);

            onWoodCollected.Invoke();
            coinSprite.enabled = false;
            coinBox.enabled = false;
            StartCoroutine("collectCoin");
        }
        else if (col.gameObject.CompareTag("RGBDodo")) {
            Debug.Log("Wood Collected by rgb dodo");
            collectCoinAudio.Play();

            // Add score to dodo
            rgbDodo.AddScore(gameConstants.score);

            onWoodCollected.Invoke();
            coinSprite.enabled = false;
            coinBox.enabled = false;
            StartCoroutine("collectCoin");
        }
    }

    IEnumerator collectCoin()
    {
		Debug.Log("Collecting Coin");
        while (collectCoinAudio.isPlaying)
        {
            yield return null;
        }
        this.gameObject.SetActive(false);
        yield  break;
	}
}
