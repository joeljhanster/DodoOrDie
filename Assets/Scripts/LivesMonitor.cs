using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LivesMonitor : MonoBehaviour
{
    public GameConstants gameConstants;

    public List<DodoCharacter> dodoCharacters;

    public Sprite fullHeart;
    public Sprite emptyHeart;

    public GameObject restartMenu;

    public void UpdateLives()
    {
        Debug.Log("Updating lives");
        bool allDead = true;
        foreach(DodoCharacter dodo in dodoCharacters)
        {
            for (int i=0; i<gameConstants.startingLives; i++)
            {
                string path = dodo.dodoScorePanel.name + "/Lives/Life" + (i+1).ToString();
                GameObject life = GameObject.Find(path);
                if (life)
                {
                    if (i < dodo.lives && dodo.taken) {
                        life.GetComponent<Image>().sprite = fullHeart;
                        allDead = false;
                    } else {
                        life.GetComponent<Image>().sprite = emptyHeart;
                    }
                }
            }
        }
        if (allDead) {
            restartMenu.SetActive(true);
        }
    }

    public void UpdateScore()
    {
        Debug.Log("Updating score");
        foreach(DodoCharacter dodo in dodoCharacters)
        {
            string path = dodo.dodoScorePanel.name + "/DodoImage/Score";
            GameObject scoreText = GameObject.Find(path);
            if (scoreText)
            {
                if (dodo.taken) {
                    scoreText.GetComponent<TextMeshProUGUI>().text = dodo.score.ToString();
                } else {
                    scoreText.GetComponent<TextMeshProUGUI>().text = "0";
                }
                
            }
        }
    }

    public void Start()
    {
        foreach(DodoCharacter dodo in dodoCharacters)
        {
            if (dodo.taken) {
                dodo.SetLives(gameConstants.startingLives);
            } else {
                dodo.SetLives(0);
            }
        }
        UpdateLives();
        UpdateScore();
    }
}
