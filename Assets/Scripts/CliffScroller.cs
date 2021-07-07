using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliffScroller : MonoBehaviour
{
    // public Renderer[] layers;
    public Renderer cliffLayer;
    public Renderer mistLayer;
    public float cliffSpeed;
    public float mistSpeed;
    // public float[] speedMultiplier;
    private float previousXPositionDodo;
    private float previousXPositionCamera;
    private float previousYPositionCamera;
    public Transform dodo;
    public Transform mainCamera;
    private float[] offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = new float[2];
        for (int i = 0; i < 2; i++) {
            offset[i] = 0.0f;
        }
        previousXPositionCamera = mainCamera.transform.position.x;
        previousYPositionCamera = mainCamera.transform.position.y;
        // offset = new float[layers.Length];
        // for(int i = 0; i < layers.Length; i++){
        //     offset[i] = 0.0f;
		// }
        // previousXPositionCamera = mainCamera.transform.position.x;
        // previousYPositionCamera = mainCamera.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // for(int i = 0; i< layers.Length; i ++){
        //     float newOffset = 0.01f;
        //     offset[i] = offset[i] + newOffset * speedMultiplier[i];
        //     layers[i].material.mainTextureOffset = new Vector2(0 , -offset[i]);
        // }
        // Cliff background
        float newOffset = 0.01f;
        offset[0] = offset[0] + newOffset * cliffSpeed;
        cliffLayer.material.mainTextureOffset = new Vector2(0, -offset[0]);

        // Mist background
        offset[1] = offset[1] + newOffset * mistSpeed;
        mistLayer.material.mainTextureOffset = new Vector2(offset[1], 0);

        // previousYPositionCamera = mainCamera.transform.position.y;
    }

    // // Start is called before the first frame update
    // void Start()
    // {
    //     offset = new float[layers.Length];
    //     for (int i = 0; i < layers.Length; i++) {
    //         offset[i] = 0.0f;
    //     }
    //     previousXPositionMario = mario.transform.position.x;
    //     previousXPositionCamera = mainCamera.transform.position.x;
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     // if camera has moved
    //     if (Mathf.Abs(previousXPositionCamera - mainCamera.transform.position.x) > 0.001f) {
    //         for (int i = 0; i < layers.Length; i++) {
    //             if (offset[i] > 1.0f || offset[i] < -1.0f)
    //                 offset[i] = 0.0f;   // reset offset
    //             float newOffset = mario.transform.position.x - previousXPositionMario;
    //             offset[i] = offset[i] + newOffset * speedMultiplier[i];
    //             layers[i].material.mainTextureOffset = new Vector2(offset[i], 0);
    //         }
    //     }
    //     // update previous pos
    //     previousXPositionMario = mario.transform.position.x;
    //     previousXPositionCamera = mainCamera.transform.position.x;
    // }
}
