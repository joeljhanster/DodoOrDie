using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{  
    private Animator eggAnimator;
    private GameObject eggObject;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        eggAnimator = GetComponent<Animator>();
        eggObject = GetComponent<GameObject>();
        StartCoroutine(explode());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator explode()
    {
        yield return new WaitForSeconds(1.0f);
        // eggAnimator.SetBool("xExplode",true);
        GameObject newObject = Instantiate(explosion,transform.position,Quaternion.identity) as GameObject;
        if(gameObject)
        {
            Destroy(gameObject);
        }
        if(newObject)
        {
            Destroy(newObject,3f);
        }
    }
}
