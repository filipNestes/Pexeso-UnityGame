using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    bool picked; // SET TRUE IF WE HAVE 2 CARDS PICKED
    bool gameOver;
    int pairs;
    int pairCounter;
    public bool hideMatches;
    public int scorePerMatch = 85;

    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject winEffect;

    List<Card> pickedCards = new List<Card>();

    void Awake() {
        instance = this;
    }

    void Start()
    {
        winPanel.SetActive(false);
        winEffect.SetActive(false);
        losePanel.SetActive(false);
    }

    public void AddCardToPickedList(Card card) {
        if(pickedCards.Contains(card)) {
            return;
        }
        pickedCards.Add(card);
        if (pickedCards.Count == 2) {
            picked = true;
            // CHECK A MATCH
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch() {
        yield return new WaitForSeconds(1.5f);
        if (pickedCards[0].GetCardId() == pickedCards[1].GetCardId()) {
            // CARD MATCH
            if (hideMatches) {
                pickedCards[0].gameObject.SetActive(false);
                pickedCards[1].gameObject.SetActive(false);
            } else {
                pickedCards[0].GetComponent<BoxCollider>().enabled = false;
                pickedCards[1].GetComponent<BoxCollider>().enabled = false;
            }

            pairCounter++;
            CheckForWin();

            // ACTIVATE EFFECTS
            pickedCards[0].ActivateEffect();    
            pickedCards[1].ActivateEffect();    

            ScoreManager.instance.AddScore(scorePerMatch);
        } else {
            pickedCards[0].FlipOpen(false);
            pickedCards[1].FlipOpen(false);
            yield return new WaitForSeconds(1.5f);
        }

        // CLEAN UP
        picked = false;
        pickedCards.Clear();
        ScoreManager.instance.AddTurn();

    }

    void CheckForWin() {
        if (pairs == pairCounter) {
            // WON GAME
            winPanel.SetActive(true);
            winEffect.SetActive(true);
            ScoreManager.instance.StopTimer();
          //  Debug.Log("WON GAME");
        }
    }

    public void GameOver() {
        gameOver = true;
        losePanel.SetActive(true);
       // Debug.Log("LOST GAME");
    }

    public bool IsGameOver() {
        return gameOver;
    }

    public bool HasPicked() {
        return picked;
    }

    public void SetPairs(int pairAmount) {
        pairs = pairAmount;
    }
}
