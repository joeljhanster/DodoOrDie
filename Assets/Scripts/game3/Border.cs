using UnityEngine;
using System.Collections;

public class Border : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.name == "Player")
            Destroy(coll.gameObject);
    }
}