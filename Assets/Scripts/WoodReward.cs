using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WoodReward : MonoBehaviour
{    
    public GameConstants gameConstants;
    public UnityEvent onWoodCollected;

    private float viewportHalfWidthX;
    private float viewportHalfHeightY;
    private Vector3 bottomLeft;
    private Rigidbody2D woodBody;
    private AudioSource woodAudio;
    private float topY;
    private bool collected = false;
    private int score = 100;

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
        if (col.gameObject.CompareTag("Player") && !collected) {
            collected = true;

            // Do some effect
            woodAudio.Play();

            // Add score to player
            // woodCollected.ApplyChange(score);

            // Call onWoodCollected event
            onWoodCollected.Invoke();

            resetPosition();
        }
    }
}
