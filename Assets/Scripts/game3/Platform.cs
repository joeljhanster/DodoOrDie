using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    void OnTriggerStay2D(Collider2D col) {
        // Frog? Then make it a Child
        // if (col.name == "Player")
        //     col.transform.parent = transform;
        
        if (
            col.gameObject.CompareTag("FlowerDodo") ||
            col.gameObject.CompareTag("GoldenDodo") ||
            col.gameObject.CompareTag("PirateDodo") ||
            col.gameObject.CompareTag("RGBDodo")
        ) {
            col.transform.parent = transform;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        col.transform.parent = null;        
    }
}
