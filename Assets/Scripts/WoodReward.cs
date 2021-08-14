using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WoodReward : MonoBehaviour
{    
    public GameConstants gameConstants;
    public UnityEvent onWoodCollected;

    public DodoCharacter flowerDodo;
    public DodoCharacter goldenDodo;
    public DodoCharacter pirateDodo;
    public DodoCharacter rgbDodo;

    private float viewportHalfWidthX;
    private float viewportHalfHeightY;
    private Vector3 bottomLeft;
    private Rigidbody2D woodBody;
    private AudioSource woodAudio;
    private float topY;
    private bool collected = false;

    // Start is called before the first frame update
    void Start()
    {
        woodBody = GetComponent<Rigidbody2D>();
        woodAudio = GetComponent<AudioSource>();
        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));

        viewportHalfWidthX = Mathf.Abs(bottomLeft.x - Camera.main.transform.position.x);
        viewportHalfHeightY = Mathf.Abs(bottomLeft.y - Camera.main.transform.position.y);

        topY = bottomLeft.y + 2 * viewportHalfHeightY;
    }

    void resetPosition()
    {
        Vector2 position = new Vector2(
            Random.Range(bottomLeft.x, bottomLeft.x + 2 * viewportHalfWidthX),
            bottomLeft.y - 10
        );
        woodBody.MovePosition(position);
        woodBody.velocity = Vector2.zero;
        woodBody.angularVelocity = 0;

        collected = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if obstacle is above camera
        if (this.gameObject.transform.position.y > topY) {
            resetPosition();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!collected) {
            if (col.gameObject.CompareTag("FlowerDodo")) {
                Debug.Log("Wood Collected by flower dodo");
                collected = true;

                woodAudio.Play();

                // Add score to dodo
                flowerDodo.AddScore(gameConstants.score);

                onWoodCollected.Invoke();

                resetPosition();
            }
            else if (col.gameObject.CompareTag("GoldenDodo")) {
                Debug.Log("Wood Collected by golden dodo");
                collected = true;

                woodAudio.Play();

                // Add score to dodo
                goldenDodo.AddScore(gameConstants.score);

                onWoodCollected.Invoke();

                resetPosition();
            }
            else if (col.gameObject.CompareTag("PirateDodo")) {
                Debug.Log("Wood Collected by pirate dodo");
                collected = true;

                woodAudio.Play();

                // Add score to dodo
                pirateDodo.AddScore(gameConstants.score);

                onWoodCollected.Invoke();

                resetPosition();
            }
            else if (col.gameObject.CompareTag("RGBDodo")) {
                Debug.Log("Wood Collected by rgb dodo");
                collected = true;

                woodAudio.Play();

                // Add score to dodo
                rgbDodo.AddScore(gameConstants.score);

                onWoodCollected.Invoke();

                resetPosition();
            }
        }
    }
}
