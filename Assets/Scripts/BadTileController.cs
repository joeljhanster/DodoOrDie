using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BadTileController : MonoBehaviour
{
      public UnityEvent onPlayerEaten;
      public DodoCharacter flowerDodo;
      public DodoCharacter goldenDodo;
      public DodoCharacter pirateDodo;
      public DodoCharacter rgbDodo;

      void  OnTriggerEnter2D(Collider2D other){
            // check if it collides with player
            // if (other.gameObject.tag  ==  "Player"){
            // Debug.Log("Kill Player");
            // // Kill Player
            // CentralManager.centralManagerInstance.killPlayer();
            // }
            
            if (other.gameObject.CompareTag("FlowerDodo")) {
                  if (!other.gameObject.GetComponent<DodoController2>().getImmunity()) {
                        Debug.Log("Spikes killed flower dodo");
                        flowerDodo.AddLives(-1);
                        onPlayerEaten.Invoke();
                        other.gameObject.GetComponent<DodoController2>().PlayerDiesSequence();
                  }
            }
            else if (other.gameObject.CompareTag("GoldenDodo")) {
                  if (!other.gameObject.GetComponent<DodoController2>().getImmunity()) {
                        Debug.Log("Spikes killed golden dodo");
                        goldenDodo.AddLives(-1);
                        onPlayerEaten.Invoke();
                        other.gameObject.GetComponent<DodoController2>().PlayerDiesSequence();
                  }
            }
            else if (other.gameObject.CompareTag("PirateDodo")) {
                  if (!other.gameObject.GetComponent<DodoController2>().getImmunity()) {
                        Debug.Log("Spikes killed pirate dodo");
                        pirateDodo.AddLives(-1);
                        onPlayerEaten.Invoke();
                        other.gameObject.GetComponent<DodoController2>().PlayerDiesSequence();
                  }
            }
            else if (other.gameObject.CompareTag("RGBDodo")) {
                  if (!other.gameObject.GetComponent<DodoController2>().getImmunity()) {
                        Debug.Log("Spikes killed rgb dodo");
                        rgbDodo.AddLives(-1);
                        onPlayerEaten.Invoke();
                        other.gameObject.GetComponent<DodoController2>().PlayerDiesSequence();
                  }
            }
      }
}
