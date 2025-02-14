using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Assign in Inspector
    public float timeRemaining = 120f; // Start at 2 minutes
    private bool timerRunning = true;

    public GameObject obj; // General UI object (Optional)
    public GameObject player1WinUI;
    public GameObject player2WinUI;
    public GameObject player3WinUI;
    public GameObject player4WinUI;

    void Update()
    {
        if (timerRunning && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else if (timerRunning && timeRemaining <= 0)
        {
            obj.SetActive(true); // Activate general UI (optional)
            timeRemaining = 0;
            timerRunning = false;
            StartCoroutine(DelayedScoreCheck()); // Start 2-second delay
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"{minutes:00}:{seconds:00}"; // Format MM:SS
    }

    IEnumerator DelayedScoreCheck()
    {
        yield return new WaitForSeconds(4f); // Wait for 2 seconds before checking scores
        OnTimerEnd();
    }

    void OnTimerEnd()
    {


        // Store player scores in an array
        int[] scores = { destroy.player1Score, destroy.player2Score, destroy.player3Score, destroy.player4Score };
        int highestScore = Mathf.Max(scores);
        List<int> winners = new List<int>();

        // Find all winners
        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] == highestScore)
            {
                winners.Add(i + 1); // Player numbers start from 1
            }
        }

        // Activate the respective UI objects for the winners
        foreach (int winner in winners)
        {
            if (winner == 1) player1WinUI.SetActive(true);
            if (winner == 2) player2WinUI.SetActive(true);
            if (winner == 3) player3WinUI.SetActive(true);
            if (winner == 4) player4WinUI.SetActive(true);
        }

        // Debugging
        if (winners.Count == 1)
        {
            Debug.Log($"Player {winners[0]} Wins!");
        }
        else
        {
            Debug.Log("It's a tie between players: " + string.Join(", ", winners));
        }
    }
}