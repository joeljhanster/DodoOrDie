using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beach_CameraController : MonoBehaviour
{
    public  Transform player; // Mario's Transform
    public  Transform endLimitX; // GameObject that indicates end of map
    public  Transform startLimitX; // GameObject that indicates end of map
    public  Transform endLimitY;
    public  Transform startLimitY;
    private  float offsetx; // initial x-offset between camera and Mario
    private  float offsety;
    private  float startX; // smallest x-coordinate of the Camera
    private  float endX; // largest x-coordinate of the camera
    private  float startY;
    private  float endY;
    private  float viewportHalfWidth;
    private  float viewportHalfHeight;
    // Start is called before the first frame update
    void Start()
    {
        // get coordinate of the bottomleft of the viewport
	    // z doesn't matter since the camera is orthographic
	    Vector3 bottomLeft =  Camera.main.ViewportToWorldPoint(new  Vector3(0, 0, 0));
	    viewportHalfWidth  =  Mathf.Abs(bottomLeft.x  -  this.transform.position.x);
        viewportHalfHeight = Mathf.Abs(bottomLeft.y - this.transform.position.y);
	    
        offsetx  =  this.transform.position.x  -  player.position.x;
        offsety = this.transform.position.y - player.position.y;
	    startX  =  startLimitX.transform.position.x  +  viewportHalfWidth;
	    endX  =  endLimitX.transform.position.x  -  viewportHalfWidth;
        startY = startLimitY.transform.position.y + viewportHalfHeight;
        endY = endLimitY.transform.position.y - viewportHalfHeight;
    }

    // Update is called once per frame
    void Update()
    {
        float desiredX =  player.position.x  +  offsetx;
        float desiredY = player.position.y + offsety;
        // check if desiredX is within startX and endX
        if (desiredX  >  startX  &&  desiredX  <  endX){
            this.transform.position  =  new  Vector3(desiredX, this.transform.position.y, this.transform.position.z);
        }
        
        // if (desiredY > startY && desiredY < endY){
        //     this.transform.position = new Vector3(this.transform.position.x, desiredY, this.transform.position.z);
        // }
    }
}
