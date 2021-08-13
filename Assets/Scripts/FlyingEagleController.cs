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
    private AudioSource eagleAudio;

    private float viewportHalfWidthX;
    private float viewportHalfHeightY;

    private Vector3 bottomLeft;

    private Vector2 startPosition;
    private Vector2 endPosition;

    private int flyRight = 1;

    private bool flying = false;
    
    private float time = 0.0f;

    private int i = 0;
    private bool soundPlayed = false;
    private bool playerSurvived = false;

    // Start is called before the first frame update
    void Start()
    {
        eagleSprite = GetComponent<SpriteRenderer>();
        eagleBody = GetComponent<Rigidbody2D>();
        eagleBox = GetComponent<BoxCollider2D>();
        eagleAudio = GetComponent<AudioSource>();

        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        
        viewportHalfWidthX = Mathf.Abs(bottomLeft.x - Camera.main.transform.position.x);
        viewportHalfHeightY = Mathf.Abs(bottomLeft.y - Camera.main.transform.position.y);

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
        flying = false;
        i++;
        soundPlayed = false;
    }

    public void PlayerEatenResponse()
    {
        eagleAudio.Play();
    }

    public void PlayerSurvivesResponse()
    {
        playerSurvived = true;
        eagleAudio.Play();
    }
    void flyAway()
    {
        Vector2 direction = new Vector2(endPosition.x - transform.position.x, bottomLeft.y + 3 * viewportHalfHeightY - transform.position.y);
        eagleBody.MovePosition(eagleBody.position + 3 * direction / gameConstants.flyDuration * Time.fixedDeltaTime);
    }

    void flyAcross()
    {
        if (flying) {
            if (!soundPlayed) {
                eagleAudio.Play();
                soundPlayed = true;
            }
            Vector2 direction = endPosition - startPosition;
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

        if (playerSurvived) {
            // fly away
            flyAway();
        } else if (time > gameConstants.flyInterval * i) {
            flying = true;
            flyAcross();
        }
        eagleSprite.flipX = flyRight == 1 ? false : true;
    }
}
