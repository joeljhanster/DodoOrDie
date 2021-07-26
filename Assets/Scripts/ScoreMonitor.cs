using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMonitor : MonoBehaviour
{
    public IntVariable woodCollected;
    public Text text;

    public void UpdateScore()
    {
        Debug.Log("Updating score");
        text.text = "x " + woodCollected.Value.ToString();
    }

    public void Start()
    {
        UpdateScore();
    }
}
