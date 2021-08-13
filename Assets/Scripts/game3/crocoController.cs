using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crocoController : MonoBehaviour
{
    private  Animator crocoAnimator;
    private bool hitdodo = false;
    // public float timeStart;
    // public Vector3 vector3;

    // public GameObject Player;
    // public GameObject other;

    private AudioSource crocoaudio;

    private List<GameObject> targetDodos = new List<GameObject>();

    void Start(){
        crocoAnimator = GetComponent<Animator>();
        // Player = GameObject.Find("Player");
        crocoaudio = GetComponent<AudioSource>();
        // other = GameObject.Find("Lives");
    }

    void Update(){
        // if (hitdodo == true){
        //     timeStart -= Time.deltaTime;
        //     if (timeStart <= 1){
        //         if (timeStart <= 0){
        //             Player.transform.parent = null;  
        //             respawn();
        //             timeStart = 2;
        //         }
        //         crocoAnimator.SetTrigger("onWait2sec");
        //         crocoaudio.PlayOneShot(crocoaudio.clip);
        //     }
        // }   
    }
    // Start is called before the first frame update
    void OnTriggerStay2D(Collider2D col) {
        // Frog? Then make it a Child
        // if (col.name == "Player"){
        //     col.transform.parent = transform;
        //     hitdodo = true;
        // }

        if (
            col.gameObject.CompareTag("FlowerDodo") ||
            col.gameObject.CompareTag("GoldenDodo") ||
            col.gameObject.CompareTag("PirateDodo") ||
            col.gameObject.CompareTag("RGBDodo")
        ) {
            col.transform.parent = transform;
            if (targetDodos.IndexOf(col.gameObject) < 0) {
                targetDodos.Add(col.gameObject);
            }
            StartCoroutine("waitAndEat");
        }
    }

    
    void OnTriggerExit2D(Collider2D col) {
        // if (col.name == "Player"){
        //     col.transform.parent = null;  
        //     hitdodo = false;     
        // }

        if (
            col.gameObject.CompareTag("FlowerDodo") ||
            col.gameObject.CompareTag("GoldenDodo") ||
            col.gameObject.CompareTag("PirateDodo") ||
            col.gameObject.CompareTag("RGBDodo")
        ) {
            col.transform.parent = null;
            if (targetDodos.IndexOf(col.gameObject) >= 0) {
                targetDodos.Remove(col.gameObject);
            }
        }
    }

    IEnumerator waitAndEat()
    {
        yield return new WaitForSeconds(1.5f);
        crocoAnimator.SetTrigger("onWait2sec");
        yield return new WaitForSeconds(0.5f);
        
        crocoaudio.Play();
        // At least 1 dodo is still on crocodile
        if (targetDodos.Count > 0) {
            // crocoAnimator.SetTrigger("onWait2Sec");
            // crocoaudio.Play();
            foreach(GameObject dodo in targetDodos) {
                dodo.transform.parent = null;
                dodo.GetComponent<DodoController3>().resetPosition();
            }
        }
    }


    // public void respawn(){
    //     if (Player.transform.localPosition == vector3){
    //         return;
    //     }
    //     else{
    //         Player.transform.localPosition = vector3;
    //         other.GetComponent<UpdateScore>().ifdie();
    //     }
    // }
}
