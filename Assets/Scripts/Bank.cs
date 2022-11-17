using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] public int currentBalance;
    [SerializeField] TextMeshProUGUI displayBalance;
    [SerializeField] TextMeshProUGUI displayLose;
    [SerializeField] TextMeshProUGUI displayScore;
    public int currentScore;
    public int GetCurrentBalance()
    {
        return currentBalance;
    }

    private void Awake()
    {
        currentBalance = startingBalance;
        displayLose.enabled = false;
        updateDisplay();
    }
    private void Start()
    {
        currentScore = 0;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
        displayScore.text = "Score: " + currentScore;
    }
    public void Deposit(int amount)
    {
        currentBalance = currentBalance + Mathf.Abs(amount);
        updateDisplay();
    }
    public void Withdraw(int amount)
    {
        currentBalance = currentBalance - Mathf.Abs(amount);
        updateDisplay();
        if (currentBalance < 0)
        {
            Debug.Log("You lose!");
            displayLose.enabled = true;
            //ReloadLevel();
        }
    }
    void ReloadLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    void updateDisplay()
    {
        displayBalance.text = "Gold: " + currentBalance;
    }

    public void AddScore(int reward)
    {
        currentScore = currentScore + reward;
    }
}