using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crocoController : MonoBehaviour
{
    private  Animator crocoAnimator;
    private bool hitdodo = false;
    public float timeStart;
    public Vector3 vector3;

    public GameObject Player;


    void Start(){
        crocoAnimator = GetComponent<Animator>();
        Player = GameObject.Find("Player");
    }
    void Update(){
        if (hitdodo == true){
            timeStart -= Time.deltaTime;
            if (timeStart <= 1){
                if (timeStart <= 0){
                    Player.transform.parent = null;  
                    Player.transform.localPosition=vector3;
                    timeStart = 2;
                }
                crocoAnimator.SetTrigger("onWait2sec");
            }
        }   
    }
    // Start is called before the first frame update
    void OnTriggerStay2D(Collider2D coll) {
        // Frog? Then make it a Child
        if (coll.name == "Player"){
            coll.transform.parent = transform;
            hitdodo = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        if (coll.name == "Player"){
            coll.transform.parent = null;  
            hitdodo = false;     
        } 
    }
}
