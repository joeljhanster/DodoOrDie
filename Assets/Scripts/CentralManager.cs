using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CentralManager : MonoBehaviour
{
    public  GameObject gameManagerObject;
	private  GameManager gameManager;
	public  static  CentralManager centralManagerInstance;
    // add reference to PowerupManager
    // public  GameObject powerupManagerObject;
    // private  PowerupManager powerUpManager;
    // public  GameObject spawnManagerObject;
    // private  SpawnManager spawnManager;

    void  Awake()
    {
		// centralManagerInstance  =  this;
	}

    // Start is called before the first frame update
    void Start()
    {
        centralManagerInstance  =  this;
        gameManager  =  gameManagerObject.GetComponent<GameManager>();
        // // instantiate in start
        // powerUpManager  =  powerupManagerObject.GetComponent<PowerupManager>();
        // spawnManager = spawnManagerObject.GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  void  killPlayer(){
	    Debug.Log("Central Manager: killPlayer()");
        gameManager.killPlayer();
    }

    public  void  increaseScore()
    {
        Debug.Log("Central Manager: increaseScore()");
		gameManager.increaseScore();
	}

    // public  void  consumePowerup(KeyCode k, GameObject g)
    // {
    //     Debug.Log("Central Manager: consumePowerup()");
    //     powerUpManager.consumePowerup(k,g);
    // }

    // public  void  addPowerup(Texture t, int i, ConsumableInterface c)
    // {
    //     Debug.Log("Central Manager: addPowerup()");
    //     powerUpManager.addPowerup(t, i, c);
    // }

    // public void spawnNewEnemy()
    // {
    //     Debug.Log("Central Manager: spawnNewEnemy()");
    //     spawnManager.spawnNewEnemy();
    // }

    //  public void changeScene()
    // {
    //     StartCoroutine(LoadYourAsyncScene("MarioLevel2"));
    // }


    // IEnumerator LoadYourAsyncScene(string sceneName)
    // {
    //     // The Application loads the Scene in the background as the current Scene runs.
    //     // This is particularly good for creating loading screens.
    //     AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    //     // Wait until the asynchronous scene fully loads
    //     while (!asyncLoad.isDone)
    //     {
    //         yield return null;
    //     }
    // }

}
