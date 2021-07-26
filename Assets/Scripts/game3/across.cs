using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class across : MonoBehaviour
{
    private AudioSource congrataudio;

    // Start is called before the first frame update
    void Start()
    {
        congrataudio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.name == "Player"){
            congrataudio.PlayOneShot(congrataudio.clip);
        }
    }
    // Update is called once per frame
    void OnTriggerStay2D(Collider2D coll) {
        // Frog? Then make it a Child
        if (coll.name == "Player"){
            coll.transform.parent = transform;

        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        coll.transform.parent = null;        
    }
}

