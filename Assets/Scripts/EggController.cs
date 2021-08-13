using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{  
    private Animator eggAnimator;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(explode());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator explode()
    {
        yield return new WaitForSeconds(3.0f);
        eggAnimator.SetBool("xExplode",true);
        Destroy(this.gameObject);
    }
}
