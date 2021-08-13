using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private AudioSource audioSource;
    public Transform target;
    private Vector3 currentPos;
    private Vector3 dodoPos;
    private bool faceRight = true;
    public float attackRadius = 2.0f;
    public float speed;
    private bool called = false;
    public List<Transform> dodoObjects;
    public Transform nearestDodo ;
    public float mindistance=10000000000.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // currentPos = transform.position;
        // dodoPos = dodoPlayer.position;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player eaten by eagle!");
        }
    }
    // Update is called once per frame
    void Update()
    {
        Transform player = null;
        float step =  speed * Time.deltaTime*0.3f;
        foreach(Transform dodo in dodoObjects) {
            if (Mathf.Pow((transform.position.x-dodo.position.x),2)+Mathf.Pow((transform.position.y-dodo.position.y),2)<mindistance){
                nearestDodo = dodo;
                mindistance = Mathf.Pow((transform.position.x-dodo.position.x),2)+Mathf.Pow((transform.position.y-dodo.position.y),2);
            }
        }
        transform.position = Vector3.MoveTowards(transform.position,nearestDodo.transform.position, step);
        if ( transform.position.x < nearestDodo.position.x){
            faceRight = true;
            spriteRenderer.flipX=false;
            }
        else
        {
            faceRight = false;
            spriteRenderer.flipX=true;
            }

        if (mindistance<attackRadius){
            // if (!called){
            // CentralManager.centralManagerInstance.killPlayer();
            // }
            called = true;
        }

    }
}