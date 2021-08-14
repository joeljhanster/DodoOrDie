using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMonitor : MonoBehaviour
{
    public List<DodoCharacter> dodoCharacters;
    public Text text;

    public void UpdateScore()
    {
        Debug.Log("Updating score");
        string scoreText = "Scores: ";
        foreach(DodoCharacter dodo in dodoCharacters) {
            scoreText += "\n" + dodo.dodoName + ": " + dodo.score.ToString(); 
        }
        text.text = scoreText;
    }

    public void Start()
    {
        UpdateScore();
    }
}
