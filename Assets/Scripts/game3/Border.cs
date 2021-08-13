using UnityEngine;
using System.Collections;

public class Border : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D col) {
        if (
            col.gameObject.CompareTag("FlowerDodo") ||
            col.gameObject.CompareTag("GoldenDodo") ||
            col.gameObject.CompareTag("PirateDodo") ||
            col.gameObject.CompareTag("RGBDodo")
        ) {
            col.gameObject.GetComponent<DodoController3>().resetPosition();
        }
    }
}