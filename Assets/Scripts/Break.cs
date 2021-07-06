using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    // public GameObject gameObject;
    private bool broken = false;
    public  GameObject Debris; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void  OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Player") &&  !broken){
            broken  =  true;
            // assume we have 5 debris per box
            for (int x =  0; x<5; x++){
                Instantiate(Debris, transform.position, Quaternion.identity);
            }
            gameObject.transform.GetComponent<SpriteRenderer>().enabled  =  false;
            gameObject.transform.GetComponent<BoxCollider2D>().enabled  =  false;
            GetComponent<EdgeCollider2D>().enabled  =  false;
            // GetComponent<SpriteRenderer>().enabled = false;

        }
    }
}
