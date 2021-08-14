using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
    public GameObject prefab;
    public float interval = 1;
    public Vector2 velocity = Vector2.down;

    public Vector3 vector3;

    public GameObject woodResource;


    // Use this for initialization
    void Start () {
        InvokeRepeating("SpawnNext", 0, interval);
    }

    void SpawnNext() {
        Debug.Log(this.gameObject.name);
        // Instantiate
        GameObject g =(GameObject)Instantiate(
            prefab,
            transform.position,
            Quaternion.Euler(vector3)
        );

        // Set Velocity
        g.GetComponent<Rigidbody2D>().velocity = velocity;

        float randomValue = Random.Range(0.0f, 1.0f);

        if (woodResource && randomValue > 0.7f) {
            // Debug.Log(g.GetComponent<SpriteRenderer>().sprite.rect.width);
            Debug.Log(g.transform.localScale);
            // Debug.Log(woodResource.transform.localScale);
            // float logHalfWidth = g.GetComponent<SpriteRenderer>().sprite.rect.width / 2.3f;
            float offset = 0.8f;
            GameObject wood = (GameObject) Instantiate(
                woodResource,
                new Vector3(g.transform.position.x, Random.Range(g.transform.position.y - offset, g.transform.position.y + offset), g.transform.position.z),
                Quaternion.Euler(vector3)
            );
            Debug.Log(wood.transform.localScale);
            wood.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            wood.GetComponent<Rigidbody2D>().velocity = velocity;
        }
    }
}