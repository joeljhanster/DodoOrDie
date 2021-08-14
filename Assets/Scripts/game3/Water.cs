using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {
    void OnTriggerStay2D(Collider2D col) {
        // Frog?
        if (
            col.gameObject.CompareTag("FlowerDodo") ||
            col.gameObject.CompareTag("GoldenDodo") ||
            col.gameObject.CompareTag("PirateDodo") ||
            col.gameObject.CompareTag("RGBDodo")
        ) {
            // Not jumping?
            if (!col.gameObject.GetComponent<DodoController3>().isJumping()) {
                // Not on a platform?
                if (col.transform.parent == null) {
                    // Player dies
                    // respawn(col.gameObject);
                    col.gameObject.GetComponent<DodoController3>().resetPosition();
                }
            }
        }
        // else if (col.gameObject.CompareTag("GoldenDodo")) {
        //     // Not jumping?
        //     if (!col.gameObject.GetComponent<DodoController3>().isJumping()) {
        //         // Not on a platform?
        //         if (col.transform.parent == null) {
        //             // Player dies
        //             // respawn(col.gameObject);
        //             col.gameObject.GetComponent<DodoController3>().resetPosition();
        //         }
        //     }
        // }
        // else if (col.gameObject.CompareTag("PirateDodo")) {
        //     // Not jumping?
        //     if (!col.gameObject.GetComponent<DodoController3>().isJumping()) {
        //         // Not on a platform?
        //         if (col.transform.parent == null) {
        //             // Player dies
        //             // respawn(col.gameObject);
        //             col.gameObject.GetComponent<DodoController3>().resetPosition();
        //         }
        //     }
        // }
        // else if (col.gameObject.CompareTag("RGBDodo")) {
        //     // Not jumping?
        //     if (!col.gameObject.GetComponent<DodoController3>().isJumping()) {
        //         // Not on a platform?
        //         if (col.transform.parent == null) {
        //             // Player dies
        //             // respawn(col.gameObject);
        //             col.gameObject.GetComponent<DodoController3>().resetPosition();
        //         }
        //     }
        // }
    }
}
