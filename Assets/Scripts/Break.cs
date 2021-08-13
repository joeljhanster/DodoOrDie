using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    private bool broken = false;
    public GameObject Debris;
    public GameConstants gameConstants;
    private float viewportHalfWidthX;
    private float viewportHalfHeightY;
    private Rigidbody2D obstacleBody;
    private AudioSource obstacleAudio;
    private Vector3 bottomLeft;
    private float topY;
    private Quaternion startRot;

    // Start is called before the first frame update
    void Start()
    {
        obstacleBody = GetComponent<Rigidbody2D>();
        obstacleAudio = GetComponent<AudioSource>();

        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));

        viewportHalfWidthX = Mathf.Abs(bottomLeft.x - Camera.main.transform.position.x);
        viewportHalfHeightY = Mathf.Abs(bottomLeft.y - Camera.main.transform.position.y);

        topY = bottomLeft.y + 2 * viewportHalfHeightY;

        startRot = transform.rotation;

    }

    void resetPosition()
    {
        Vector2 position = new Vector2(
            Random.Range(bottomLeft.x, bottomLeft.x + 2 * viewportHalfWidthX),
            bottomLeft.y - 10
        );
        obstacleBody.MovePosition(position);
        obstacleBody.velocity = Vector2.zero;
        obstacleBody.angularVelocity = 0;
        transform.rotation = startRot;
        broken = false;
    }

    // Update is called once per frame
    void Update()
    {
       // Check if obstacle is above camera
       if (this.gameObject.transform.position.y > topY) {
           resetPosition();
       }
    }
    void  OnTriggerEnter2D(Collider2D col)
    {
        if ((
            col.gameObject.CompareTag("FlowerDodo") || 
            col.gameObject.CompareTag("GoldenDodo") ||
            col.gameObject.CompareTag("PirateDodo") ||
            col.gameObject.CompareTag("RGBDodo")
        ) && !broken) {
            obstacleAudio.Play();
            broken = true;
            // assume we have 5 debris per rock
            for (int x = 0; x < gameConstants.numDebris; x++) {
                Instantiate(Debris, transform.position, Quaternion.identity);
            }
            resetPosition();
        }
    }
}
