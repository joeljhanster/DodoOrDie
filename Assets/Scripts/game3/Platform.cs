using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    void OnTriggerStay2D(Collider2D coll) {
        // Frog? Then make it a Child
        if (coll.name == "Player")
            coll.transform.parent = transform;
    }

    void OnTriggerExit2D(Collider2D coll) {
        coll.transform.parent = null;        
    }
}
