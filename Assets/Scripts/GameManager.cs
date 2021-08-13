using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public  Text score;
	private  int playerScore =  0;
	public  delegate  void gameEvent();
    public  static  event  gameEvent OnPlayerDeath;
    public  static  event  gameEvent OnEnemyDeath;
    // Singleton Pattern
    private  static  GameManager _instance;

    // Getter
    public  static  GameManager Instance
    {
	    get { return  _instance; }
    }

    // override  public  void  Awake(){
	// 	base.Awake();
	// 	// Debug.Log("awake called");
	// 	// other instructions...
	// }



	public  void  increaseScore(){
		playerScore  +=  1;
		score.text  =  "SCORE: "  +  playerScore.ToString();
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public  void  killPlayer(){
	    OnPlayerDeath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
