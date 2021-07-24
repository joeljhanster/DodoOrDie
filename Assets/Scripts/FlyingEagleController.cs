using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEagleController : MonoBehaviour
{
    public GameConstants gameConstants;
    private SpriteRenderer eagleSprite;
    private Rigidbody2D eagleBody;
    private BoxCollider2D eagleBox;
    private Animator eagleAnimator;

    private float viewportHalfWidthX;
    private float viewportHalfHeightY;

    private Vector3 bottomLeft;

    private Vector2 startPosition;
    private Vector2 endPosition;

    private int flyRight = 1;

    private bool flying = false;
    
    private float time = 0.0f;

    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        eagleSprite = GetComponent<SpriteRenderer>();
        eagleBody = GetComponent<Rigidbody2D>();
        eagleBox = GetComponent<BoxCollider2D>();

        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        
        viewportHalfWidthX = Mathf.Abs(bottomLeft.x - Camera.main.transform.position.x);
        viewportHalfHeightY = Mathf.Abs(bottomLeft.y - Camera.main.transform.position.y);

        // startPosition = 
        resetPosition();
    }

    void resetPosition()
    {
        flyRight *= -1;
        startPosition = new Vector2(
            Camera.main.transform.position.x + (-flyRight) * (viewportHalfWidthX + eagleBox.size.x),
            Random.Range(bottomLeft.y, bottomLeft.y + viewportHalfHeightY)
        );
        endPosition = new Vector2(
            Camera.main.transform.position.x + (flyRight) * (viewportHalfWidthX + eagleBox.size.x),
            Random.Range(bottomLeft.y, bottomLeft.y + viewportHalfHeightY)
        );
        transform.position = new Vector3(startPosition.x, startPosition.y, 0.0f);
        Debug.Log(startPosition);
        Debug.Log(endPosition);
        flying = false;
        i++;
    }

    void flyAcross()
    {
        if (flying) {
            Vector2 direction = endPosition - startPosition;
            Debug.Log((flyRight) * endPosition.x - transform.position.x);
            
            if ((flyRight) * (endPosition.x - transform.position.x) > 0) {
                // Fly
                eagleBody.MovePosition(eagleBody.position + direction / gameConstants.flyDuration * Time.fixedDeltaTime);
            } else {
                resetPosition();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.fixedDeltaTime;
        if (time > gameConstants.flyInterval * i) {
            flying = true;
            flyAcross();
        }
        eagleSprite.flipX = flyRight == 1 ? false : true;
    }
}
