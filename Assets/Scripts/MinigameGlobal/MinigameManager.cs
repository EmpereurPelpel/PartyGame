using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum MinigamePhase
{
    Loading,
    WaitForReady,
    Timer,
    Playing,
    End
}
public class MinigameManager : MonoBehaviour
{
    #region Singleton
    private static MinigameManager instance;
    public static MinigameManager Instance => instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError($"Only one instance of {this.name} authorized !");
    }
    #endregion

    private Transform player1;
    private Transform player2;

    public Transform Player1 => player1;
    public Transform Player2 => player2;

    [SerializeField] private Transform playerPrefab;


    private MinigamePhase currentMinigamePhase;
    public MinigamePhase CurrentMinigamePhase => currentMinigamePhase;

    private float timerReady = 3f;

    [SerializeField] private GameStartCountdownUI gameStartCountdownUI;
    [SerializeField] private GameObject readyScreen;
    [SerializeField] private TextMeshProUGUI playerWinText;
    [SerializeField] private GameObject endScreen;

    public event EventHandler OnMinigameStart;
    public event EventHandler OnMinigameLoaded;

    private void Start()
    {
        InitMiniGame();
    }

    private void InitMiniGame()
    {
        currentMinigamePhase = MinigamePhase.Loading;
        player1 = Instantiate(playerPrefab);
        player1.GetComponent<MinigamePlayerController>().SetPlayerNumber(PlayerNumber.Player1);
        Instantiate(GameManager.Instance.CharacterModelP1, player1);
        player2 = Instantiate(playerPrefab);
        player2.GetComponent<MinigamePlayerController>().SetPlayerNumber(PlayerNumber.Player2);
        Instantiate(GameManager.Instance.CharacterModelP2, player2);
        OnMinigameLoaded?.Invoke(this, EventArgs.Empty);
        currentMinigamePhase = MinigamePhase.WaitForReady;
    }

    private void Update()
    {
        if (currentMinigamePhase == MinigamePhase.Timer)
            ManageTimerReady();
    }

    public void StartTimer()
    {
        gameStartCountdownUI.Show();
        readyScreen.SetActive(false);
        currentMinigamePhase = MinigamePhase.Timer;
        timerReady = 3f;
    }

    private void ManageTimerReady()
    {
        if (timerReady > 0)
        {
            timerReady -= Time.deltaTime;
        }
        else
        {
            gameStartCountdownUI.Hide();
            currentMinigamePhase = MinigamePhase.Playing;
            OnMinigameStart?.Invoke(this, EventArgs.Empty);
        }
    }

    public float GetCountdownToStartTimer()
    {
        return timerReady;
    }

    public void EndMinigame(PlayerNumber winner)
    {
        currentMinigamePhase = MinigamePhase.End;
        GameManager.Instance.EndMinigame(winner);
        endScreen.SetActive(true);
        playerWinText.text = $"PLAYER {(winner == PlayerNumber.Player1 ? 1 : 2)} WINS!";
    }

    public void BackToBoardScene()
    {
        GameManager.Instance.ChangeToBoardScene();
    }

    
}
