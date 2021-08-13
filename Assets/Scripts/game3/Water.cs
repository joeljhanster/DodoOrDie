using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {
    public Vector3 vector3;
    private GameObject Player;

    public UpdateScore other;
    // public Text lives;
    // private int playerScore =  5;
    private AudioSource failaudio;

    void Start(){
        // UpdateScore = GameObject.Find("Lives");
        Player = GameObject.Find("Player");
        failaudio = GetComponent<AudioSource>();

    }
    void OnTriggerStay2D(Collider2D coll) {
        // Frog?
        if (coll.name == "Player")
            // Not Jumping?
            if (!coll.GetComponent<Frog>().isJumping())
                // Not on a platform?
                if (coll.transform.parent == null){
                    // coll.gameObject.transform.position = vector3;
                    // other.GetComponent<UpdateScore>().ifdie();
                    respawn();
                }
                    // Game Over
                    // Destroy(coll.gameObject);

                    // playerScore -= 1;
		            // lives.text = "Lives: "  + playerScore.ToString();
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
