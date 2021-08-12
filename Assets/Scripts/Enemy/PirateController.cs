using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public Transform target;
    private Vector3 currentPos;
    private Vector3 dodoPos;
    private bool faceRight = true;
    public float attackRadius = 2.0f;
    public float speed;
    public bool called = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // currentPos = transform.position;
        // dodoPos = dodoPlayer.position;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(target.transform.position,transform.position);
        if (dist > attackRadius)
        {
            animator.SetBool("xAttack",false);
            float step =  speed * Time.deltaTime;
            // Debug.Log("current pos: "+ transform.position);
            // Debug.Log("dodo pos: "+ target.position);
            // Debug.Log("new pos: "+Vector3.MoveTowards(transform.position,target.transform.position, step));
            animator.SetFloat("xSpeed", Mathf.Abs(rigidBody.velocity.x));
            animator.SetBool("xAttack",false);
            transform.position = Vector3.MoveTowards(transform.position,target.transform.position, step);
            if ( transform.position.x < target.position.x){
                faceRight = true;
                spriteRenderer.flipX=false;
            }
            else
            {
                faceRight = false;
                spriteRenderer.flipX=true;
            }
        }
        else
        {
            animator.SetBool("xAttack",true);
            if (called==false){
                CentralManager.centralManagerInstance.killPlayer();
                called = true;
            }
            // 
            Debug.Log("Player dieds");
        }

    }
}
