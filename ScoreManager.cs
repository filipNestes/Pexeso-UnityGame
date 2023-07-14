using System.Net.Sockets;
using System.Text.RegularExpressions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int timeForLevelToComplete = 60;
    public Image timeImage;
    public TMP_Text timeText;
    public TMP_Text scoreText;
    public TMP_Text turnsText;

    Coroutine timer;
    int score;
    int turns;

    void Awake() {
        instance = this;
    }

    void Start()
    {
        timer = StartCoroutine(Timer());
        AddScore(0);
    }

    IEnumerator Timer() {
        int tempTime = timeForLevelToComplete;
        timeText.text = timeForLevelToComplete.ToString();
        while (tempTime > 0)
        {
            tempTime--;
            yield return new WaitForSeconds(1);

            timeImage.fillAmount = tempTime / (float)timeForLevelToComplete;
            timeText.text = tempTime.ToString();
        }
        // GAME OVER
        GameManager.instance.GameOver();
    }

    public void AddScore(int scoreToAdd) {
        score+= scoreToAdd;
        scoreText.text = score.ToString("D4");
    }

    public void AddTurn() {
        turns++;
        turnsText.text = turns.ToString("D2");
    }

    public void StopTimer() {
        StopCoroutine(timer);
    }
}
