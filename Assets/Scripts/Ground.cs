using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ground : MonoBehaviour
{
    public GameConstants gameConstants;
    public UnityEvent onSceneChange;
    public Transform forestBackground;
    private AudioSource groundAudio;
    private Vector3 bottomLeft;
    private bool playerSurvived = false;
    private Vector3 originalPosition;
    private bool hit = false;
    

    // Start is called before the first frame update
    void Start()
    {
        groundAudio = GetComponent<AudioSource>();
        originalPosition = forestBackground.position;
        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        // forestBackground.position = new Vector3(Camera.main.transform.position.x, botto)
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if ((
            col.gameObject.CompareTag("FlowerDodo") ||
            col.gameObject.CompareTag("GoldenDodo") ||
            col.gameObject.CompareTag("PirateDodo") ||
            col.gameObject.CompareTag("RGBDodo")
         ) && !hit) {
            groundAudio.Play();
            // Throw leaves

            // Change scene
            Debug.Log("Invoking scene change");
            onSceneChange.Invoke();
            hit = true;
        }
    }

    public void playerSurvivesResponse()
    {
        playerSurvived = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSurvived) {
            float speed = gameConstants.cliffMaxSpeed * Time.fixedDeltaTime;
            if (forestBackground.position.y - bottomLeft.y < 0.9 * (bottomLeft.y - originalPosition.y)) {
                forestBackground.position += Vector3.up * speed;
            }
        }
    }
}
