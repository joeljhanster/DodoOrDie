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
    public GameObject other;

    private AudioSource crocoaudio;

    void Start(){
        crocoAnimator = GetComponent<Animator>();
        Player = GameObject.Find("Player");
        crocoaudio = GetComponent<AudioSource>();
        other = GameObject.Find("Lives");
    }
    void Update(){
        if (hitdodo == true){
            timeStart -= Time.deltaTime;
            if (timeStart <= 1){
                if (timeStart <= 0){
                    Player.transform.parent = null;  
                    respawn();
                    timeStart = 2;
                }
                crocoAnimator.SetTrigger("onWait2sec");
                crocoaudio.PlayOneShot(crocoaudio.clip);
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
    public void respawn(){
        if (Player.transform.position == vector3){
            return;
        }
        else{
            Player.transform.position = vector3;
            other.GetComponent<UpdateScore>().ifdie();
        }
    }
}
