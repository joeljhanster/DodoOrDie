using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    public GameObject obstacle;

    private float viewportHalfWidthX;
    private float viewportHalfHeightY;
    private float obstacleWidth;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        
        viewportHalfWidthX  =  Mathf.Abs(bottomLeft.x  -  Camera.main.transform.position.x);
        viewportHalfHeightY = Mathf.Abs(bottomLeft.y  -  Camera.main.transform.position.y);

        obstacleWidth = obstacle.GetComponent<SpriteRenderer>().sprite.rect.width;
        
        Instantiate(obstacle,
        new Vector3(
            Random.Range(bottomLeft.x, bottomLeft.x + 2 * viewportHalfWidthX),
                bottomLeft.y, 0) , Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
        // new Vector3(Random.Range(startX,endX), startY);
        // Instantiate(obstacle, new Vector3(Random.Range(startX,endX), startY) , Quaternion.identity);
    }
}
