using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

    void OnTriggerStay2D(Collider2D coll) {
        // Frog?
        if (coll.name == "Player")
            // Not Jumping?
            if (!coll.GetComponent<Frog>().isJumping())
                // Not on a platform?
                if (coll.transform.parent == null)
                    // Game Over
                    Destroy(coll.gameObject);
    }
}
