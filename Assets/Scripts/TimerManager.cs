using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public GameConstants gameConstants;

    public UnityEvent onPlayerSurvives;

    public Text countdownText;
    public Text timer;
    public AudioSource countdownAudio;
    private int timeLeft;
    private string minutes;
    private string seconds;

    void Start()
    {
        timeLeft = gameConstants.gameDuration;
        minutes = Mathf.Floor(timeLeft / 60).ToString("00");
        seconds = Mathf.RoundToInt(timeLeft % 60).ToString("00");
        timer.text = minutes + ":" + seconds;

        StartCoroutine("countdown");
    }

    IEnumerator countdown()
    {
        countdownText.text = "3";
        countdownAudio.Play();
        yield return new WaitForSeconds(0.7f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1.0f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1.0f);
        countdownText.text = "Escape!";
        StartCoroutine("startTimer");
        yield return new WaitForSeconds(1.0f);
        countdownText.text = string.Empty;
    }

    IEnumerator startTimer()
    {
        for (int i=0; i <= gameConstants.gameDuration; i++) {

            minutes = Mathf.Floor(timeLeft / 60).ToString("00");
            seconds = Mathf.RoundToInt(timeLeft % 60).ToString("00");

            timer.text = minutes + ":" + seconds;

            yield return new WaitForSeconds(1.0f);
            timeLeft -= 1;
        }
        onPlayerSurvives.Invoke();
    }
}
