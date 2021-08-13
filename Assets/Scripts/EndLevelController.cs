using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelController : MonoBehaviour
{
    private AudioSource celebrationAudio;
    // Start is called before the first frame update
    void Start()
    {
        celebrationAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void  OnTriggerEnter2D(Collider2D other){
        if (
            other.gameObject.CompareTag("FlowerDodo") || 
            other.gameObject.CompareTag("GoldenDodo") ||
            other.gameObject.CompareTag("PirateDodo") ||
            other.gameObject.CompareTag("RGBDodo")
        ){
            string currScene = SceneManager.GetActiveScene().name;
            Debug.Log(currScene);
            Debug.Log("Level Ended!");
            // SceneManager.LoadScene("Game2TOGame3");
            celebrationAudio.Play();
            StartCoroutine(changeScene());
        }
    }

    IEnumerator changeScene(){
        Debug.Log("Changing Scene...");
        yield return new WaitForSeconds(6.5f);
        celebrationAudio.Stop();
        AsyncOperation asyncLoaded = SceneManager.LoadSceneAsync("Game2TOGame3", LoadSceneMode.Single);
        while(!asyncLoaded.isDone){
            yield return null;
        }
    }
}
