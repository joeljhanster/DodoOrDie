using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class EndLevelController : MonoBehaviour
{
    public UnityEvent onSceneChange;
    private AudioSource celebrationAudio;
    // Start is called before the first frame update
    void Start()
    {
        celebrationAudio = GetComponent<AudioSource>();
    }

    void  OnTriggerEnter2D(Collider2D other){
        if (
            other.gameObject.CompareTag("FlowerDodo") || 
            other.gameObject.CompareTag("GoldenDodo") ||
            other.gameObject.CompareTag("PirateDodo") ||
            other.gameObject.CompareTag("RGBDodo")
        ){
            onSceneChange.Invoke();
            Debug.Log("Level Ended!");
            celebrationAudio.Play();

            // string currScene = SceneManager.GetActiveScene().name;
            // Debug.Log(currScene);
            // // SceneManager.LoadScene("Game2TOGame3");
            // StartCoroutine(changeScene());
        }
    }

    // IEnumerator changeScene(){
    //     Debug.Log("Changing Scene...");
    //     yield return new WaitForSeconds(6.5f);
    //     celebrationAudio.Stop();
    //     AsyncOperation asyncLoaded = SceneManager.LoadSceneAsync("Game2TOGame3", LoadSceneMode.Single);
    //     while(!asyncLoaded.isDone){
    //         yield return null;
    //     }
    // }
}
