using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Options")]
    bool gameOver = false;
    int score = 0;

    [Header("Timers")]
    public float checkWinnerTime = 120;

    [Header("Game Objects")]
    public GameObject players;

    [Header("UI")]
    public TMP_Text winnerText;
    public GameObject gameOverPanel;

    private List<GameObject> foodCollection = new List<GameObject>();
    private int numPlayersFell = 0;

    private void Awake() {
        players = GameObject.Find("Players");
        // gameOverPanel.SetActive(false);

        if (instance == null) {
            instance = this;
        }

        // Listen to player events
        PlayerController.OnFallOff += PlayerFell;

        StartCoroutine(InitTimerEnd());
    }

    // Map Events

    IEnumerator InitTimerEnd() {
        yield return new WaitForSeconds(checkWinnerTime);
        CheckWinner();
    }

    public void IncrementScore() {
        if (!gameOver){
            score++;
        }
    }

    public void PlayerFell(PlayerController player) {
        numPlayersFell += 1;
        int numPlayersLeft = StaticValues.numPlayers - numPlayersFell;
        if (numPlayersLeft == 1) {
            CheckWinner();
        }
    }

    public void CheckWinner() {
        List<GameObject> playersLeft = new List<GameObject>{};
        for (int i = 0; i < StaticValues.players.Length; i++) {
            if (StaticValues.players[i].activeSelf) {
                playersLeft.Add(StaticValues.players[i]);
            }
        }

        if (playersLeft.Count == 1) {
            winnerText.text = playersLeft[0].GetComponent<PlayerController>().playerId.ToString();
        } else {
            var maxvalue = playersLeft.Max(w => w.GetComponent<PlayerController>().score);
            var player = playersLeft.First(w => w.GetComponent<PlayerController>().score == maxvalue);
            winnerText.text = player.GetComponent<PlayerController>().playerId.ToString();
        }

        numPlayersFell = 0;
        gameOver = true;
        if (gameOverPanel != null) {
            gameOverPanel.SetActive(true);
        }
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene("MenuScene");
    }

    public void RestartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
