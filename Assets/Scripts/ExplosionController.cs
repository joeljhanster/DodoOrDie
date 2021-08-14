using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PirateBeard"))
        {
            // Kill pirate
            Debug.Log("Pirate got killed by explosion");
            col.gameObject.GetComponent<PirateController>().KillSelf();
        }
    }
}
