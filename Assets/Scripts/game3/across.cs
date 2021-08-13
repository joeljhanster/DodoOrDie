using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class across : MonoBehaviour
{
    public UnityEvent onSceneChange;
    private AudioSource congrataudio;

    // Start is called before the first frame update
    void Start()
    {
        congrataudio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        // if (col.name == "Player"){
        //     congrataudio.PlayOneShot(congrataudio.clip);
        // }

        if (
            col.gameObject.CompareTag("FlowerDodo") ||
            col.gameObject.CompareTag("GoldenDodo") ||
            col.gameObject.CompareTag("PirateDodo") ||
            col.gameObject.CompareTag("RGBDodo")
        ) {
            congrataudio.Play();
            // onSceneChange.Invoke();
        }

    }
    // Update is called once per frame
    void OnTriggerStay2D(Collider2D col) {
        // Frog? Then make it a Child
        // if (col.name == "Player"){
        //     col.transform.parent = transform;
        // }
        if (
            col.gameObject.CompareTag("FlowerDodo") ||
            col.gameObject.CompareTag("GoldenDodo") ||
            col.gameObject.CompareTag("PirateDodo") ||
            col.gameObject.CompareTag("RGBDodo")
        ) {
            col.transform.parent = transform;
        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        coll.transform.parent = null;     
    }
}

