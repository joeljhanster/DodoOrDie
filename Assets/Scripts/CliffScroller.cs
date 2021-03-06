using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliffScroller : MonoBehaviour
{
    // public Renderer[] layers;
    public Renderer cliffLayer;
    public Renderer mistLayer;
    public Renderer forestLayer;
    public GameConstants gameConstants;

    private float cliffSpeed;
    private float mistSpeed;

    // public float[] speedMultiplier;
    private float previousXPositionDodo;
    private float previousXPositionCamera;
    private float previousYPositionCamera;
    public Transform dodo;
    // public Transform mainCamera;
    private float[] offset;
    private float time = 0.0f;
    private bool playerSurvived = false;
    // Start is called before the first frame update
    void Start()
    {
        offset = new float[2];
        for (int i = 0; i < 2; i++) {
            offset[i] = 0.0f;
        }
        previousXPositionCamera = Camera.main.transform.position.x;
        previousYPositionCamera = Camera.main.transform.position.y;

        cliffSpeed = gameConstants.cliffStartSpeed;
        mistSpeed = gameConstants.mistStartSpeed;
    }

    void CalculateSpeed() {
         // How much to increase over time?
        float mistIncrement = (gameConstants.mistMaxSpeed - gameConstants.mistStartSpeed) * Time.fixedDeltaTime / gameConstants.gameDuration;
        float cliffIncrement = (gameConstants.cliffMaxSpeed - gameConstants.cliffStartSpeed) * Time.fixedDeltaTime / gameConstants.gameDuration;

        if (time < gameConstants.gameDuration) {
            time += Time.fixedDeltaTime;
            mistSpeed += mistIncrement;
            cliffSpeed += cliffIncrement;
        }
    }

    public void PlayerSurvived()
    {
        playerSurvived = true;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateSpeed();
        // Cliff background
        float newOffset = 0.01f;
        offset[0] = offset[0] + newOffset * cliffSpeed; // Make it increase with time
        cliffLayer.material.mainTextureOffset = new Vector2(0, -offset[0]);

        // Mist background
        offset[1] = offset[1] + newOffset * mistSpeed;
        mistLayer.material.mainTextureOffset = new Vector2(offset[1], 0);

        // Forest background
        // if (!playerSurvived) {
        //     forestLayer.material.mainTextureOffset = new Vector2(0, -offset[0]);
        // }
    }
}
