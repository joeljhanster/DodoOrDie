using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesMonitor : MonoBehaviour
{
    public IntVariable dodoLives;
    public GameConstants gameConstants;
    public Text text;

    public void UpdateLives()
    {
        Debug.Log("Updating lives");
        text.text = "Lives: " + dodoLives.Value.ToString();
    }

    public void Start()
    {
        dodoLives.SetValue(gameConstants.startingLives);
        UpdateLives();
    }
}
