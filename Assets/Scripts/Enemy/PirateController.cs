using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private AudioSource audioSource;
    private bool faceRight = true;
    public float attackRadius = 1.5f;
    public float speed;
    private bool called = false;
    public List<GameObject> dodoObjects;
    
    public AudioSource swordAudio;

    private float viewportHalfWidthX;
    private float viewportHalfHeightY;
    private Vector3 bottomLeft;

    private Vector3 originalPosition;

    public GameObject woodResource;

    private bool playerSurvives = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        swordAudio = GetComponent<AudioSource>();

        originalPosition = transform.position;

        // currentPos = transform.position;
        // dodoPos = dodoPlayer.position;
    }

    // void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.gameObject.CompareTag("Player"))
    //     {
    //         Debug.Log("Player eaten by eagle!");
    //     }
    // }

    public void KillSelf()
    {
        Debug.Log("Pirate killing self");
        // Spawn Wood
        GameObject wood = (GameObject) Instantiate(
            woodResource,
            transform.position,
            Quaternion.identity
        );
        wood.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        GameObject light = GameObject.Find(wood.name + "/Light 2D");
        light.SetActive(false);

        // Reset Pirate Position
        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));

        viewportHalfWidthX = Mathf.Abs(bottomLeft.x - Camera.main.transform.position.x);
        viewportHalfHeightY = Mathf.Abs(bottomLeft.y - Camera.main.transform.position.y);

        Vector2 position = new Vector2(
            Random.Range(bottomLeft.x - viewportHalfWidthX, bottomLeft.x + viewportHalfWidthX * 3),
            Random.Range(bottomLeft.y - viewportHalfHeightY * 2, bottomLeft.y - viewportHalfHeightY)
        );
        rigidBody.MovePosition(position);
    }

    public void playerSurvivesResponse()
    {
        // Pirate dies or run away or kill the dodo bird with least score
        playerSurvives = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        float minDistance=10000000000.0f;
        int minScore = 100000;
        Transform nearestDodo = new GameObject().transform;
        nearestDodo.position = originalPosition;

        foreach(GameObject dodo in dodoObjects) {
            Debug.Log(dodo.name);
            if (dodo && dodo.activeSelf && !dodo.GetComponent<DodoController4>().getImmuneStatus()){
                Debug.Log("Calculating distance for " + dodo.name);
                float distance = 
                    Mathf.Pow((transform.position.x-dodo.transform.position.x), 2) + 
                    Mathf.Pow((transform.position.y-dodo.transform.position.y), 2);
                
                if (!playerSurvives) {
                    if (distance < minDistance) {
                        Debug.Log("Changing nearest dodo: " + dodo.name);
                        nearestDodo = dodo.transform;
                        minDistance = distance;
                    }
                } else {
                    // Chase dodo with lowest score
                    if (dodo.GetComponent<DodoController4>().dodoCharacter.score < minScore) {
                        nearestDodo = dodo.transform;
                        minDistance = distance;
                    }
                }
                
            }
        }

        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, nearestDodo.transform.position, step);
        // Debug.Log(step);
        // Debug.Log("xSpeed1",rigidBody.velocity.x);
        animator.SetFloat("xSpeed", Mathf.Abs(step));
        if (transform.position.x < nearestDodo.position.x){
            faceRight = true;
            spriteRenderer.flipX=false;
            }
        else
        {
            faceRight = false;
            spriteRenderer.flipX=true;
            }
        
        if (minDistance < attackRadius){
            animator.SetBool("xAttack",true);
            // swordAudio.Play(0);
        }
        else{
            animator.SetBool("xAttack",false);
        }

    }
}