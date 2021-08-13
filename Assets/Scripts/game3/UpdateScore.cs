using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateScore : MonoBehaviour
{
    public Text Textlives;
    private int totallife=5;
    public Vector3 vector3;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ifdie(){
        // if(Player.transform.position!=vector3){
        totallife -=1;
        Textlives.text = "Lives: "  + totallife.ToString();
        // }
    }
}
