using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    // public GameObject gameObject;
    private bool broken = false;
    public GameObject Debris;

    public Transform mainCamera;
    private float viewportHalfWidthX;
    private float viewportHalfHeightY;
    private float obstacleWidth;
    private Rigidbody2D obstacleBody;
    private Vector3 bottomLeft;
    private float topY;

    // Start is called before the first frame update
    void Start()
    {
        obstacleBody = GetComponent<Rigidbody2D>();

        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));

        viewportHalfWidthX = Mathf.Abs(bottomLeft.x - mainCamera.position.x);
        viewportHalfHeightY = Mathf.Abs(bottomLeft.y - mainCamera.position.y);

        topY = bottomLeft.y + 2 * viewportHalfHeightY;
    }

    void resetPosition()
    {
        Vector2 position = new Vector2(
            Random.Range(bottomLeft.x, bottomLeft.x + 2 * viewportHalfWidthX),
            bottomLeft.y - 10
        );
        obstacleBody.MovePosition(position);
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
    void  OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Player") &&  !broken){
            broken  =  true;
            // assume we have 5 debris per box
            for (int x =  0; x<5; x++){
                Instantiate(Debris, transform.position, Quaternion.identity);
            }
            // gameObject.transform.GetComponent<SpriteRenderer>().enabled  =  false;
            // gameObject.transform.GetComponent<BoxCollider2D>().enabled  =  false;
            // GetComponent<EdgeCollider2D>().enabled  =  false;
            // GetComponent<SpriteRenderer>().enabled = false;
            // Destroy(this.gameObject);
            resetPosition();
        }
    }
}
