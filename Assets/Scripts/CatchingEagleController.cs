using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchingEagleController : MonoBehaviour
{
    public GameConstants gameConstants;
    public Transform player;

    private SpriteRenderer eagleSprite;
    private Rigidbody2D eagleBody;
    private BoxCollider2D eagleBox;
    private Animator eagleAnimator;

    private float viewportHalfWidthX;
    private float viewportHalfHeightY;
    
    private Vector3 bottomLeft;
    private float dangerZoneY;
    private float eatingZoneY;

    private Vector3 startPosition;

    private bool chasing = false;

    private float moveDistance = 0.0f;
    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        eagleSprite = GetComponent<SpriteRenderer>();   // used for flipping eagle or opening wings when dodo is near
        eagleBody = GetComponent<Rigidbody2D>();
        eagleBox = GetComponent<BoxCollider2D>();

        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        
        viewportHalfWidthX = Mathf.Abs(bottomLeft.x - Camera.main.transform.position.x);
        viewportHalfHeightY = Mathf.Abs(bottomLeft.y - Camera.main.transform.position.y);

        startPosition = new Vector3(bottomLeft.x + viewportHalfWidthX, bottomLeft.y + (2.0f * viewportHalfHeightY) - (eagleBox.size.y / 5.0f), 0.0f);
        transform.position = startPosition;
        dangerZoneY = Camera.main.transform.position.y - viewportHalfHeightY / 2.0f;
        eatingZoneY = Camera.main.transform.position.y - eagleBox.size.y / 2.0f;

        eagleSprite.enabled = false;
        eagleBox.enabled = false;
    }

    void chasePlayer() {
        if (player.position.y > dangerZoneY && player.position.y < transform.position.y && Mathf.Abs(Camera.main.transform.position.x - player.position.x) < viewportHalfWidthX) {
            moveDistance = player.position.x - transform.position.x;
            chasing = true;
        } else {
            moveDistance = startPosition.x - transform.position.x;
            chasing = false;
        }
        if (Mathf.Abs(moveDistance) > 0.0f) {
            Vector2 velocity = new Vector2(moveDistance / gameConstants.patrolTime, 0);
            eagleBody.MovePosition(eagleBody.position + velocity * Time.fixedDeltaTime);
        }

        // if (player.position.y > eatingZoneY && player.position.y < transform.position.y) {
        //     // eagleBody.MovePositionX(player.position.x);
        //     // Instead of moving position, maybe remove the top boundary box so player disappears
        //     eagleBody.MovePosition(new Vector2(player.position.x, transform.position.y));
        // }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.fixedDeltaTime;
        if (time > gameConstants.startDuration) {
            eagleSprite.enabled = true;
            eagleBox.enabled = true;
            chasePlayer();
        }

        // Toggle animation based on chasing (open wings when chasing)
    }
}
