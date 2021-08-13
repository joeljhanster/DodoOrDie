using UnityEngine;
using System.Collections;

public class Border : MonoBehaviour {
    public Vector3 vector3;
    public GameObject Player;
    private AudioSource failaudio;
    public UpdateScore other;

    void Start(){
        Player = GameObject.Find("Player");
        failaudio = GetComponent<AudioSource>();

    }
    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.tag == "Player")
            // coll.transform.parent = null;     
            respawn();
    }

    public void respawn(){
        if (Player.transform.position == vector3){
            return;
        }
        else{
            Player.transform.position = vector3;
            other.GetComponent<UpdateScore>().ifdie();
            failaudio.PlayOneShot(failaudio.clip);
        }
    }
}