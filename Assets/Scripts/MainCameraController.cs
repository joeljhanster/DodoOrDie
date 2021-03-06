using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    // public  Transform player; // Mario's Transform
    public List<Transform> players;
    public  Transform endLimit; // GameObject that indicates end of map
    public  Transform startLimit; // GameObject that indicates end of map
    private  float offset; // initial x-offset between camera and Mario
    private  float startX; // smallest x-coordinate of the Camera
    private  float endX; // largest x-coordinate of the camera
    private  float viewportHalfWidth;
    private float viewportHalfHeight;

    private Vector3 bottomLeft;

    private float originalX;


    // override  public  void  Awake(){
	// 	base.Awake();
	// 	// Debug.Log("awake called");
	// 	// other instructions...
	// }

    // Start is called before the first frame update
    void Start()
    {
        // get coordinate of the bottomleft of the viewport
	    // z doesn't matter since the camera is orthographic
	    bottomLeft =  Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
	    viewportHalfWidth  =  Mathf.Abs(bottomLeft.x  -  this.transform.position.x);
        viewportHalfHeight = Mathf.Abs(bottomLeft.y  -  this.transform.position.y);
        
	    // offset  =  this.transform.position.x  -  player.position.x;
        offset  =  this.transform.position.x  -  players[0].position.x;
	    startX  =  startLimit.transform.position.x  +  viewportHalfWidth;
	    endX  =  endLimit.transform.position.x  -  viewportHalfWidth;
        
        originalX = this.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float positionX = 0;
        int activeDodoCount = 0;
        foreach(Transform dodo in players)
        {
            if (
                dodo.gameObject.activeSelf &&
                dodo.gameObject.transform.position.y > bottomLeft.y &&
                dodo.gameObject.transform.position.y < bottomLeft.y + 3 * viewportHalfHeight
            ) {
                activeDodoCount += 1;
                positionX += dodo.position.x;
            }
        }
        float desiredX;
        float targetX;

        // float desiredX = (positionX / activeDodoCount) + offset;
        if (activeDodoCount > 0) {
            desiredX = (positionX / activeDodoCount); // center of all dodos
        } else {
            desiredX = transform.position.x;
        }

        if (desiredX < startX) {
            targetX = startX;
        } else if (desiredX < endX) {
            targetX = desiredX;
        } else {
            targetX = endX;
        }
        this.transform.position = new Vector3(targetX, this.transform.position.y, this.transform.position.z);
        // float desiredX =  player.position.x  +  offset;
        // check if desiredX is within startX and endX
        // if (desiredX  >  startX  &&  desiredX  <  endX){
        //     this.transform.position  =  new  Vector3(desiredX, this.transform.position.y, this.transform.position.z);
        // }
    }
}
